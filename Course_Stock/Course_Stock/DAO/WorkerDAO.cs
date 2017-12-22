using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Course_Stock.Models;
using Course_Stock.Log;


namespace Course_Stock.DAO
{
    public class WorkerDAO : DAO
    {
        public List<Material> GetAllMaterial()
        {
            Log.Log.For(this).Info("Start method GetAllMaterial");
            
            List<Material> recordList = new List<Material>();
            try
            {
                ConnectSCS();
                SqlCommand command = new SqlCommand("SELECT * FROM [Material]", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Material record = new Material();
                    record.id = Convert.ToInt32(reader["id_material"]);
                    record.name = Convert.ToString(reader["name"]);
                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception ex) { Log.Log.For(this).Error("GetAllMaterial Falled " + ex); }
            finally { Disconnect(); }
            return recordList;
        }


        public List<Table> GetTables(int id)
        {
            Log.Log.For(this).Info("Start method GetTables");
            List<Table> recordList = new List<Table>();
            try
            {
                ConnectSCS();
                SqlCommand command = new SqlCommand("SELECT * FROM [Tables] WHERE [Tables].[id_material]=@id ORDER BY [Tables].[date_open] DESC", Connection);
                command.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Table record = new Table();
                    record.id = Convert.ToInt32(reader["id_table"]);
                    record.id_material = Convert.ToInt32(reader["id_material"]);
                    record.date_open = Convert.ToDateTime(reader["date_open"]);
                    if (!reader.IsDBNull(3))
                    {
                        record.date_close = Convert.ToDateTime(reader["date_close"]);
                    }
                    record.close = Convert.ToBoolean(reader["close"]);
                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception ex) { Log.Log.For(this).Error("GetTables Falled " + ex); }
            finally { Disconnect(); }
            return recordList;
        }

        public List<Record> GetRecords(int id)
        {
            Log.Log.For(this).Info("Start method GetRecords");
            List<Record> recordList = new List<Record>();
            try
            {
                ConnectSCS();
                SqlCommand command = new SqlCommand("SELECT * FROM [Records] WHERE ([Records].[id_table] = @id) AND  ([Records].[check] = 1) ORDER BY [Records].[date_time] DESC", Connection);
                command.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Record record = new Record();
                    record.id = Convert.ToInt32(reader["id_record"]);
                    record.id_table = Convert.ToInt32(reader["id_table"]);
                    record.date_time = Convert.ToDateTime(reader["date_time"]);
                    record.supply_or_delivery = Convert.ToBoolean(reader["supply_or_delivery"]);
                    record.qantity = Convert.ToInt32(reader["quantity"]);
                    record.provider = Convert.ToString(reader["provider"]);
                    record.worker = Convert.ToString(reader["worker"]);
                    record.check = Convert.ToBoolean(reader["check"]);
                    recordList.Add(record);

                }
                reader.Close();
            }
            catch (Exception ex) { Log.Log.For(this).Error("GetRecords Falled " + ex); }
            finally { Disconnect(); }
            return recordList;
        }

        public void OpenTable(int id)
        {
            Log.Log.For(this).Info("Start method OpenTable");
            try
            {
                ConnectSCS();
                SqlCommand cmd = new SqlCommand("INSERT INTO[dbo].[Tables] ([id_material], [date_open],[close]) VALUES(@id_material, GetDate(), 0)", Connection);
                cmd.Parameters.Add(new SqlParameter("@id_material", id));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { Log.Log.For(this).Error("OpenTable Falled " + ex); }
            finally
            {
                Disconnect();
            }
        }

        public void CloseTable(int id)
        {
            Log.Log.For(this).Info("Start method CloseTable");
            try
            {
                ConnectSCS();
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Tables] SET [Tables].[close]=1, [Tables].[date_close]=GetDate() WHERE [Tables].[id_table]=@id ", Connection);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { Log.Log.For(this).Error("CloseTable Falled " + ex); }
            finally
            {
                Disconnect();
            }

        }

        public void CreateRecord(Record rec)
        {
            Log.Log.For(this).Info("Start method CreateRecord");
            try
            {
                ConnectSCS();
                SqlCommand cmd = new SqlCommand("INSERT INTO[dbo].[Records] ([id_table], [date_time], [supply_or_delivery], [quantity], [provider], [worker]) VALUES(@id_table, GetDate(), @supply_or_delivery, @quantity, @provider, @worker)", Connection);
                cmd.Parameters.Add(new SqlParameter("@id_table", rec.id_table));
                cmd.Parameters.Add(new SqlParameter("@supply_or_delivery", rec.supply_or_delivery));
                cmd.Parameters.Add(new SqlParameter("@quantity", rec.qantity));
                if (rec.provider == null)
                {
                    cmd.Parameters.Add(new SqlParameter("@provider", ""));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@provider", rec.provider));
                }
                cmd.Parameters.Add(new SqlParameter("@worker", rec.worker));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { Log.Log.For(this).Error("CreateRecord Falled " + ex); }
            finally
            {
                Disconnect();
            }
        }

        public void CreateMaterial(Material m)
        {
            Log.Log.For(this).Info("Start method CreateMaterial");
            try
            {
                ConnectSCS();
                SqlCommand cmd = new SqlCommand("INSERT INTO[dbo].[Material] ([name], [quantity]) VALUES(@name, 0)", Connection);
                cmd.Parameters.Add(new SqlParameter("@name", m.name));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { Log.Log.For(this).Error("CreateMaterial Falled " + ex); }
            finally
            {
                Disconnect();
            }
        }


        public List<Record> GetRecordsNonCheck()
        {
            Log.Log.For(this).Info("Start method GetRecordsNonCheck");
            List<Record> recordList = new List<Record>();
            try
            {
                ConnectSCS();
                SqlCommand command = new SqlCommand("SELECT [Material].[name],[Records].[id_record], [Records].[date_time],[Records].[supply_or_delivery], [Records].[quantity], [Records].[provider], [Records].[worker] FROM[Records],[Tables],[Material] WHERE([Records].[check] IS NULL) AND([Tables].id_table =[Records].[id_table]) AND([Tables].id_material =[Material].[id_material]) ORDER BY[Records].[date_time]", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Record record = new Record();
                    record.id = Convert.ToInt32(reader["id_record"]);
                    record.name_material = Convert.ToString(reader["name"]);
                    record.date_time = Convert.ToDateTime(reader["date_time"]);
                    record.supply_or_delivery = Convert.ToBoolean(reader["supply_or_delivery"]);
                    record.qantity = Convert.ToInt32(reader["quantity"]);
                    record.provider = Convert.ToString(reader["provider"]);
                    record.worker = Convert.ToString(reader["worker"]);
                    recordList.Add(record);

                }
                reader.Close();
            }
            catch (Exception ex) { Log.Log.For(this).Error("GetRecordsNonCheck Falled " + ex); }
            finally { Disconnect(); }
            return recordList;
        }

        public void SetCheck(int id, bool check)
        {
            Log.Log.For(this).Info("Start method SetCheck");
            try
            {
                ConnectSCS();
                SqlCommand cmd = new SqlCommand("exec Up_Material @id, @ch", Connection);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@ch", check));

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { Log.Log.For(this).Error("SetCheck Falled " + ex); }
            finally
            {
                Disconnect();
            }

        }


    }

}

