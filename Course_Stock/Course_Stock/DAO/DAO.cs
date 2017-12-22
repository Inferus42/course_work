using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Course_Stock.DAO
{
    public class DAO
    {

        string conSCS = WebConfigurationManager.ConnectionStrings["SCS"].ConnectionString;
        string conDC = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        protected SqlConnection Connection { get; set; }

        public void ConnectSCS()
        {
            Log.Log.For(this).Info("Open connect with base " + conSCS);
            Connection = new SqlConnection(conSCS);
            Connection.Open();
        }
        public void ConnectDC()
        {
            Log.Log.For(this).Info("Open connect with base " + conDC);
            Connection = new SqlConnection(conDC);
            Connection.Open();
        }

        public void Disconnect()
        {
            Log.Log.For(this).Info("Close connect");
            Connection.Close();
        }
    }
}