using Microsoft.Data.SqlClient;

namespace Program.scr.core.dbt
{
    public class DBT_Deals
    {
        public int DealId;
        public int? PropertyId;
        public int ClientId;
        public decimal SalePrice;
        public string Status;
        public DateTime? SignedDate;
        public decimal? CommissionRate;
        public DateTime? CreatedAt;


        public static List<DBT_Deals> GetAll()
        {
            var objs = new List<DBT_Deals>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Deals";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Deals();

                                obj.DealId = reader.GetInt32(0);
                                if (reader.IsDBNull(1)) obj.PropertyId = null;
                                else obj.PropertyId = reader.GetInt32(1);
                                obj.ClientId = reader.GetInt32(2);
                                obj.SalePrice = reader.GetDecimal(3);
                                obj.Status = reader.GetString(4);
                                if (reader.IsDBNull(5)) obj.SignedDate = null;
                                else obj.SignedDate = DateTime.Parse(reader.GetValue(5).ToString());
                                if (reader.IsDBNull(6)) obj.CommissionRate = null;
                                else obj.CommissionRate = reader.GetDecimal(6);
                                if (reader.IsDBNull(7)) obj.CreatedAt = null;
                                else obj.CreatedAt = DateTime.Parse(reader.GetValue(7).ToString());

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }

        public static DBT_Deals GetById(int id)
        {
            var obj = new DBT_Deals();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Deals WHERE DealId = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.DealId = reader.GetInt32(0);
                                if (reader.IsDBNull(1)) obj.PropertyId = null;
                                else obj.PropertyId = reader.GetInt32(1);
                                obj.ClientId = reader.GetInt32(2);
                                obj.SalePrice = reader.GetDecimal(3);
                                obj.Status = reader.GetString(4);
                                if (reader.IsDBNull(5)) obj.SignedDate = null;
                                else obj.SignedDate = DateTime.Parse(reader.GetValue(5).ToString());
                                if (reader.IsDBNull(6)) obj.CommissionRate = null;
                                else obj.CommissionRate = reader.GetDecimal(6);
                                if (reader.IsDBNull(7)) obj.CreatedAt = null;
                                else obj.CreatedAt = DateTime.Parse(reader.GetValue(7).ToString());
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Deals obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        if (obj.PropertyId != null) query.CommandText = "INSERT INTO Deals (PropertyId, ClientId, SalePrice, Status, SignedDate, CommissionRate) VALUES (@PropertyId, @ClientId, @SalePrice, @Status, @SignedDate, @CommissionRate);";
                        else query.CommandText = "INSERT INTO Deals (ClientId, SalePrice, Status, SignedDate, CommissionRate) VALUES (@ClientId, @SalePrice, @Status, @SignedDate, @CommissionRate);";

                        if (obj.PropertyId != null) query.Parameters.AddWithValue("@PropertyId", obj.PropertyId);
                        query.Parameters.AddWithValue("@ClientId", obj.ClientId);
                        query.Parameters.AddWithValue("@SalePrice", obj.SalePrice);
                        query.Parameters.AddWithValue("@Status", obj.Status);
                        query.Parameters.AddWithValue("@SignedDate", obj.SignedDate);
                        query.Parameters.AddWithValue("@CommissionRate", obj.CommissionRate);
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
                        query.CommandText = "SELECT MAX(DealId) FROM Deals;";
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

        public static int Edit(DBT_Deals obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        if (obj.PropertyId != null) query.CommandText = "UPDATE Deals SET PropertyId = @PropertyId, ClientId = @ClientId, SalePrice = @SalePrice, Status = @Status, SignedDate = @SignedDate, CommissionRate = @CommissionRate WHERE DealId = @id;";
                        else query.CommandText = "UPDATE Deals SET ClientId = @ClientId, SalePrice = @SalePrice, Status = @Status, SignedDate = @SignedDate, CommissionRate = @CommissionRate WHERE DealId = @id;";
                        
                        if (obj.PropertyId != null) query.Parameters.AddWithValue("@PropertyId", obj.PropertyId);
                        query.Parameters.AddWithValue("@ClientId", obj.ClientId);
                        query.Parameters.AddWithValue("@SalePrice", obj.SalePrice);
                        query.Parameters.AddWithValue("@Status", obj.Status);
                        query.Parameters.AddWithValue("@SignedDate", obj.SignedDate);
                        query.Parameters.AddWithValue("@CommissionRate", obj.CommissionRate);
                        query.Parameters.AddWithValue("@id", obj.DealId);
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
                        query.CommandText = "DELETE FROM Deals WHERE DealId = @id;";
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
