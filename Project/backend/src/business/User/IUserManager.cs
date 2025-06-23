namespace Business
{

    public interface IUserManager
    {
        (bool, string, int, int) ImportFile(string csv_path);
        User? GetUser(string ID);
        (bool, string?) AddUser(string ID, string Name, string Email, string PhoneNumber, string BirthDate, string Sex, string Passport, string CountryCode, string Address, string AccountCreation, string PayMethod, string AccountStatus);
        bool DeleteUser(string ID);

    }

}