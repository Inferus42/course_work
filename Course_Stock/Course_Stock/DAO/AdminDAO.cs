using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Course_Stock.Models;

namespace Course_Stock.DAO
{
    public class AdminDAO : DAO
    {
        public List<User> GetAllUsers()
        {
            Log.Log.For(this).Info("Start method GetAllUsers");
            List<User> recordList = new List<User>();
            try
            {
                ConnectDC();
                SqlCommand command = new SqlCommand("SELECT [AspNetUsers].[Id], [AspNetUsers].[UserName], [AspNetUsers].[Email], [AspNetUsers].[PhoneNumber], [AspNetRoles].[Name] FROM [AspNetUsers],[AspNetUserRoles],[AspNetRoles] WHERE ([AspNetUsers].[Id]=[AspNetUserRoles].[UserId]) AND ([AspNetUserRoles].[RoleId]=[AspNetRoles].[Id])", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User record = new User();
                    record.id = Convert.ToString(reader["Id"]);
                    record.UserName = Convert.ToString(reader["UserName"]);
                    record.email = Convert.ToString(reader["Email"]);
                    record.phone = Convert.ToString(reader["PhoneNumber"]);
                    record.role = Convert.ToString(reader["Name"]);

                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception ex) { Log.Log.For(this).Error("GetAllUsers Falled " + ex); }
            finally { Disconnect(); }
            return recordList;
        }

        public User getById(string id)
        {
            Log.Log.For(this).Info("Start method getById");
            List<User> users = GetAllUsers();
            foreach (var s in users)
            {
                if (s.id == id) { return s; }
            }
            Log.Log.For(this).Info("getById equals null");
            return null;

        }

        public void EditRecord(User record)
        {
            Log.Log.For(this).Info("Start method EditRecord");
            try
            {
                ConnectDC();
                SqlCommand command = new SqlCommand("UPDATE [AspNetUsers] SET [UserName]=@UserName, [Email]=@email, [PhoneNumber]=@phone WHERE ([AspNetUsers].[Id]=@id)", Connection);

                command.Parameters.Add(new SqlParameter("@UserName", record.UserName));
                if (record.email == null)
                {
                    command.Parameters.Add(new SqlParameter("@email", ""));
                }
                else
                {
                    command.Parameters.Add(new SqlParameter("@email", record.email));
                }
                if (record.phone == null)
                {
                    command.Parameters.Add(new SqlParameter("@phone", ""));
                }
                else
                {
                    command.Parameters.Add(new SqlParameter("@phone", record.phone));
                }
                command.Parameters.Add(new SqlParameter("@id", record.id));
                command.ExecuteNonQuery();

                command = new SqlCommand("SELECT [AspNetRoles].[Id] FROM [AspNetRoles] WHERE [AspNetRoles].[Name]=@role", Connection);
                command.Parameters.Add(new SqlParameter("@role", record.role));
                SqlDataReader reader = command.ExecuteReader();
                int id = -1;
                while (reader.Read())
                {

                    id = Convert.ToInt32(reader["Id"]);

                }
                reader.Close();


                command = new SqlCommand("UPDATE [AspNetUserRoles] SET [RoleId]=@id WHERE [AspNetUserRoles].[UserId]=@idUser", Connection);

                command.Parameters.Add(new SqlParameter("@id", id));
                command.Parameters.Add(new SqlParameter("@idUser", record.id));
                command.ExecuteNonQuery();

            }
            catch (Exception ex) { Log.Log.For(this).Error("EditRecord Falled " + ex); }
            finally
            {
                Disconnect();
            }

        }


        public List<string> GetRoles()
        {
            Log.Log.For(this).Info("Start method GetRoles");
            List<string> recordList = new List<string>();
            try
            {
                ConnectDC();


                SqlCommand command = new SqlCommand("Select [AspNetRoles].[Name] FROM [AspNetRoles]", Connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    recordList.Add(Convert.ToString(reader["Name"]));
                }
                reader.Close();
            }
            catch (Exception ex) { Log.Log.For(this).Error("GetRoles Falled " + ex); }
            finally { Disconnect(); }
            return recordList;
        }



        public void DeleteRecord(string id)
        {
            Log.Log.For(this).Info("Start method DeleteRecord");
            try
            {
                ConnectDC();
                SqlCommand command = new SqlCommand("DELETE FROM [AspNetUsers] WHERE Id = @Id", Connection);
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }
            catch (Exception ex) { Log.Log.For(this).Error("DeleteRecord Falled " + ex); }
            finally
            {
                Disconnect();
            }

        }

    }
}