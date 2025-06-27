using Interface;


namespace Interface
{

    public class FacadeException : Exception {

        public FacadeException() : base() {}
        public FacadeException(string Message) : base(Message) {}

    }

}