using Microsoft.Data.SqlClient;

namespace Program.scr.core.dbt
{
    public class DBT_Users
    {
        public int UserId;
        public string Login;
        public string Password;
        public string Role;
        public DateTime? CreatedAt;


        public static List<DBT_Users> GetAll()
        {
            var objs = new List<DBT_Users>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Users";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Users();

                                obj.UserId = reader.GetInt32(0);
                                obj.Login = reader.GetString(1);
                                obj.Password = reader.GetString(2);
                                obj.Role = reader.GetString(3);
                                if (reader.IsDBNull(4)) obj.CreatedAt = null;
                                else obj.CreatedAt = DateTime.Parse(reader.GetValue(4).ToString());

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }

        public static DBT_Users GetById(int id)
        {
            var obj = new DBT_Users();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Users WHERE UserId = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.UserId = reader.GetInt32(0);
                                obj.Login = reader.GetString(1);
                                obj.Password = reader.GetString(2);
                                obj.Role = reader.GetString(3);
                                if (reader.IsDBNull(4)) obj.CreatedAt = null;
                                else obj.CreatedAt = DateTime.Parse(reader.GetValue(4).ToString());
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Users obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO Users VALUES (@Login, @Password, @Role, @CreatedAt);";
                        query.Parameters.AddWithValue("@Login", obj.Login);
                        query.Parameters.AddWithValue("@Password", obj.Password);
                        query.Parameters.AddWithValue("@Role", obj.Role);
                        query.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch { return -1; }
            int _id = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT MAX(UserId) FROM Users;";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _id = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch { return -1; }
            return _id;
        }

        public static int Edit(DBT_Users obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE Users SET Login = @Login, Password = @Password, Role = @Role, CreatedAt = @CreatedAt WHERE UserId = @id;";
                        query.Parameters.AddWithValue("@Login", obj.Login);
                        query.Parameters.AddWithValue("@Password", obj.Password);
                        query.Parameters.AddWithValue("@Role", obj.Role);
                        query.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);
                        query.Parameters.AddWithValue("@id", obj.UserId);
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch { return -1; }
            return 0;
        }

        public static int Remove(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "DELETE FROM Users WHERE UserId = @id;";
                        query.Parameters.AddWithValue("@id", id);
                        query.ExecuteNonQuery();
                    }
                }
            }
            catch { return -1; }
            return 0;
        }

    }
}
