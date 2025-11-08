using Microsoft.Data.SqlClient;

namespace Program.scr.core.dbt
{
    public class DBT_Properties
    {
        public int PropertyId;
        public int? ManagerId;
        public string Address;
        public string Type;
        public string? Description;
        public decimal Area;
        public int Rooms;
        public decimal Price;
        public string Status;
        public DateTime? CreatedAt;
        public DateTime? UpdatedAt;


        public static List<DBT_Properties> GetAll()
        {
            var objs = new List<DBT_Properties>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Properties";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Properties();

                                obj.PropertyId = reader.GetInt32(0);
                                if (reader.IsDBNull(1)) obj.ManagerId = null;
                                else obj.ManagerId = reader.GetInt32(1);
                                obj.Address = reader.GetString(2);
                                obj.Type = reader.GetString(3);
                                if (reader.IsDBNull(4)) obj.Description = string.Empty;
                                else obj.Description = reader.GetString(4);
                                obj.Area = reader.GetDecimal(5);
                                obj.Rooms = reader.GetInt32(6);
                                obj.Price = reader.GetDecimal(7);
                                obj.Status = reader.GetString(8);
                                if (reader.IsDBNull(9)) obj.CreatedAt = null;
                                else obj.CreatedAt = DateTime.Parse(reader.GetValue(9).ToString());
                                if (reader.IsDBNull(10)) obj.UpdatedAt = null;
                                else obj.UpdatedAt = DateTime.Parse(reader.GetValue(10).ToString());

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }

        public static DBT_Properties GetById(int id)
        {
            var obj = new DBT_Properties();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Properties WHERE PropertyId = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.PropertyId = reader.GetInt32(0);
                                if (reader.IsDBNull(1)) obj.ManagerId = null;
                                else obj.ManagerId = reader.GetInt32(1);
                                obj.Address = reader.GetString(2);
                                obj.Type = reader.GetString(3);
                                if (reader.IsDBNull(4)) obj.Description = string.Empty;
                                else obj.Description = reader.GetString(4);
                                obj.Area = reader.GetDecimal(5);
                                obj.Rooms = reader.GetInt32(6);
                                obj.Price = reader.GetDecimal(7);
                                obj.Status = reader.GetString(8);
                                if (reader.IsDBNull(9)) obj.CreatedAt = null;
                                else obj.CreatedAt = DateTime.Parse(reader.GetValue(9).ToString());
                                if (reader.IsDBNull(10)) obj.UpdatedAt = null;
                                else obj.UpdatedAt = DateTime.Parse(reader.GetValue(10).ToString());
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Properties obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        if(obj.ManagerId != null) query.CommandText = "INSERT INTO Properties (ManagerId, Address, Type, Description, Area, Rooms, Price, Status) VALUES (@ManagerId, @Address, @Type, @Description, @Area, @Rooms, @Price, @Status);";
                        else query.CommandText = "INSERT INTO Properties (Address, Type, Description, Area, Rooms, Price, Status) VALUES (@Address, @Type, @Description, @Area, @Rooms, @Price, @Status);";
                        
                        if (obj.ManagerId != null) query.Parameters.AddWithValue("@ManagerId", obj.ManagerId);
                        query.Parameters.AddWithValue("@Address", obj.Address);
                        query.Parameters.AddWithValue("@Type", obj.Type);
                        query.Parameters.AddWithValue("@Description", obj.Description);
                        query.Parameters.AddWithValue("@Area", obj.Area);
                        query.Parameters.AddWithValue("@Rooms", obj.Rooms);
                        query.Parameters.AddWithValue("@Price", obj.Price);
                        query.Parameters.AddWithValue("@Status", obj.Status);
                        //query.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);
                        //query.Parameters.AddWithValue("@UpdatedAt", obj.UpdatedAt);
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
                        query.CommandText = "SELECT MAX(PropertyId) FROM Properties;";
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

        public static int Edit(DBT_Properties obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        if(obj.ManagerId != null) query.CommandText = "UPDATE Properties SET ManagerId = @ManagerId, Address = @Address, Type = @Type, Description = @Description, Area = @Area, Rooms = @Rooms, Price = @Price, Status = @Status, UpdatedAt = GETDATE() WHERE PropertyId = @id;";
                        else query.CommandText = "UPDATE Properties SET ManagerId = null, Address = @Address, Type = @Type, Description = @Description, Area = @Area, Rooms = @Rooms, Price = @Price, Status = @Status, UpdatedAt = GETDATE() WHERE PropertyId = @id;";
                        if (obj.ManagerId != null) query.Parameters.AddWithValue("@ManagerId", obj.ManagerId);
                        query.Parameters.AddWithValue("@Address", obj.Address);
                        query.Parameters.AddWithValue("@Type", obj.Type);
                        query.Parameters.AddWithValue("@Description", obj.Description);
                        query.Parameters.AddWithValue("@Area", obj.Area);
                        query.Parameters.AddWithValue("@Rooms", obj.Rooms);
                        query.Parameters.AddWithValue("@Price", obj.Price);
                        query.Parameters.AddWithValue("@Status", obj.Status);
                        //query.Parameters.AddWithValue("@CreatedAt", obj.CreatedAt);
                        //query.Parameters.AddWithValue("@UpdatedAt", obj.UpdatedAt);
                        query.Parameters.AddWithValue("@id", obj.PropertyId);
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
                        query.CommandText = "DELETE FROM Properties WHERE PropertyId = @id;";
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
