using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Data;

namespace Louie_Remaster
{
    class sqlConnector
    {
        private SqlConnection con;
        public bool hasSetup = false;

        public void Setup(string dataSource, string initialCatalog, string username, string password)
        {
            string connectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3};", dataSource, initialCatalog, username, password);
            con = new SqlConnection(connectionString);

            try
            {
                con.Open();
                con.Close();
                hasSetup = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(("There was an error connecting to the server, " + e.Message));
            }
        }

        public void Setup()
        {
            var password = File.ReadAllText("Database.txt");
            this.Setup("den1.mssql2.gear.host", "discordcount", "discordcount", password);
        }

        public void Execute(string sqlString)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlString, con);
            cmd.ExecuteScalar();
            con.Close();
        }

        public void Close()
        {
            con.Close();
        }

        public string GetSingleValue(string sqlString)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlString, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string returnData;
            returnData = reader[0].ToString();
            con.Close();
            return returnData;
        }

        public DataTable GetDataTable(string sqlString)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlString, con);
            SqlDataAdapter reader = new SqlDataAdapter(cmd);
            DataTable returnData = new DataTable();
            reader.Fill(returnData);
            con.Close();
            return returnData;
        }

        public List<string> GetTableSchema()
        {
            con.Open();
            List<string> returnlist = new List<string>();
            DataTable results = new DataTable();
            results = con.GetSchema("Tables");

            foreach (DataRow row in results.Rows)
            {
                string tablename = (string)row[2];
                returnlist.Add(tablename);
            }
            con.Close();
            return returnlist;
        }
    }
}
