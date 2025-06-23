using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Business;
using System.Collections;

namespace DataBase
{
    public class DAO : IDAO
    {
        private static DAO? instance;
        private DAO() { }

        //Method which starts the database and says if the database was sucessfully connected
        public static bool InitDataBase()
        {

            try
            {
                instance = new DAO();

                using MySqlConnection conn = CreateConnection();
                conn.Open();

                //Users
                using MySqlCommand cmd_init_database = new(DAOConfig.UserTable, conn);
                cmd_init_database.ExecuteNonQuery();

                AddIndexIfNotExists(conn, "Users", "index_name", DAOConfig.UserIndexName);
                AddIndexIfNotExists(conn, "Users", "index_account_creation", DAOConfig.UserIndexAccountCreation);

                return true;

            }
            catch (MySqlException)
            {
                return false;
            }

        }

        //Method which gives the instance to connection of DataBase
        public static DAO GetInstance()
        {

            if (instance == null)
                throw new InvalidOperationException("DAO not initialized");

            return instance;
        }

        private static void AddIndexIfNotExists(MySqlConnection conn, string Table, string Index, string Query)
        {
            string check_cmd_string = @"SELECT COUNT(1)
                                FROM INFORMATION_SCHEMA.STATISTICS
                                WHERE TABLE_SCHEMA = 'HeavenBooking'
                                AND table_name = @table
                                AND index_name = @index";

            using (MySqlCommand check_cmd = new(check_cmd_string, conn))
            {
                check_cmd.Parameters.AddWithValue("@table", Table);
                check_cmd.Parameters.AddWithValue("@index", Index);

                var exists = Convert.ToInt32(check_cmd.ExecuteScalar());

                if (exists == 0)
                {
                    using (MySqlCommand cmd = new(Query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private static MySqlConnection CreateConnection()
        {
            return new MySqlConnection(DAOConfig.ConnectionString);
        }

        public bool ImportUsers()
        {
            using var conn = new MySqlConnection(DAOConfig.ConnectionString);
            return DAOUser.ImportUsers(conn);
        }

        public bool AddUser(string ID, string Name, string Email, string PhoneNumber, string BirthDate, string Sex, string Passport, string CountryCode, string Address, string AccountCreation, string PayMethod, string AccountStatus)
        {
            using var conn = new MySqlConnection(DAOConfig.ConnectionString);
            return DAOUser.AddUser(conn,ID,Name,Email,PhoneNumber,BirthDate,Sex,Passport,CountryCode,Address,AccountCreation,PayMethod,AccountStatus);
        }

        public bool DeleteUser(string ID)
        {
            using var conn = new MySqlConnection(DAOConfig.ConnectionString);
            return DAOUser.DeleteUserbyID(conn,ID);
        }


        public User GetUserByID(string ID)
        {
            using var conn = new MySqlConnection(DAOConfig.ConnectionString);
            return DAOUser.GetUserbyID(conn, ID);
        }

        public ISet<string> GetUsersIDs()
        {
            using var conn = new MySqlConnection(DAOConfig.ConnectionString);
            return DAOUser.GetUsersIDs(conn);
        }

        public int GetUsersCount()
        {
            using var conn = new MySqlConnection(DAOConfig.ConnectionString);
            return DAOUser.GetUsersCount(conn);
        }

        public int GetUsersActiveCount()
        {
            using var conn = new MySqlConnection(DAOConfig.ConnectionString);
            return DAOUser.GetUsersActiveCount(conn);
        }

        /*
                private void CloseConnection()
                {
                    if (this.conn.State != System.Data.ConnectionState.Closed)
                        this.conn.Close();
                }

                public User this[string ID]
                {
                    get
                    {
                        throw new KeyNotFoundException();
                    }
                    set
                    {
                        throw new Exception();
                    }
                }

                public ICollection<string> Keys
                {
                    get
                    {
                        return [];
                    }
                }

                public ICollection<User> Values
                {
                    get
                    {
                        return [];
                    }
                }

                public int Count => 0;

                public bool IsReadOnly => false;


                public (bool, int, int) ImportCSVFile(string path)
                {
                    bool valid = false;
                    int success = 0;
                    int failed = 0;

                    if (File.Exists(path))
                    {

                        FileStream reader = File.Open(path, FileMode.Open);

                        reader.BeginRead();

                    }

                    return (valid, success, failed);
                }

                public void Add(string ID, User user)
                {
                    throw new Exception();
                }

                public bool ContainsKey(string ID)
                {
                    throw new Exception();
                }

                public bool Remove(string ID)
                {
                    throw new Exception();
                }

                public bool Remove(KeyValuePair<string, User> pair)
                {
                    throw new Exception();
                }

                public bool TryGetValue(string ID, out User user)
                {
                    throw new Exception();
                }

                public void Add(KeyValuePair<string, User> pair)
                {
                    throw new Exception();
                }

                public void Clear()
                {
                    throw new Exception();
                }

                public bool Contains(KeyValuePair<string, User> pair)
                {
                    throw new Exception();
                }

                public void CopyTo(KeyValuePair<string, User>[] array, int arrayIndex)
                {
                    throw new Exception();
                }

                public IEnumerator<KeyValuePair<string, User>> GetEnumerator()
                {
                    throw new NotImplementedException();
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return GetEnumerator();
                }


                /*
                    public List<User> GetAllUsers()
                    {
                        var users = new List<User>();

                        using (var connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            string sql = "SELECT id, name, email FROM users";

                            using (var cmd = new MySqlCommand(sql, connection))
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    users.Add(new User
                                    {
                                        Id = reader.GetInt32("id"),
                                        Name = reader.GetString("name"),
                                        Email = reader.GetString("email")
                                    });
                                }
                            }
                        }

                        return users;
                    }

                    public void AddUser(User user)
                    {
                        using (var connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            string sql = "INSERT INTO users (name, email) VALUES (@name, @email)";

                            using (var cmd = new MySqlCommand(sql, connection))
                            {
                                cmd.Parameters.AddWithValue("@name", user.Name);
                                cmd.Parameters.AddWithValue("@email", user.Email);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    public User? GetUserById(int id)
                    {
                        using (var connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            string sql = "SELECT id, name, email FROM users WHERE id = @id";

                            using (var cmd = new MySqlCommand(sql, connection))
                            {
                                cmd.Parameters.AddWithValue("@id", id);

                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        return new User
                                        {
                                            Id = reader.GetInt32("id"),
                                            Name = reader.GetString("name"),
                                            Email = reader.GetString("email")
                                        };
                                    }
                                }
                            }
                        }

                        return null;
                    }

                    public void DeleteUser(int id)
                    {
                        using (var connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            string sql = "DELETE FROM users WHERE id = @id";

                            using (var cmd = new MySqlCommand(sql, connection))
                            {
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }*/
    }
}