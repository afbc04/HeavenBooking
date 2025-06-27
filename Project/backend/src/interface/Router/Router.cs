using Interface;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Interface {

    public class Router : IRouter {

        private IFacade _model;

        /// <exception cref="FacadeDataBaseException">Database wasnt loaded successfully</exception>
        public Router() {
            this._model = new Facade();
        }

        public RouterPacket ReadPacket(HttpListenerRequest request) {

            if (request.Url == null)
                return new RouterPacket(400);

            string[] segments_of_path = request.Url.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (segments_of_path.Length == 0)
                segments_of_path = [""];

            RouterPacket packet = request.HttpMethod switch
            {
                "GET" => RouterGet.GetRequests(this._model,request, request.Url.AbsolutePath, segments_of_path),
                "POST" => RouterPosts.PostRequests(this._model,request, request.Url.AbsolutePath, segments_of_path),
                "PUT" => RouterPuts.PutRequests(this._model,request, request.Url.AbsolutePath, segments_of_path),
                "DELETE" => RouterDelete.DeleteRequests(this._model,request, request.Url.AbsolutePath, segments_of_path),
                "OPTIONS" => this.OptionsRequests(request),
                _ => new RouterPacket(501),
            };

            return packet;

        }

        public void WritePacket(HttpListenerResponse response, RouterPacket packet) {

            try {

                response.StatusCode = packet.StatusCode;

                if (packet.JSON != null) {

                    byte[] buffer = Encoding.UTF8.GetBytes(packet.JSON);
                    response.ContentLength64 = buffer.Length;
                    response.ContentType = "application/json";

                    response.OutputStream.Write(buffer, 0, buffer.Length);

                }

                response.Close();
            
            }
            catch (Exception) {}

        }

        private RouterPacket OptionsRequests(HttpListenerRequest request) {
            return new RouterPacket(200);
        }

    }


}