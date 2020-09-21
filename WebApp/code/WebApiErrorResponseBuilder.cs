using MacroContext.ApplicationServices.InternalExceptions;
using MacroContext.Contract.Errors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace WebApp.code
{
    public static class WebApiErrorResponseBuilder
    {
        public static HttpResponseMessage CreateErrorResponseOrNull(Exception thrownException,
            HttpRequestMessage request)
        {
            if (thrownException is JsonSerializationException)
            {
                // Return when the supplied model (command or query) can't be deserialized.
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, thrownException.Message);
            }

            if (thrownException is KeyNotFoundException)
            {
                // Return when the requested resource does not exist anymore. Catching a KeyNotFoundException 
                // is an example, but you probably shouldn't throw KeyNotFoundException in this case, since it
                // could be thrown for other reasons (such as program errors) in which case this branch should
                // of course not execute.
                return request.CreateErrorResponse(HttpStatusCode.NotFound, thrownException);
            }

            if(thrownException is EntityAlreadyExistsException)
            {
                //return request.CreateErrorResponse(HttpStatusCode.Conflict,);
                var e = (EntityAlreadyExistsException)thrownException;
                var errorData = new EntitiesAlreadyExistsError(e.EntityIds);
                var response = request.CreateResponse<EntitiesAlreadyExistsError>(HttpStatusCode.Conflict, errorData);
                return response;
            }

            // If the thrown exception can't be handled: return null.
            return null;
        }

        public static string GetValueOrNull(this HttpRequestHeaders headers, string name)
        {
            IEnumerable<string> values;
            return headers.TryGetValues(name, out values) ? values.FirstOrDefault() : null;
        }
    }

}