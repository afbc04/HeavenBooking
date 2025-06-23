using System.Net;

namespace Interface
{

    public interface IRouter
    {
        string GetRequests(HttpListenerRequest request, HttpListenerResponse response, string url, string[] parameters);
        string PostRequests(HttpListenerRequest request, HttpListenerResponse response, string url, string[] parameters);
        string PutRequests(HttpListenerRequest request, HttpListenerResponse response, string url, string[] parameters);
        string DeleteRequests(HttpListenerRequest request, HttpListenerResponse response, string url, string[] parameters);
        void ErrorRequest(HttpListenerResponse response, int status_code);

    }

}