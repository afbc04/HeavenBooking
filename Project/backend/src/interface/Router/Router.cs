using Interface;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Interface
{
    public class Router : IRouter
    {

        private IFacade model;

        public Router()
        {
            this.model = new Facade();
        }

        //Routes of GET Requests
        public string GetRequests(HttpListenerRequest request, HttpListenerResponse response, string url, string[] parameters)
        {

            response.StatusCode = 200;

            if (RouterRegex.UsersById.IsMatch(url))
            {
                Console.WriteLine($"GET {parameters[1]}");
                return this.model.GetUser(parameters[1]);
            }
            else if (RouterRegex.Users.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.ReservationsById.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.Reservations.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.FlightsById.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.Flights.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.HotelsById.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.Hotels.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.AirportsById.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.Airports.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.ReservationsOfUser.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.FlightsOfUser.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.FlightsAndReservationsOfUser.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.RatingHotel.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.AllRatingsHotel.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.ReservationsOfHotel.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.FlightsOfAirport.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.TopFlightsAirport.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.TopPassengersAirportYear.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.TopDelayAirport.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.TopRatingHotels.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.TopReservationsHotels.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.TopRevenueHotels.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.RevenueHotel.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.PrefixUsers.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.MetricsAll.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.MetricsYear.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.MetricsMonth.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.GlobalInformation.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.RevenueAllTimeHotel.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.TopPassengersAirport.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else
            {
                throw new RouterException(404);
            }

        }
        
        //Routes of POST Requests
        public string PostRequests(HttpListenerRequest request, HttpListenerResponse response, string url, string[] parameters)
        {

            response.StatusCode = 201;

            if (RouterRegex.Users.IsMatch(url))
            {
                return RouterPosts.AddUser(model, request, response, url, parameters);
            }
            else if (RouterRegex.Reservations.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.Flights.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.Hotels.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.Airports.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.UsersBatch.IsMatch(url))
            {
                return RouterPosts.UserBatch(this.model, request, response, url, parameters);
            }
            else if (RouterRegex.ReservationsBatch.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.HotelsBatch.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.FlightsBatch.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.AirportsBatch.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.PassengersBatch.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else
            {
                throw new RouterException(404);
            }

        }
        
        //Routes of PUT Requests
        public string PutRequests(HttpListenerRequest request, HttpListenerResponse response, string url, string[] parameters)
        {

            if (RouterRegex.UsersById.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.ReservationsById.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.HotelsById.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.FlightsById.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.AirportsById.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else
            {
                throw new RouterException(404);
            }

        }

        //Routes of DELETE Requests
        public string DeleteRequests(HttpListenerRequest request, HttpListenerResponse response, string url, string[] parameters)
        {

            if (RouterRegex.UsersById.IsMatch(url))
            {
                return this.model.DeleteUser(parameters[1]);
            }
            else if (RouterRegex.ReservationsById.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.HotelsById.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.FlightsById.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else if (RouterRegex.AirportsById.IsMatch(url))
            {
                throw new RouterException(404);
            }
            else
            {
                throw new RouterException(404);
            }

        }

        public void ErrorRequest(HttpListenerResponse response, int status_code)
        {
            response.StatusCode = status_code;
        }

    }


}