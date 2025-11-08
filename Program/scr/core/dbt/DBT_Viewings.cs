using Microsoft.Data.SqlClient;

namespace Program.scr.core.dbt
{
    public class DBT_Viewings
    {
        public int ViewingId;
        public int RequestId;
        public int PropertyId;
        public DateTime ScheduledTime;
        public string Status;
        public string? Notes;
        public DateTime? CreatedAt;


        public static List<DBT_Viewings> GetAll()
        {
            var objs = new List<DBT_Viewings>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Viewings";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Viewings();

                                obj.ViewingId = reader.GetInt32(0);
                                obj.RequestId = reader.GetInt32(1);
                                obj.PropertyId = reader.GetInt32(2);
                                obj.ScheduledTime = DateTime.Parse(reader.GetValue(3).ToString());
                                obj.Status = reader.GetString(4);
                                if (reader.IsDBNull(5)) obj.Notes = string.Empty;
                                else obj.Notes = reader.GetString(5);
                                if (reader.IsDBNull(6)) obj.CreatedAt = null;
                                else obj.CreatedAt = DateTime.Parse(reader.GetValue(6).ToString());

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }

        public static DBT_Viewings GetById(int id)
        {
            var obj = new DBT_Viewings();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Viewings WHERE ViewingId = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.ViewingId = reader.GetInt32(0);
                                obj.RequestId = reader.GetInt32(1);
                                obj.PropertyId = reader.GetInt32(2);
                                obj.ScheduledTime = DateTime.Parse(reader.GetValue(3).ToString());
                                obj.Status = reader.GetString(4);
                                if (reader.IsDBNull(5)) obj.Notes = string.Empty;
                                else obj.Notes = reader.GetString(5);
                                if (reader.IsDBNull(6)) obj.CreatedAt = null;
                                else obj.CreatedAt = DateTime.Parse(reader.GetValue(6).ToString());
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Viewings obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO Viewings (RequestId, PropertyId, ScheduledTime, Status, Notes) VALUES (@RequestId, @PropertyId, @ScheduledTime, @Status, @Notes);";
                        query.Parameters.AddWithValue("@RequestId", obj.RequestId);
                        query.Parameters.AddWithValue("@PropertyId", obj.PropertyId);
                        query.Parameters.AddWithValue("@ScheduledTime", obj.ScheduledTime);
                        query.Parameters.AddWithValue("@Status", obj.Status);
                        query.Parameters.AddWithValue("@Notes", obj.Notes);
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
                        query.CommandText = "SELECT MAX(ViewingId) FROM Viewings;";
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

        public static int Edit(DBT_Viewings obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE Viewings SET RequestId = @RequestId, PropertyId = @PropertyId, ScheduledTime = @ScheduledTime, Status = @Status, Notes = @Notes WHERE ViewingId = @id;";
                        query.Parameters.AddWithValue("@RequestId", obj.RequestId);
                        query.Parameters.AddWithValue("@PropertyId", obj.PropertyId);
                        query.Parameters.AddWithValue("@ScheduledTime", obj.ScheduledTime);
                        query.Parameters.AddWithValue("@Status", obj.Status);
                        query.Parameters.AddWithValue("@Notes", obj.Notes);
                        query.Parameters.AddWithValue("@id", obj.ViewingId);
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
                        query.CommandText = "DELETE FROM Viewings WHERE ViewingId = @id;";
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
