using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet
{
    /// <summary>
    /// An exception that returns a specific HTTP status code.
    /// </summary>
    public class HttpResponseException : Exception
    {
        /// <summary>
        /// Creates a new HttpResponseException.
        /// </summary>
        /// <param name="statusCode">The HTTP status code to return as HTTP response.</param>
        public HttpResponseException(HttpStatusCode statusCode)
            : this(statusCode, statusCode.ToString(), null)
        { }

        /// <summary>
        /// Creates a new HttpResponseException.
        /// </summary>
        /// <param name="statusCode">The HTTP status code to return as HTTP response.</param>
        /// <param name="message">The exception message also returned as HTTP status text.</param>
        public HttpResponseException(HttpStatusCode statusCode, string? message)
            : this(statusCode, message, null)
        { }

        /// <summary>
        /// Creates a new HttpResponseException.
        /// </summary>
        /// <param name="statusCode">The HTTP status code to return as HTTP response.</param>
        /// <param name="message">The exception message also returned as HTTP status text.</param>
        /// <param name="innerException">Inner exception.</param>
        public HttpResponseException(HttpStatusCode statusCode, string? message, Exception? innerException)
            : base(message, innerException)
        {
            this.Data["StatusCode"] = statusCode;
        }

        /// <inheritdoc/>
        public HttpResponseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// HTTP status code returned by this exception.
        /// </summary>
        public HttpStatusCode StatusCode => (HttpStatusCode)(Data["StatusCode"] ?? HttpStatusCode.InternalServerError);

        /// <inheritdoc/>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
