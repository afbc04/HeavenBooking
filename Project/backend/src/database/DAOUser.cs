using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Business;
using System.Collections;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace DataBase {
    public class DAOUser {

        public static bool ImportUsers(MySqlConnection conn, IList<UserBatch> list_of_users) {

            conn.Open();

            try {

                int size_of_batch = 1000;
                int users_to_be_batched = list_of_users.Count;
                int users_batched = 0;

                while (users_batched < users_to_be_batched) {

                    int count = Math.Min(size_of_batch, users_to_be_batched - users_batched);
                    var query = new System.Text.StringBuilder("INSERT INTO Users (id, user_name, email, birth_date, sex, passport, country_code, account_creation, account_status) VALUES ");
                    var list_of_users_to_be_inserted = new List<MySqlParameter>();

                    for (int i = 0; i < count; i++) {

                        UserBatch user = list_of_users[users_batched + i];
                        string parameter_id = $"@{i}_";

                        query.Append($"({parameter_id}id, {parameter_id}user_name, {parameter_id}email, {parameter_id}birth_date, {parameter_id}sex, {parameter_id}passport, {parameter_id}country_code, {parameter_id}account_creation, {parameter_id}account_status)");

                        if (i < count - 1)
                            query.Append(',');

                        list_of_users_to_be_inserted.Add(new MySqlParameter(parameter_id + "id", user.ID));
                        list_of_users_to_be_inserted.Add(new MySqlParameter(parameter_id + "user_name", user.Name));
                        list_of_users_to_be_inserted.Add(new MySqlParameter(parameter_id + "email", user.Email));
                        list_of_users_to_be_inserted.Add(new MySqlParameter(parameter_id + "birth_date", user.BirthDate));
                        list_of_users_to_be_inserted.Add(new MySqlParameter(parameter_id + "sex", user.Sex));
                        list_of_users_to_be_inserted.Add(new MySqlParameter(parameter_id + "passport", user.Passport));
                        list_of_users_to_be_inserted.Add(new MySqlParameter(parameter_id + "country_code", user.CountryCode));
                        list_of_users_to_be_inserted.Add(new MySqlParameter(parameter_id + "account_creation", user.AccountCreation));
                        list_of_users_to_be_inserted.Add(new MySqlParameter(parameter_id + "account_status", user.AccountStatus));
                    }

                    using var cmd = new MySqlCommand(query.ToString(), conn);
                    cmd.Parameters.AddRange(list_of_users_to_be_inserted.ToArray());
                    cmd.ExecuteNonQuery();

                    users_batched += count;
                }

                return true;
            }
            catch (Exception) {
                return false;
            }

        }

        public static bool PutUser(MySqlConnection conn, UserBatch user) {
            
            conn.Open();

            using MySqlCommand cmd = new(@"
                INSERT INTO Users (id, user_name, email, birth_date, sex, passport, country_code, account_creation, account_status)
                VALUES (@id, @name, @email, @birth_date, @sex, @passport, @country_code, @account_creation, @account_status)
                ON DUPLICATE KEY UPDATE
                    user_name = @name,
                    email = @email,
                    birth_date = @birth_date,
                    sex = @sex,
                    passport = @passport,
                    country_code = @country_code,
                    account_status = @account_status", conn);

            cmd.Parameters.AddWithValue("@id", user.ID);
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@birth_date", user.BirthDate);
            cmd.Parameters.AddWithValue("@sex", user.Sex);
            cmd.Parameters.AddWithValue("@passport", user.Passport);
            cmd.Parameters.AddWithValue("@country_code", user.CountryCode);
            cmd.Parameters.AddWithValue("@account_creation", user.AccountCreation);
            cmd.Parameters.AddWithValue("@account_status", user.AccountStatus);

            return cmd.ExecuteNonQuery() > 0;
    
        }
        public static bool DeleteUserbyID(MySqlConnection conn, string ID) {

            conn.Open();
            using MySqlCommand cmd = new("DELETE FROM Users WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", ID);

            return cmd.ExecuteNonQuery() > 0;

        }

        public static UserComplete GetUserbyID(MySqlConnection conn, string ID) {

            conn.Open();

            using MySqlCommand cmd = new("SELECT * FROM Users WHERE id = @id", conn);
            cmd.Parameters.AddWithValue("@id", ID);
            var reader = cmd.ExecuteReader();
            reader.Read();

            string user_Name = reader.GetString("user_name");
            string user_Email = reader.GetString("email");

            DateTime user_BirthDate = reader.GetDateTime("birth_date");
            DateTime TodaysDate = DateTime.Now;

            int user_Age = TodaysDate.Year - user_BirthDate.Year;
            if (TodaysDate.Month <= user_BirthDate.Month && TodaysDate.Day < user_BirthDate.Day)
                user_Age--;

            short user_Sex = reader.GetInt16("sex");
            string user_Passport = reader.GetString("passport");
            string user_CountryCode = reader.GetString("country_code");
            bool user_AccountStatus = reader.GetBoolean("account_status");
            DateTime user_AccountCreation = reader.GetDateTime("account_creation");

            return new UserComplete(ID, user_Name,user_Email, Dates.DateToString(user_BirthDate), user_Sex, user_Age, user_CountryCode, user_Passport, user_AccountStatus,Dates.DateToString(user_AccountCreation));

        }

        public static IList<UserList> GetUsers(MySqlConnection conn, int offset, int limit) {

            conn.Open();

            using MySqlCommand cmd = new("SELECT id, user_name, country_code, account_status FROM Users LIMIT @limit OFFSET @offset", conn);
            cmd.Parameters.AddWithValue("@limit", limit);
            cmd.Parameters.AddWithValue("@offset", offset * limit);
            var reader = cmd.ExecuteReader();
            
            IList<UserList> list_of_users = new List<UserList>();

            while (reader.Read()) {

                string user_ID = reader.GetString("id");
                string user_Name = reader.GetString("user_name");
                string user_CountryCode = reader.GetString("country_code");
                bool user_AccountStatus = reader.GetBoolean("account_status");

                list_of_users.Add(new UserList(user_ID, user_Name, user_CountryCode, user_AccountStatus));

            }

            return list_of_users;

        }

        public static IList<UserPrefix> GetUsersPrefix(MySqlConnection conn, int offset, int limit, string prefix) {

            conn.Open();
            using MySqlCommand cmd = new("SELECT id, user_name FROM Users WHERE user_name COLLATE utf8mb4_general_ci LIKE @prefix ORDER BY user_name ASC, id ASC LIMIT @limit OFFSET @offset", conn);
            cmd.Parameters.AddWithValue("@prefix",prefix + "%");
            cmd.Parameters.AddWithValue("@limit", limit);
            cmd.Parameters.AddWithValue("@offset", offset * limit);
            var reader = cmd.ExecuteReader();
            
            IList<UserPrefix> list_of_users = new List<UserPrefix>();

            while (reader.Read()) {

                string user_ID = reader.GetString("id");
                string user_Name = reader.GetString("user_name");

                list_of_users.Add(new UserPrefix(user_ID, user_Name));

            }

            return list_of_users;

        }

        public static IDictionary<int,int> GetUsersMetricsYears(MySqlConnection conn) {

            conn.Open();
            using MySqlCommand cmd = new("SELECT YEAR(account_creation) as year, COUNT(*) as number_of_users FROM Users GROUP BY YEAR(account_creation)", conn);
            var reader = cmd.ExecuteReader();
            
            IDictionary<int,int> years = new Dictionary<int,int>();

            while (reader.Read()) {

                int year = reader.GetInt32("year");
                int number_of_users = reader.GetInt32("number_of_users");

                years[year] = number_of_users;

            }

            return years;

        }

        public static IDictionary<int,int> GetUsersMetricsMonths(MySqlConnection conn, int year) {

            conn.Open();
            using MySqlCommand cmd = new("SELECT MONTH(account_creation) as month, COUNT(*) as number_of_users FROM Users WHERE YEAR(account_creation) = @year GROUP BY MONTH(account_creation)", conn);
            cmd.Parameters.AddWithValue("@year",year);
            var reader = cmd.ExecuteReader();
            
            IDictionary<int,int> months = new Dictionary<int,int>();

            while (reader.Read()) {

                int month = reader.GetInt32("month");
                int number_of_users = reader.GetInt32("number_of_users");

                months[month] = number_of_users;

            }

            return months;

        }

        public static IDictionary<int,int> GetUsersMetricsDays(MySqlConnection conn, int year, int month) {

            conn.Open();
            using MySqlCommand cmd = new("SELECT DAY(account_creation) as day, COUNT(*) as number_of_users FROM Users WHERE YEAR(account_creation) = @year AND MONTH(account_creation) = @month GROUP BY DAY(account_creation)", conn);
            cmd.Parameters.AddWithValue("@month",month);
            cmd.Parameters.AddWithValue("@year",year);
            var reader = cmd.ExecuteReader();
            
            IDictionary<int,int> days = new Dictionary<int,int>();

            while (reader.Read()) {

                int day = reader.GetInt32("day");
                int number_of_users = reader.GetInt32("number_of_users");

                days[day] = number_of_users;

            }

            return days;

        }

        public static ISet<string> GetUsersIDs(MySqlConnection conn) {

            conn.Open();

            using MySqlCommand cmd = new("SELECT id FROM Users", conn);
            cmd.CommandTimeout = 0;
            var reader = cmd.ExecuteReader();

            ISet<string> set_of_ids = new HashSet<string>();

            while (reader.Read())
                set_of_ids.Add(reader.GetString("id"));


            return set_of_ids;

        }

        public static int GetUsersCount(MySqlConnection conn) {

            conn.Open();
            
            using MySqlCommand cmd = new("SELECT COUNT(*) FROM Users", conn);
            var count = cmd.ExecuteScalar();
            return Convert.ToInt32(count);

        }

        public static int GetUsersActiveCount(MySqlConnection conn) {

            conn.Open();
            using MySqlCommand cmd = new("SELECT COUNT(*) FROM Users WHERE account_status = TRUE", conn);
            var count = cmd.ExecuteScalar();
            return Convert.ToInt32(count);

        }

    }
}