using Microsoft.Data.SqlClient;

namespace Program.scr.core.dbt
{
    public class DBT_ClientProfiles
    {
        public int ClientId;
        public int UserId;
        public string FullName;
        public string? Phone;
        public string? Email;


        public static List<DBT_ClientProfiles> GetAll()
        {
            var objs = new List<DBT_ClientProfiles>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM ClientProfiles";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_ClientProfiles();

                                obj.ClientId = reader.GetInt32(0);
                                obj.UserId = reader.GetInt32(1);
                                obj.FullName = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.Phone = string.Empty;
                                else obj.Phone = reader.GetString(3);
                                if (reader.IsDBNull(4)) obj.Email = string.Empty;
                                else obj.Email = reader.GetString(4);

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }

        public static DBT_ClientProfiles GetById(int id)
        {
            var obj = new DBT_ClientProfiles();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM ClientProfiles WHERE ClientId = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.ClientId = reader.GetInt32(0);
                                obj.UserId = reader.GetInt32(1);
                                obj.FullName = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.Phone = string.Empty;
                                else obj.Phone = reader.GetString(3);
                                if (reader.IsDBNull(4)) obj.Email = string.Empty;
                                else obj.Email = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static DBT_ClientProfiles GetByUserId(int id)
        {
            var obj = new DBT_ClientProfiles();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM ClientProfiles WHERE UserId = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.ClientId = reader.GetInt32(0);
                                obj.UserId = reader.GetInt32(1);
                                obj.FullName = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.Phone = string.Empty;
                                else obj.Phone = reader.GetString(3);
                                if (reader.IsDBNull(4)) obj.Email = string.Empty;
                                else obj.Email = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_ClientProfiles obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO ClientProfiles VALUES (@UserId, @FullName, @Phone, @Email);";
                        query.Parameters.AddWithValue("@UserId", obj.UserId);
                        query.Parameters.AddWithValue("@FullName", obj.FullName);
                        query.Parameters.AddWithValue("@Phone", obj.Phone);
                        query.Parameters.AddWithValue("@Email", obj.Email);
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
                        query.CommandText = "SELECT MAX(ClientId) FROM ClientProfiles;";
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

        public static int Edit(DBT_ClientProfiles obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE ClientProfiles SET UserId = @UserId, FullName = @FullName, Phone = @Phone, Email = @Email WHERE ClientId = @id;";
                        query.Parameters.AddWithValue("@UserId", obj.UserId);
                        query.Parameters.AddWithValue("@FullName", obj.FullName);
                        query.Parameters.AddWithValue("@Phone", obj.Phone);
                        query.Parameters.AddWithValue("@Email", obj.Email);
                        query.Parameters.AddWithValue("@id", obj.ClientId);
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
                        query.CommandText = "DELETE FROM ClientProfiles WHERE ClientId = @id;";
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
