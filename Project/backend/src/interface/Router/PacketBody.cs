using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Interface {

    public class PacketBody {

        private JsonElement? JSON;
        
        public PacketBody(string JSON) {

            try {
                this.JSON = JsonSerializer.Deserialize<JsonElement>(JSON);
            } catch (Exception) {
                this.JSON = null;
            }

        }

        /// <summary>
        /// Gets the string of a packet body
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public string? GetString(string field) {

            if (this.JSON == null)
                return null;

            return (this.JSON.Value.TryGetProperty(field, out JsonElement json_body) && json_body.ValueKind == JsonValueKind.String) ? json_body.GetString() : null;

        }

        /// <summary>
        /// Gets the bool of a packet body
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool? GetBool(string field) {

            if (this.JSON == null)
                return null;

            return (this.JSON.Value.TryGetProperty(field, out JsonElement json_body) && (json_body.ValueKind == JsonValueKind.True || json_body.ValueKind == JsonValueKind.False)) ? json_body.GetBoolean() : null;

        }

        /// <summary>
        /// Gets the integer of a packet body
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public int? GetInteger(string field) {

            if (this.JSON == null)
                return null;

            if (this.JSON.Value.TryGetProperty(field, out JsonElement json_body) && json_body.ValueKind == JsonValueKind.Number) {

                if (json_body.TryGetInt32(out int result))
                    return result;

            }

            return null;
        }

        /// <summary>
        /// Gets the double of a packet body
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public double? GetDouble(string field) {

            if (this.JSON == null)
                return null;

            if (this.JSON.Value.TryGetProperty(field, out JsonElement json_body) && json_body.ValueKind == JsonValueKind.Number) {

                if (json_body.TryGetDouble(out double result))
                    return result;
                    
            }

            return null;
        }

        /// <summary>
        /// Get the body of a request
        /// </summary>
        /// <param name="request"></param>
        /// <returns>String JSON of the body</returns>
        public static string GetBody(HttpListenerRequest request) {

            StreamReader reader = new(request.InputStream, request.ContentEncoding);
            string json_string = reader.ReadToEnd();
            reader.Dispose();

            return json_string;
        }

    }

}