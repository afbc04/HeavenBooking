using Interface;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Interface {

    public class RouterDelete {

        public static RouterPacket DeleteRequests(IFacade model, HttpListenerRequest request, string url, string[] parameters) {

            if (RouterRegex.UsersById.IsMatch(url))
                return model.DeleteUser(parameters[1]);

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