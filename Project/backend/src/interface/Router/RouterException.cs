using Interface;


namespace Interface
{

    public class RouterException : Exception
    {

        public readonly int error_code;

        public RouterException(int error_code) : base()
        {

            this.error_code = error_code;

        }

    }

}