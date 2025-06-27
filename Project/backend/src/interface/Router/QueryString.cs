using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Interface {

    public class QueryString {

        /// <summary>
        /// Gets the string of a query string
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string? GetString(HttpListenerRequest request,string field) {

            string? value = request.QueryString[field];
            return string.IsNullOrWhiteSpace(value) == true ? null : value;

        }

        /// <summary>
        /// Gets the bool of a query string
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool? GetBool(HttpListenerRequest request, string field) {

            string? value = request.QueryString[field];

            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (bool.TryParse(value,out bool value_bool))
                return value_bool;

            return null;

        }

        /// <summary>
        /// Gets the integer of a query string
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static int? GetInteger(HttpListenerRequest request, string field) {

            string? value = request.QueryString[field];

            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (int.TryParse(value,out int value_int))
                return value_int;

            return null;

        }

        /// <summary>
        /// Gets the double of a query string
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static double? GetDouble(HttpListenerRequest request, string field) {

            string? value = request.QueryString[field];

            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (double.TryParse(value,out double value_double))
                return value_double;

            return null;

        }

    }

}