namespace DataBase
{

    static public class DAOConfig
    {

        static public readonly string ConnectionString = "server=localhost;database=HeavenBooking;uid=user;pwd=password;Pooling=true;Min Pool Size=2;Max Pool Size=20;AllowLoadLocalInfile=true;Charset=utf8mb4;";
        static public readonly string UserTable = @"CREATE TABLE IF NOT EXISTS Users (
                                                    	id VARCHAR(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin PRIMARY KEY,
                                                        user_name VARCHAR(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL,
                                                        email VARCHAR(100) NOT NULL,
                                                        birth_date DATE NOT NULL,
                                                        sex TINYINT NOT NULL,
                                                        passport VARCHAR(20) NOT NULL,
                                                        country_code CHAR(2) NOT NULL,
                                                        account_creation DATE NOT NULL,
                                                        account_status BOOLEAN NOT NULL
                                                    );";
        static public readonly string UserIndexName = "CREATE INDEX index_name ON Users(user_name);";
        static public readonly string UserIndexAccountCreation = "CREATE INDEX index_account_creation ON Users(account_creation);";

    }

}