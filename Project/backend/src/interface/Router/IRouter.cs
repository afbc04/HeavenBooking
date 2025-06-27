using System.Net;

namespace Interface {

    public interface IRouter {
        /// <summary> 
        /// Reads a packet and processes it
        /// </summary>
        /// <returns>Router Packet to be send</returns>
        /// <exception cref="Exception">Some accumulated exception</exception>
        RouterPacket ReadPacket(HttpListenerRequest request);

        /// <summary> 
        /// Send the packet given to client
        /// </summary>
        /// <returns></returns>
        /// <exception></exception>
        void WritePacket(HttpListenerResponse response, RouterPacket packet);

    }

}