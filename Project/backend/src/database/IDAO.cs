using Business;

namespace DataBase
{
    public interface IDAO
    {

        bool ImportUsers(IList<UserBatch> ListOfUsers);
        ISet<string> GetUsersIDs();
        UserComplete GetUserByID(string ID);
        bool PutUser(UserBatch user);
        bool DeleteUser(string ID);
        IList<UserList> GetUsers(int offset, int limit);
        IList<UserPrefix> GetUsersPrefix(string prefix, int offset, int limit);
        IDictionary<int,int> GetUsersMetricsYears();
        IDictionary<int,int> GetUsersMetricsMonths(int year);
        IDictionary<int,int> GetUsersMetricsDays(int year, int month);

    }
}