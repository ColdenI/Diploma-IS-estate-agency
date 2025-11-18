using Microsoft.Data.SqlClient;

namespace Program.scr.core.dbt
{
    public class DBT_Requests
    {
        public int RequestId;
        public int ClientId;
        public string DesiredType;
        public decimal? BudgetMin;
        public decimal? BudgetMax;
        public string Status;
        public DateTime? CreatedAt;


        public static List<DBT_Requests> GetAll()
        {
            var objs = new List<DBT_Requests>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Requests";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Requests();

                                obj.RequestId = reader.GetInt32(0);
                                obj.ClientId = reader.GetInt32(1);
                                obj.DesiredType = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.BudgetMin = null;
                                else obj.BudgetMin = reader.GetDecimal(3);
                                if (reader.IsDBNull(4)) obj.BudgetMax = null;
                                else obj.BudgetMax = reader.GetDecimal(4);
                                obj.Status = reader.GetString(5);
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
        public static List<DBT_Requests> GetAllByClientId(int id)
        {
            var objs = new List<DBT_Requests>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = $"SELECT * FROM Requests WHERE ClientId = {id}";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Requests();

                                obj.RequestId = reader.GetInt32(0);
                                obj.ClientId = reader.GetInt32(1);
                                obj.DesiredType = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.BudgetMin = null;
                                else obj.BudgetMin = reader.GetDecimal(3);
                                if (reader.IsDBNull(4)) obj.BudgetMax = null;
                                else obj.BudgetMax = reader.GetDecimal(4);
                                obj.Status = reader.GetString(5);
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

        public static DBT_Requests GetById(int id)
        {
            var obj = new DBT_Requests();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Requests WHERE RequestId = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.RequestId = reader.GetInt32(0);
                                obj.ClientId = reader.GetInt32(1);
                                obj.DesiredType = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.BudgetMin = null;
                                else obj.BudgetMin = reader.GetDecimal(3);
                                if (reader.IsDBNull(4)) obj.BudgetMax = null;
                                else obj.BudgetMax = reader.GetDecimal(4);
                                obj.Status = reader.GetString(5);
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

        public static int Create(DBT_Requests obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO Requests (ClientId, DesiredType, BudgetMin, BudgetMax, Status) VALUES (@ClientId, @DesiredType, @BudgetMin, @BudgetMax, @Status);";
                        query.Parameters.AddWithValue("@ClientId", obj.ClientId);
                        query.Parameters.AddWithValue("@DesiredType", obj.DesiredType);
                        query.Parameters.AddWithValue("@BudgetMin", obj.BudgetMin);
                        query.Parameters.AddWithValue("@BudgetMax", obj.BudgetMax);
                        query.Parameters.AddWithValue("@Status", obj.Status);
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
                        query.CommandText = "SELECT MAX(RequestId) FROM Requests;";
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

        public static int Edit(DBT_Requests obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE Requests SET ClientId = @ClientId, DesiredType = @DesiredType, BudgetMin = @BudgetMin, BudgetMax = @BudgetMax, Status = @Status WHERE RequestId = @id;";
                        query.Parameters.AddWithValue("@ClientId", obj.ClientId);
                        query.Parameters.AddWithValue("@DesiredType", obj.DesiredType);
                        query.Parameters.AddWithValue("@BudgetMin", obj.BudgetMin);
                        query.Parameters.AddWithValue("@BudgetMax", obj.BudgetMax);
                        query.Parameters.AddWithValue("@Status", obj.Status);
                        query.Parameters.AddWithValue("@id", obj.RequestId);
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
                        query.CommandText = "DELETE FROM Requests WHERE RequestId = @id;";
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
