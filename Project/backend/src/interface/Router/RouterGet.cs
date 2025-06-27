using Interface;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Interface {

    public class RouterGet {

        public static RouterPacket GetRequests(IFacade model, HttpListenerRequest request, string url, string[] parameters) {

            if (RouterRegex.UsersById.IsMatch(url))
                return model.GetUser(WebUtility.UrlDecode(parameters[1]));
            
            else if (RouterRegex.Users.IsMatch(url)) {
                int? page = QueryString.GetInteger(request,"page");
                return model.GetUsers(page);
            }

            else if (RouterRegex.ReservationsById.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.Reservations.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.FlightsById.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.Flights.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.HotelsById.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.Hotels.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.AirportsById.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.Airports.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.ReservationsOfUser.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.FlightsOfUser.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.FlightsAndReservationsOfUser.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.RatingHotel.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.AllRatingsHotel.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.ReservationsOfHotel.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.FlightsOfAirport.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.TopFlightsAirport.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.TopPassengersAirportYear.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.TopDelayAirport.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.TopRatingHotels.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.TopReservationsHotels.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.TopRevenueHotels.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.RevenueHotel.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.PrefixUsers.IsMatch(url)) {
                int? page = QueryString.GetInteger(request,"page");
                return model.GetUsersPrefix(WebUtility.UrlDecode(parameters[1]),page);
            }

            else if (RouterRegex.MetricsYears.IsMatch(url))
                return model.GetMetrics(null,null);

            else if (RouterRegex.MetricsMonths.IsMatch(url))
                return model.GetMetrics(int.Parse(parameters[1]),null);

            else if (RouterRegex.MetricsDays.IsMatch(url))
                return model.GetMetrics(int.Parse(parameters[1]),int.Parse(parameters[2]));

            else if (RouterRegex.GlobalInformation.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.RevenueAllTimeHotel.IsMatch(url))
            {
                return new RouterPacket(404);
            }
            else if (RouterRegex.TopPassengersAirport.IsMatch(url))
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