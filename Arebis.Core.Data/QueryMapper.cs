﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Arebis.Core.Extensions;

namespace Arebis.Core.Data
{
    /// <summary>
    /// Mapper to perform SQL queries and return results as mapped to typed objects or dynamic ExpandoObjects.
    /// </summary>
    public class QueryMapper : IDisposable
    {
        private static readonly Regex numerical = new Regex("^[0-9]+$", RegexOptions.Compiled);

        private bool isDisposed = false;

        /// <summary>
        /// Constructs a QueryMapper.
        /// </summary>
        public QueryMapper(DbConnection connection, string sql, CommandType commandType = CommandType.Text)
        {
            if (connection.State != ConnectionState.Open) connection.Open();
            this.Connection = connection;
            this.Command = connection.CreateCommand();
            this.Command.CommandText = sql;
            this.Command.CommandType = commandType;
        }

        /// <summary>
        /// Database connection.
        /// </summary>
        public DbConnection Connection { get; private set; }

        /// <summary>
        /// Query command.
        /// </summary>
        public DbCommand Command { get; private set; }

        private DbDataReader? reader;

        /// <summary>
        /// Query results reader.
        /// </summary>
        public DbDataReader Reader
        {
            get
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException("(QueryMapper)", "The invoked QueryMapper has been disposed. Avoid Linq delayed execution after disposing the QueryMapper.");
                }
                if (this.reader == null)
                {
                    this.reader = this.Command.ExecuteReader();
                }
                return this.reader;
            }
        }

        /// <summary>
        /// Asserts no reader is set yet.
        /// </summary>
        protected void AssertNoReader()
        {
            if (this.reader != null) throw new Exception("Cannot set parameters on QueryMapper once reader is created.");
        }

        /// <summary>
        /// Adds parameters for each property of the given object.
        /// </summary>
        public QueryMapper WithParameters(object parameterObject, string parameterNamePrefix = "@", string parameterNameSuffix = "")
        {
            return this.WithParameters(parameterObject.ToDictionary(), parameterNamePrefix, parameterNameSuffix);
        }

        /// <summary>
        /// Adds parameters for each element of the dictionary.
        /// </summary>
        public QueryMapper WithParameters(IDictionary<string, object?> parameters, string parameterNamePrefix = "@", string parameterNameSuffix = "")
        {
            AssertNoReader();

            if (parameters != null)
            {
                foreach (var pair in parameters)
                {
                    this.Command.AddParameter(parameterNamePrefix + pair.Key + parameterNameSuffix, pair.Value);
                }
            }
            return this; // to support fluent notation.
        }

        /// <summary>
        /// Adds a parameter.
        /// </summary>
        public QueryMapper WithParameter(string name, object value, ParameterDirection direction = ParameterDirection.Input, DbType dbType = DbType.String, int? size = null)
        {
            AssertNoReader();

            this.Command.AddParameter(name, value, direction, dbType, size);

            return this; // to support fluent notation.
        }

        /// <summary>
        /// Adds a parameter.
        /// </summary>
        public QueryMapper WithParameter(DbParameter parameter)
        {
            AssertNoReader();

            this.Command.Parameters.Add(parameter);

            return this; // to support fluent notation.
        }

        /// <summary>
        /// Skips a number of rows.
        /// </summary>
        public QueryMapper Skip(int rowcount)
        {
            for (int r = 0; r < rowcount; r++)
            {
                if (!Reader.Read()) break;
            }

            return this; // to support fluent notation.
        }

        /// <summary>
        /// Maps all rows to ExpandoObjects.
        /// </summary>
        /// <returns>An enumeration of ExpandoOjects.</returns>
        public IEnumerable<dynamic> TakeAll()
        {
            return this.Take(Int32.MaxValue);
        }

        /// <summary>
        /// Take rowcount number of rows and maps them to ExpandoObjects.
        /// </summary>
        /// <param name="rowcount">Up to number of rows to return.</param>
        /// <returns>An enumeration of ExpandoOjects.</returns>
        public IEnumerable<dynamic> Take(int rowcount)
        {
            // Retrieve fields names:
            var fieldcount = Reader.FieldCount;
            var fields = new string[fieldcount];
            for (int f = 0; f < fields.Length; f++)
            {
                fields[f] = Reader.GetName(f);
            }

            // Map rows to ExpandoObjects:
            for (int r = 0; r < rowcount; r++)
            {
                if (!Reader.Read()) break;

                var obj = (IDictionary<string, object?>)new ExpandoObject();
                for (int c = 0; c < fieldcount; c++)
                {
                    obj[fields[c]] = (Reader.IsDBNull(c) ? null : Reader.GetValue(c));
                }
                yield return obj;
            }
        }

        /// <summary>
        /// Maps all rows to objects of type T.
        /// Numerical column names map to default indexer properties.
        /// </summary>
        /// <typeparam name="T">Type of objects to return.</typeparam>
        /// <returns>An enumeration of T objects.</returns>
        public IEnumerable<T> TakeAll<T>()
            where T : new()
        {
            return this.Take<T>(Int32.MaxValue);
        }

        /// <summary>
        /// Take rowcount number of rows and maps them to objects of type T.
        /// Numerical column names map to default indexer properties.
        /// </summary>
        /// <typeparam name="T">Type of objects to return.</typeparam>
        /// <param name="rowcount">Up to number of rows to return.</param>
        /// <returns>An enumeration of T objects.</returns>
        public IEnumerable<T> Take<T>(int rowcount)
            where T : new()
        {
            // Map columns to type properties:
            this.MapColumns<T>(this.Reader, out PropertyInfo[] colProperties, out object[][] colIndexer);

            // Map rows to objects:
            for (int r = 0; r < rowcount; r++)
            {
                if (!Reader.Read()) break;

                var obj = new T();
                for (int c = 0; c < colProperties.Length; c++)
                {
                    var prop = colProperties[c];
                    if (prop != null)
                    {
                        if (!Reader.IsDBNull(c))
                        {
                            prop.SetValue(obj, Reader.GetValue(c), colIndexer[c]);
                        }
                    }
                }
                yield return obj;
            }
        }

        /// <summary>
        /// Maps columns to properties of type T. By default, properties are mapped by name and numerical
        /// column names are mapped to the default indexer properties.
        /// </summary>
        protected virtual void MapColumns<T>(DbDataReader reader, out PropertyInfo[] properties, out object[][] indexes)
        {
            // Map column names to matching property info's:
            Dictionary<string, PropertyInfo> typeProperties = GetPropertiesMap<T>();
            properties = new PropertyInfo[this.Reader.FieldCount];
            indexes = new object[this.Reader.FieldCount][];
            PropertyInfo? indexerProperty = null;
            for (int c = 0; c < properties.Length; c++)
            {
                var columnName = Reader.GetName(c);
                if (typeProperties.TryGetValue(columnName, out PropertyInfo? property))
                {
                    if (property.CanWrite) properties[c] = property;
                }
                else if (numerical.IsMatch(columnName))
                {
                    property = indexerProperty ?? (indexerProperty = typeof(T).GetProperty("Item"));
                    if (property != null && property.CanWrite)
                    {
                        properties[c] = property;
                        indexes[c] = new object[1] { Int32.Parse(columnName) };
                    }
                }
            }
        }

        /// <summary>
        /// Returns a dictionary to help mapping columns to properties.
        /// </summary>
        /// <typeparam name="T">The type to build the dictionary for.</typeparam>
        /// <returns>A dictionary mapping potential column names with properties.</returns>
        protected virtual Dictionary<string, PropertyInfo> GetPropertiesMap<T>()
        {
            var typeProperties = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
            typeProperties.Merge(typeof(T).GetProperties().Select(p => new KeyValuePair<string, PropertyInfo>(p.Name, p)));
            return typeProperties;
        }

        /// <summary>
        /// Returns a datatable filled with all rows. If a datatable is given, that one is filled, otherwise a new datatable is created.
        /// </summary>
        public DataTable FillTable(DataTable? dataTable = null)
        {
            dataTable ??= new DataTable();

            var dataAdapter = DbProviderFactories.GetFactory(this.Connection)!.CreateDataAdapter()!;
            dataAdapter.SelectCommand = this.Connection.CreateCommand(this.Command.CommandText);
            dataAdapter.Fill(dataTable);

            return dataTable;
        }

        /// <summary>
        /// Disposes this query mapper.
        /// </summary>
        public virtual void Dispose()
        {
            if (this.reader != null) this.Reader.Dispose();
            this.Command.Dispose();
            this.isDisposed = true;

            GC.SuppressFinalize(this);
        }
    }
}
