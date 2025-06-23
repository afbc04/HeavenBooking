using Interface;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Interface
{
    public class RouterPuts
    {

        public static string UpdateUser(IFacade model, HttpListenerRequest request, HttpListenerResponse response, string url, string[] parameters)
        {

            if (request.HasEntityBody == false)
                throw new RouterException(400);

            using StreamReader reader = new(request.InputStream, request.ContentEncoding);
            string json_string = reader.ReadToEnd();

            try
            {
                JsonElement JSON = JsonSerializer.Deserialize<JsonElement>(json_string);

                string? user_name = JSON.TryGetProperty("name", out JsonElement json_user_name) && json_user_name.ValueKind == JsonValueKind.String ? json_user_name.GetString() : null;
                string? user_email = JSON.TryGetProperty("email", out JsonElement json_user_email) && json_user_email.ValueKind == JsonValueKind.String ? json_user_email.GetString() : null;
                string? user_phone_number = JSON.TryGetProperty("phone_number", out JsonElement json_user_phone_number) && json_user_phone_number.ValueKind == JsonValueKind.String ? json_user_phone_number.GetString() : null;
                string? user_birth_date = JSON.TryGetProperty("birth_date", out JsonElement json_user_birth_date) && json_user_birth_date.ValueKind == JsonValueKind.String ? json_user_birth_date.GetString() : null;
                string? user_sex = JSON.TryGetProperty("sex", out JsonElement json_user_sex) && json_user_sex.ValueKind == JsonValueKind.String ? json_user_sex.GetString() : null;
                string? user_passport = JSON.TryGetProperty("passport", out JsonElement json_user_passport) && json_user_passport.ValueKind == JsonValueKind.String ? json_user_passport.GetString() : null;
                string? user_country_code = JSON.TryGetProperty("country_code", out JsonElement json_user_country_code) && json_user_country_code.ValueKind == JsonValueKind.String ? json_user_country_code.GetString() : null;
                string? user_address = JSON.TryGetProperty("address", out JsonElement json_user_address) && json_user_address.ValueKind == JsonValueKind.String ? json_user_address.GetString() : null;
                string? user_account_creation = JSON.TryGetProperty("account_creation", out JsonElement json_user_account_creation) && json_user_account_creation.ValueKind == JsonValueKind.String ? json_user_account_creation.GetString() : null;
                string? user_pay_method = JSON.TryGetProperty("pay_method", out JsonElement json_user_pay_method) && json_user_pay_method.ValueKind == JsonValueKind.String ? json_user_pay_method.GetString() : null;
                string? user_account_status = JSON.TryGetProperty("account_status", out JsonElement json_user_account_status) && json_user_account_status.ValueKind == JsonValueKind.String ? json_user_account_status.GetString() : null;

                (bool updated, string json_response) = model.UpdateUser(parameters[1], user_name, user_email, user_phone_number, user_birth_date, user_sex, user_passport, user_country_code, user_address, user_account_creation, user_pay_method, user_account_status);

                if (updated == false)
                    response.StatusCode = 422;

                return json_response;

            }
            catch (JsonException) { throw new RouterException(400); }
            
        }
       
    }

}