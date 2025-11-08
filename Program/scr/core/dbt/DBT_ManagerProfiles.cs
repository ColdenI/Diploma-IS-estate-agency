using Microsoft.Data.SqlClient;

namespace Program.scr.core.dbt
{
    public class DBT_ManagerProfiles
    {
        public int ManagerId;
        public int UserId;
        public string FullName;
        public string? Phone;
        public string? LicenseNumber;
        public string? Email;
        public string? Post;
        public DateTime? HireDate;


        public static List<DBT_ManagerProfiles> GetAll()
        {
            var objs = new List<DBT_ManagerProfiles>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM ManagerProfiles";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_ManagerProfiles();

                                obj.ManagerId = reader.GetInt32(0);
                                obj.UserId = reader.GetInt32(1);
                                obj.FullName = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.Phone = string.Empty;
                                else obj.Phone = reader.GetString(3);
                                if (reader.IsDBNull(4)) obj.LicenseNumber = string.Empty;
                                else obj.LicenseNumber = reader.GetString(4);
                                if (reader.IsDBNull(5)) obj.Email = string.Empty;
                                else obj.Email = reader.GetString(5);
                                if (reader.IsDBNull(6)) obj.Post = string.Empty;
                                else obj.Post = reader.GetString(6);
                                if (reader.IsDBNull(7)) obj.HireDate = null;
                                else obj.HireDate = DateTime.Parse(reader.GetValue(7).ToString());

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }

        public static DBT_ManagerProfiles GetById(int id)
        {
            var obj = new DBT_ManagerProfiles();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM ManagerProfiles WHERE ManagerId = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.ManagerId = reader.GetInt32(0);
                                obj.UserId = reader.GetInt32(1);
                                obj.FullName = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.Phone = string.Empty;
                                else obj.Phone = reader.GetString(3);
                                if (reader.IsDBNull(4)) obj.LicenseNumber = string.Empty;
                                else obj.LicenseNumber = reader.GetString(4);
                                if (reader.IsDBNull(5)) obj.Email = string.Empty;
                                else obj.Email = reader.GetString(5);
                                if (reader.IsDBNull(6)) obj.Post = string.Empty;
                                else obj.Post = reader.GetString(6);
                                if (reader.IsDBNull(7)) obj.HireDate = null;
                                else obj.HireDate = DateTime.Parse(reader.GetValue(7).ToString());
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static DBT_ManagerProfiles GetByUserId(int id)
        {
            var obj = new DBT_ManagerProfiles();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM ManagerProfiles WHERE UserId = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.ManagerId = reader.GetInt32(0);
                                obj.UserId = reader.GetInt32(1);
                                obj.FullName = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.Phone = string.Empty;
                                else obj.Phone = reader.GetString(3);
                                if (reader.IsDBNull(4)) obj.LicenseNumber = string.Empty;
                                else obj.LicenseNumber = reader.GetString(4);
                                if (reader.IsDBNull(5)) obj.Email = string.Empty;
                                else obj.Email = reader.GetString(5);
                                if (reader.IsDBNull(6)) obj.Post = string.Empty;
                                else obj.Post = reader.GetString(6);
                                if (reader.IsDBNull(7)) obj.HireDate = null;
                                else obj.HireDate = DateTime.Parse(reader.GetValue(7).ToString());
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_ManagerProfiles obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO ManagerProfiles VALUES (@UserId, @FullName, @Phone, @LicenseNumber, @Email, @Post, @HireDate);";
                        query.Parameters.AddWithValue("@UserId", obj.UserId);
                        query.Parameters.AddWithValue("@FullName", obj.FullName);
                        query.Parameters.AddWithValue("@Phone", obj.Phone);
                        query.Parameters.AddWithValue("@LicenseNumber", obj.LicenseNumber);
                        query.Parameters.AddWithValue("@Email", obj.Email);
                        query.Parameters.AddWithValue("@Post", obj.Post);
                        query.Parameters.AddWithValue("@HireDate", obj.HireDate);
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
                        query.CommandText = "SELECT MAX(ManagerId) FROM ManagerProfiles;";
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

        public static int Edit(DBT_ManagerProfiles obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE ManagerProfiles SET UserId = @UserId, FullName = @FullName, Phone = @Phone, LicenseNumber = @LicenseNumber, Email = @Email, Post = @Post, HireDate = @HireDate WHERE ManagerId = @id;";
                        query.Parameters.AddWithValue("@UserId", obj.UserId);
                        query.Parameters.AddWithValue("@FullName", obj.FullName);
                        query.Parameters.AddWithValue("@Phone", obj.Phone);
                        query.Parameters.AddWithValue("@LicenseNumber", obj.LicenseNumber);
                        query.Parameters.AddWithValue("@Email", obj.Email);
                        query.Parameters.AddWithValue("@Post", obj.Post);
                        query.Parameters.AddWithValue("@HireDate", obj.HireDate);
                        query.Parameters.AddWithValue("@id", obj.ManagerId);
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
                        query.CommandText = "DELETE FROM ManagerProfiles WHERE ManagerId = @id;";
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
