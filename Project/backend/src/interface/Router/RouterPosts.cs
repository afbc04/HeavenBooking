using Interface;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Interface {

    public class RouterPosts {

        public static RouterPacket PostRequests(IFacade model, HttpListenerRequest request, string url, string[] parameters) {

            if (request.HasEntityBody == false)
                return new RouterPacket(400);
            

            PacketBody body = new PacketBody(PacketBody.GetBody(request));

            if (RouterRegex.Users.IsMatch(url))
                return model.AddUser(body.GetString("id"),
                                     body.GetString("name"),
                                     body.GetString("email"),
                                     body.GetString("phone_number"),
                                     body.GetString("birth_date"),
                                     body.GetString("sex"),
                                     body.GetString("passport"),
                                     body.GetString("country_code"),
                                     body.GetString("address"),
                                     body.GetString("account_creation"),
                                     body.GetString("pay_method"),
                                     body.GetString("account_status"));

            

            else if (RouterRegex.Reservations.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.Flights.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.Hotels.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.Airports.IsMatch(url))
            {
                return new RouterPacket(404);
            }

            else if (RouterRegex.UsersBatch.IsMatch(url))
                return model.ImportUsers(body.GetString("path"));
            
            else if (RouterRegex.ReservationsBatch.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.HotelsBatch.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.FlightsBatch.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.AirportsBatch.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.PassengersBatch.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else
            {
                return new RouterPacket(404);
            }

        }
/*
        public static string UserBatch(IFacade model, HttpListenerRequest request, HttpListenerResponse response, string url, string[] parameters)
        {

            if (request.HasEntityBody == false)
                throw new RouterException(400);

            using StreamReader reader = new(request.InputStream, request.ContentEncoding);
            string json_string = reader.ReadToEnd();

            try
            {
                JsonElement JSON = JsonSerializer.Deserialize<JsonElement>(json_string);

                string? user_path = JSON.TryGetProperty("path", out JsonElement json_user_path) && json_user_path.ValueKind == JsonValueKind.String ? json_user_path.GetString() : null;
                return model.ImportUsers(user_path);

            }
            catch (JsonException) { throw new RouterException(400); }

        }*/
        
        
       
    }

}