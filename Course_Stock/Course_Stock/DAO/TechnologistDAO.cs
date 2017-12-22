using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Course_Stock.Models;
using Course_Stock.Log;

namespace Course_Stock.DAO
{
    public class TechnologistDAO : DAO
    {
        public List<Product> GetAllProduct()
        {
            Log.Log.For(this).Info("Start method GetAllProduct");
            
            List<Product> recordList = new List<Product>();
            try
            {
                ConnectSCS();
                SqlCommand command = new SqlCommand("SELECT * FROM [Product]", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product record = new Product();
                    record.id = Convert.ToInt32(reader["id_product"]);
                    record.name = Convert.ToString(reader["name"]);
                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception ex) { Log.Log.For(this).Error("GetAllProduct Falled " + ex); }
            finally { Disconnect(); }
            return recordList;
        }

        public List<Component> GetComponentProduct(int id)
        {
            Log.Log.For(this).Info("Start method GetComponentProduct");
            List<Component> recordList = new List<Component>();
            try
            {
                ConnectSCS();
                SqlCommand command = new SqlCommand("SELECT [List].[quantity_material], [Material].[name] FROM [List], [Material] WHERE [List].[id_product]=@id AND [List].[id_material]=[Material].id_material", Connection);
                command.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Component record = new Component();
                    record.quantityInProduct = Convert.ToInt32(reader["quantity_material"]);
                    record.name = Convert.ToString(reader["name"]);
                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception ex) { Log.Log.For(this).Error("GetComponentProduct Falled " + ex); }
            finally { Disconnect(); }
            return recordList;
        }


        public List<Component> GetComponentForCalculate(int id)
        {
            Log.Log.For(this).Info("Start method GetComponentForCalculate");
            
            List<Component> recordList = new List<Component>();
            try
            {
                ConnectSCS();
                SqlCommand command = new SqlCommand("SELECT [List].[quantity_material], [Material].[name], [Material].[quantity] FROM [List], [Material] WHERE [List].[id_product]=@id AND [List].[id_material]=[Material].id_material", Connection);
                command.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Component record = new Component();
                    record.name = Convert.ToString(reader["name"]);
                    record.quantityInStockNow = Convert.ToInt32(reader["quantity"]);
                    record.quantityInProduct = Convert.ToInt32(reader["quantity_material"]);
                    
                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception ex) { Log.Log.For(this).Error("GetComponentForCalculate Falled " + ex); }
            finally { Disconnect(); }
            return recordList;
        }




    }
}