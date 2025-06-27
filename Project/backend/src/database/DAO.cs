using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Business;
using System.Collections;

namespace DataBase {

    public class DAO : IDAO {

        private static DAO? instance;
        private DAO() { }

        /// <summary>
        /// Starts the DAO
        /// </summary>
        /// <returns></returns>
        public static bool InitDataBase() {

            try {

                instance = new DAO();

                using MySqlConnection conn = CreateConnection();
                conn.Open();

                InitUsers(conn);

                return true;

            } 
            catch (MySqlException) { return false; }

        }

        /// <summary>
        /// Starts the tables of Users
        /// </summary>
        /// <param name="conn"></param>
        private static void InitUsers(MySqlConnection conn) {

            using MySqlCommand cmd_init_database = new(DAOConfig.UserTable, conn);
            cmd_init_database.ExecuteNonQuery();

            AddIndexIfNotExists(conn, "Users", "index_name", DAOConfig.UserIndexName);
            AddIndexIfNotExists(conn, "Users", "index_account_creation", DAOConfig.UserIndexAccountCreation);

        }

        /// <summary>
        /// Get a instance of the DAO
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static DAO GetInstance() {
            return instance!;
        }

        /// <summary>
        /// Auxiliar method to add a index
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="Table"></param>
        /// <param name="Index"></param>
        /// <param name="Query"></param>
        private static void AddIndexIfNotExists(MySqlConnection conn, string Table, string Index, string Query) {

            string check_cmd_string = @"SELECT COUNT(1)
                                FROM INFORMATION_SCHEMA.STATISTICS
                                WHERE TABLE_SCHEMA = 'HeavenBooking'
                                AND table_name = @table
                                AND index_name = @index";

            using MySqlCommand check_cmd = new(check_cmd_string, conn);
            
            check_cmd.Parameters.AddWithValue("@table", Table);
            check_cmd.Parameters.AddWithValue("@index", Index);

            var exists = Convert.ToInt32(check_cmd.ExecuteScalar());

            if (exists == 0) {
                
                using MySqlCommand cmd = new(Query, conn);
                cmd.ExecuteNonQuery();
                    
            }
        }

        /// <summary>
        /// Creates a conection of DAO
        /// </summary>
        /// <returns></returns>
        private static MySqlConnection CreateConnection() {
            return new MySqlConnection(DAOConfig.ConnectionString);
        }

        public bool ImportUsers(IList<UserBatch> list_of_users)
        {
            using var conn = CreateConnection();
            return DAOUser.ImportUsers(conn,list_of_users);
        }

        public bool PutUser(UserBatch user)
        {
            using var conn = CreateConnection();
            return DAOUser.PutUser(conn,user);
        }

        public bool DeleteUser(string ID)
        {
            using var conn = CreateConnection();
            return DAOUser.DeleteUserbyID(conn,ID);
        }


        public UserComplete GetUserByID(string ID)
        {
            using var conn = CreateConnection();
            return DAOUser.GetUserbyID(conn, ID);
        }

        public ISet<string> GetUsersIDs()
        {
            using var conn = CreateConnection();
            return DAOUser.GetUsersIDs(conn);
        }

        public IList<UserList> GetUsers(int offset, int limit)
        {
            using var conn = CreateConnection();
            return DAOUser.GetUsers(conn,offset,limit);
        }

        public IList<UserPrefix> GetUsersPrefix(string prefix, int offset, int limit)
        {
            using var conn = CreateConnection();
            return DAOUser.GetUsersPrefix(conn,offset,limit,prefix);
        }

        public IDictionary<int,int> GetUsersMetricsYears() 
        {
            using var conn = CreateConnection();
            return DAOUser.GetUsersMetricsYears(conn);
        }

        public IDictionary<int,int> GetUsersMetricsMonths(int year)
        {
            using var conn = CreateConnection();
            return DAOUser.GetUsersMetricsMonths(conn,year);
        }

        public IDictionary<int,int> GetUsersMetricsDays(int year, int month)
        {
            using var conn = CreateConnection();
            return DAOUser.GetUsersMetricsDays(conn,year,month);
        }

        public int GetUsersCount()
        {
            using var conn = CreateConnection();
            return DAOUser.GetUsersCount(conn);
        }

        public int GetUsersActiveCount()
        {
            using var conn = CreateConnection();
            return DAOUser.GetUsersActiveCount(conn);
        }

    }
}