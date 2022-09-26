using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Arebis.Core.Localization
{
    /// <summary>
    /// Resources per key, ordered by path, longest first.
    /// </summary>
    [Serializable]
    public class LocalizationResourceSet
    {
        /// <summary>
        /// The default culture. The culture to fall back to when no values are available in the requested culture.
        /// </summary>
        public string DefaultCulture { get; set; } = null!;

        /// <summary>
        /// The set of cultures contained in this LocalizationResourceSet.
        /// </summary>
        public HashSet<string> Cultures { get; init; } = new();

        /// <summary>
        /// The resources that make up this LocalizationResourceSet.
        /// </summary>
        public Dictionary<string, List<LocalizationResource>> Resources { get; init; } = new();

        /// <summary>
        /// Adds a localization resource to the set in the right position.
        /// If one already exists with same key and path, overwrite it or not depending on the overwrite flag.
        /// </summary>
        /// <param name="key">Key to register the resource for.</param>
        /// <param name="resource">The resource to add.</param>
        /// <param name="overwrite">Overwrite flag, true to overwrite resources with same key and path.</param>
        /// <returns>True if the given resource has been added to the set.</returns>
        public bool AddResource(string key, LocalizationResource resource, bool overwrite)
        {
            var resourceForPath = resource.ForPath ?? String.Empty;

            if (this.Resources.TryGetValue(key, out var list))
            {
                // Key found, check whether the list already contains a resource for the same path,
                // else add item in right position in list (such that list is ordered by length of paths, longest first):
                for (int i = 0; i < list.Count; i++)
                {
                    var listItem = list[i];
                    var listItemForPath = listItem.ForPath ?? String.Empty;

                    if (listItemForPath.Equals(resourceForPath))
                    {
                        if (overwrite)
                        {
                            list[i] = resource;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (resourceForPath.StartsWith(listItemForPath))
                    {
                        list.Insert(i, resource);
                        return true;
                    }
                }
                list.Add(resource);
                return true;
            }
            else
            { 
                // No such key yet, just add a new list for this key and add the resource to it:
                list = this.Resources[key] = new List<LocalizationResource>();
                list.Add(resource);
                return true;
            }
        }
    }
}
