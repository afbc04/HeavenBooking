using Business;

namespace DataBase
{
    public interface IDAO
    {

        bool ImportUsers();
        ISet<string> GetUsersIDs();
        User GetUserByID(string ID);
        bool AddUser(string ID, string Name, string Email, string PhoneNumber, string BirthDate, string Sex, string Passport, string CountryCode, string Address, string AccountCreation, string PayMethod, string AccountStatus);
        bool DeleteUser(string ID);

    }
}