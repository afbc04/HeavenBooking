using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Business;
using System.Collections;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace DataBase
{
    public class DAOUser
    {

        /*
        static bool DoesUserExists(MySqlConnection conn, string ID)
        {


            //Users
            using MySqlCommand cmd_init_database = new(DAOConfig.UserTable, conn);
            cmd_init_database.ExecuteNonQuery();

            AddIndexIfNotExists(conn, "Users", "index_name", DAOConfig.UserIndexName);
            AddIndexIfNotExists(conn, "Users", "index_account_creation", DAOConfig.UserIndexAccountCreation);

        }*/

        static public bool ImportUsers(MySqlConnection conn)
        {
            conn.Open();

            try
            {
                string path = Path.GetFullPath("datasets/users.csv");
                Console.WriteLine($"Path is {path}");
                string string_query = $@"
                    LOAD DATA LOCAL INFILE '{path.Replace("\\", "\\\\")}'
                    INTO TABLE Users
                    FIELDS TERMINATED BY ';'
                    ENCLOSED BY '""'
                    LINES TERMINATED BY '\n'
                    (id, user_name, birth_date, sex, passport, country_code, account_creation, account_status,reservations,reservations_refunded,flights,flights_arrived,flights_lost,total_spent);
                ";

                using MySqlCommand cmd = new MySqlCommand(string_query, conn);
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();

                return true;
            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public static bool AddUser(MySqlConnection conn, string ID, string Name, string Email, string PhoneNumber, string BirthDate, string Sex, string Passport, string CountryCode, string Address, string AccountCreation, string PayMethod, string AccountStatus)
        {
            conn.Open();

            using MySqlCommand cmd = new(@"
                INSERT INTO Users (id, user_name, birth_date, sex, passport, country_code, account_creation, account_status, reservations, reservations_refunded, flights, flights_arrived, flights_lost, total_spent)
                VALUES (@id, @name, @birth_date, @sex, @passport, @country_code, @account_creation, @account_status,0,0,0,0,0,0)", conn);

            int sex = 2;
            if (Regex.IsMatch(Sex, "F", RegexOptions.IgnoreCase)) sex = 1;
            if (Regex.IsMatch(Sex, "M", RegexOptions.IgnoreCase)) sex = 0;

            int active = 1;
            if (Regex.IsMatch(AccountStatus, "inactive", RegexOptions.IgnoreCase)) active = 0;

            cmd.Parameters.AddWithValue("@id", ID);
            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@birth_date", BirthDate.Replace("/","-"));
            cmd.Parameters.AddWithValue("@sex", sex);
            cmd.Parameters.AddWithValue("@passport", Passport);
            cmd.Parameters.AddWithValue("@country_code", CountryCode);
            cmd.Parameters.AddWithValue("@account_creation", AccountCreation.Split(" ")[0].Replace("/","-"));
            cmd.Parameters.AddWithValue("@account_status", active);

            return cmd.ExecuteNonQuery() > 0;
    
        }

        public static bool DeleteUserbyID(MySqlConnection conn, string ID)
        {
            conn.Open();
            using MySqlCommand cmd = new("DELETE FROM Users WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", ID);

            return cmd.ExecuteNonQuery() > 0;

        }

        public static User GetUserbyID(MySqlConnection conn, string ID)
        {
            conn.Open();
            using MySqlCommand cmd = new("SELECT * FROM Users WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", ID);
            var reader = cmd.ExecuteReader();
            reader.Read();

            string user_Name = reader.GetString("user_name");

            DateTime user_BirthDate = reader.GetDateTime("birth_date");
            DateTime TodaysDate = DateTime.Now;

            int user_Age = TodaysDate.Year - user_BirthDate.Year;
            if (TodaysDate.Month <= user_BirthDate.Month && TodaysDate.Day < user_BirthDate.Day)
                user_Age--;

            short user_Sex = reader.GetInt16("sex");
            string user_Passport = reader.GetString("passport");
            string user_CountryCode = reader.GetString("country_code");
            bool user_AccountStatus = reader.GetBoolean("account_status");

            int user_Reservations = reader.GetInt32("reservations");
            int user_ReservationsRefunded = reader.GetInt32("reservations_refunded");
            int user_Flights = reader.GetInt32("flights");
            int user_FlightsArrived = reader.GetInt32("flights_arrived");
            int user_FlightsLost = reader.GetInt32("flights_lost");
            double user_TotalSpent = reader.GetDouble("total_spent");

            return new User(ID, user_Name, user_Sex, user_Age, user_CountryCode, user_Passport, user_Reservations, user_ReservationsRefunded, user_Flights, user_FlightsArrived, user_FlightsLost, user_TotalSpent, user_AccountStatus);

        }

        public static ISet<string> GetUsersIDs(MySqlConnection conn)
        {
            conn.Open();
            using MySqlCommand cmd = new("SELECT id FROM Users", conn);
            cmd.CommandTimeout = 0;
            var reader = cmd.ExecuteReader();

            ISet<string> sets = new HashSet<string>();

            while (reader.Read())
                sets.Add(reader.GetString("id"));


            return sets;

        }

        public static int GetUsersCount(MySqlConnection conn)
        {
            conn.Open();
            using MySqlCommand cmd = new("SELECT COUNT(*) FROM Users", conn);
            var count = cmd.ExecuteScalar();
            return Convert.ToInt32(count);

        }

        public static int GetUsersActiveCount(MySqlConnection conn)
        {
            conn.Open();
            using MySqlCommand cmd = new("SELECT COUNT(*) FROM Users WHERE account_status = TRUE", conn);
            var count = cmd.ExecuteScalar();
            return Convert.ToInt32(count);

        }

    }
}