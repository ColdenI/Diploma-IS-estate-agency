using Microsoft.Data.SqlClient;

namespace Program.scr.core.dbt
{
    public class DBT_Photos
    {
        public int PhotoId;
        public int PropertyId;
        public string PhotoData;
        public bool? IsMain;


        public static List<DBT_Photos> GetAll()
        {
            var objs = new List<DBT_Photos>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Photos";
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Photos();

                                obj.PhotoId = reader.GetInt32(0);
                                obj.PropertyId = reader.GetInt32(1);
                                obj.PhotoData = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.IsMain = null;
                                else obj.IsMain = reader.GetBoolean(3);

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }
            return objs;
        }

        public static List<DBT_Photos> GetAllByPropertyId(int id)
        {
            var objs = new List<DBT_Photos>();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Photos WHERE PropertyId = @PropertyId";
                        query.Parameters.AddWithValue("@PropertyId", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = new DBT_Photos();

                                obj.PhotoId = reader.GetInt32(0);
                                obj.PropertyId = reader.GetInt32(1);
                                obj.PhotoData = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.IsMain = null;
                                else obj.IsMain = reader.GetBoolean(3);

                                objs.Add(obj);
                            }
                        }
                    }
                }
            }
            catch { objs = null; }

            if(objs != null)
                if (objs.Count == 0) return null;

            return objs;
        }

        public static DBT_Photos GetById(int id)
        {
            var obj = new DBT_Photos();
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "SELECT * FROM Photos WHERE PhotoId = @id";
                        query.Parameters.AddWithValue("@id", id);
                        using (var reader = query.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                obj.PhotoId = reader.GetInt32(0);
                                obj.PropertyId = reader.GetInt32(1);
                                obj.PhotoData = reader.GetString(2);
                                if (reader.IsDBNull(3)) obj.IsMain = null;
                                else obj.IsMain = reader.GetBoolean(3);
                            }
                        }
                    }
                }
            }
            catch { obj = null; }
            return obj;
        }

        public static int Create(DBT_Photos obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "INSERT INTO Photos VALUES (@PropertyId, @PhotoData, @IsMain);";
                        query.Parameters.AddWithValue("@PropertyId", obj.PropertyId);
                        query.Parameters.AddWithValue("@PhotoData", obj.PhotoData);
                        query.Parameters.AddWithValue("@IsMain", obj.IsMain);
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
                        query.CommandText = "SELECT MAX(PhotoId) FROM Photos;";
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

        public static int Edit(DBT_Photos obj)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(SQL._sqlConnectStr))
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = "UPDATE Photos SET PropertyId = @PropertyId, PhotoData = @PhotoData, IsMain = @IsMain WHERE PhotoId = @id;";
                        query.Parameters.AddWithValue("@PropertyId", obj.PropertyId);
                        query.Parameters.AddWithValue("@PhotoData", obj.PhotoData);
                        query.Parameters.AddWithValue("@IsMain", obj.IsMain);
                        query.Parameters.AddWithValue("@id", obj.PhotoId);
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
                        query.CommandText = "DELETE FROM Photos WHERE PhotoId = @id;";
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
