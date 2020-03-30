using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Npgsql;

namespace dbcon.connect
{
    public class pgsqldataget
    {
        private List<NpgsqlParameter> execParameters = new List<NpgsqlParameter>();
        public List<NpgsqlParameter> ExecParameters
        {
            get
            {
                return execParameters;
            }
            set { execParameters = value; }
        }

        public NpgsqlConnection connection_get()
        {
            NpgsqlConnection con = null;
            try
            {
                con = connect_get();

            }
            catch (Exception EX)
            {
                dbcon.Log.Logwrite("connection_get=" + EX.ToString());
            }
            return con;
        }

        public DataTable table(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                NpgsqlCommand com = new NpgsqlCommand(sql, connect_get());
                NpgsqlDataAdapter SqlDa = new NpgsqlDataAdapter(com);
                NpgsqlCommandBuilder comb = new NpgsqlCommandBuilder(SqlDa);
                
                if (ExecParameters.Count > 0)
                {
                    for (int i = 0; i < ExecParameters.Count; i++)
                    {
                        SqlDa.SelectCommand.Parameters.Add(ExecParameters[i]);
                    }
                }
                   
                SqlDa.Fill(dt);
                
                ExecParameters.Clear();
            }
            catch (Exception EX)
            {
                dbcon.Log.Logwrite("table=" + EX.ToString() + " Sql=" + sql);
            }
            return dt;
        }

        public int execute(string sql)
        {
            int durum = 0;
            try
            {

                NpgsqlCommand com = new NpgsqlCommand(sql, connect_get());
                if (ExecParameters.Count > 0)
                {
                    for (int i = 0; i < ExecParameters.Count; i++)
                    {
                        com.Parameters.Add(ExecParameters[i]);
                    }
                }
                durum = com.ExecuteNonQuery();
                ExecParameters.Clear();
            }
            catch (Exception EX)
            {
                dbcon.Log.Logwrite("ExecuteMetod=" + EX.ToString() + " Sql=" + sql);
            }

            return durum;
        }
        
         public DataTable table_transac(string sql, NpgsqlConnection con)
        {
            DataTable dt = new DataTable();
            try
            {
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataAdapter SqlDa = new NpgsqlDataAdapter(com);
                NpgsqlCommandBuilder comb = new NpgsqlCommandBuilder(SqlDa);
                if (ExecParameters.Count > 0)
                {
                    for (int i = 0; i < ExecParameters.Count; i++)
                    {
                        SqlDa.SelectCommand.Parameters.Add(ExecParameters[i]);
                    }
                }

                SqlDa.Fill(dt);
                ExecParameters.Clear();
            }
            catch (Exception EX)
            {
                dbcon.Log.Logwrite("table=" + EX.ToString() + " Sql=" + sql);
            }
            return dt;
        }
        public int execute_transac(string sql, NpgsqlConnection con)
        {
            int durum = 0;
            try
            {

                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                if (ExecParameters.Count > 0)
                {
                    for (int i = 0; i < ExecParameters.Count; i++)
                    {
                        com.Parameters.Add(ExecParameters[i]);
                    }
                }
                durum = com.ExecuteNonQuery();
                ExecParameters.Clear();
            }
            catch (Exception EX)
            {
                dbcon.Log.Logwrite("ExecuteMetod=" + EX.ToString() + " Sql=" + sql);
            }

            return durum;
        }
        public int calc_exec(string sql)
        {
            int maxrec = 0;
            try
            {
                NpgsqlConnection con = connect_get();
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader dRead = com.ExecuteReader();
                while (dRead.Read())
                {
                    maxrec = int.Parse("0"+dRead[0].ToString());
                }
                dRead.Close();
                dRead.Dispose();
            }
            catch (Exception EX)
            {
                dbcon.Log.Logwrite("maxrec=" + EX.ToString() + " Sql=" + sql);
            }
            return maxrec;
        }

       
        //Connect
        public NpgsqlConnection connect_get()
        {
            NpgsqlConnection Con = egeconnect1.CreateMysqlConnection(ConectionStringGet());
            return Con;
        }
        public static string BaseDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
        public static string ConectionStringGet()
        {
            string CStr = string.Empty;
            string DbParamsFile = BaseDirectory() + "connectionstring.txt";
            if (File.Exists(DbParamsFile))
                CStr = File.ReadAllText(DbParamsFile);

            return CStr;
        }
        public class egeconnect1
        {
            private static NpgsqlConnection sqlconn;
            private static NpgsqlConnection sqlconn1;
            protected egeconnect1() { }

            public static NpgsqlConnection CreateMysqlConnection(string strconn)
            {
                if (sqlconn1 != null)
                {
                    sqlconn1.Close();
                    sqlconn1.Dispose();
                }

                sqlconn1 = new NpgsqlConnection(strconn);

                if (sqlconn != null)
                {
                    if (sqlconn.Database.ToString().Trim() != sqlconn1.Database.ToString().Trim())
                    {
                        sqlconn.Close();
                        sqlconn.Dispose();
                        sqlconn = null;
                    }

                }
                ///////
                if ((sqlconn != null) && (sqlconn.State == ConnectionState.Broken))
                {
                    Dispose();
                }

                if (sqlconn == null)
                {
                    sqlconn = new NpgsqlConnection(strconn);
                    sqlconn.Open();

                }
                if (sqlconn.State == ConnectionState.Closed)
                {
                    sqlconn.Open();
                }
                /////
              
                return sqlconn;

            }

            static void Close()
            {
                if (egeconnect1.sqlconn != null &&
                    egeconnect1.sqlconn.State == System.Data.ConnectionState.Open)
                {
                    egeconnect1.sqlconn.Close();
                }
            }

            public static void Dispose()
            {
                if (egeconnect1.sqlconn != null)
                {
                    if (egeconnect1.sqlconn.State == System.Data.ConnectionState.Open)
                    {
                        egeconnect1.Close();
                    }
                    egeconnect1.sqlconn.Dispose();
                    egeconnect1.sqlconn = null;
                }
            }

            public static void Open()
            {
                if (egeconnect1.sqlconn.ConnectionString != null &&
                egeconnect1.sqlconn.State != System.Data.ConnectionState.Open)
                {
                    egeconnect1.sqlconn.Open();
                }
            }
        }


    }
}
