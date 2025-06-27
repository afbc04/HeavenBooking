using Interface;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Interface {

    public class RouterPuts {

        public static RouterPacket PutRequests(IFacade model, HttpListenerRequest request, string url, string[] parameters) {

            if (request.HasEntityBody == false)
                return new RouterPacket(400);
            

            PacketBody body = new PacketBody(PacketBody.GetBody(request));

            if (RouterRegex.UsersById.IsMatch(url))
                return model.UpdateUser(parameters[1],
                                        body.GetString("name"),
                                        body.GetString("email"),
                                        body.GetString("birth_date"),
                                        body.GetString("sex"),
                                        body.GetString("passport"),
                                        body.GetString("country_code"),
                                        body.GetString("account_status"));

            
            else if (RouterRegex.ReservationsById.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.HotelsById.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.FlightsById.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.AirportsById.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else
            {
                return new RouterPacket(404);
            }

        }
       
    }

}