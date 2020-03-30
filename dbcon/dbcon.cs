using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Npgsql;

namespace dbcon
{
    public class mssql
    {
        dbcon.connect.mssqldataget db;
        public mssql()
        {
            dbcon.connect.mssqldataget db = new connect.mssqldataget();
        }

        public SqlConnection connection_get()
        {
            return db.connection_get();
        }

        public DataTable table(string sql)
        {
            return db.table(sql);
        }

        public int execute(string sql)
        {
            return db.execute(sql);
        }

        public DataTable table_transac(string sql, SqlConnection con)
        {
            return db.table_transac(sql, con);
        }

        public int execute_transac(string sql, SqlConnection con)
        {
            return db.execute_transac(sql, con);
        }

        public int calc_exec(string sql)
        {
            return db.execute(sql);
        }
    }
    public class mysql
    {
        dbcon.connect.mysqldataget db;
        public mysql()
        {
            dbcon.connect.mysqldataget db = new connect.mysqldataget();
        }

        public MySqlConnection connection_get()
        {
            return db.connection_get();
        }
        public DataTable table(string sql)
        {
            return db.table(sql);
        }

        public int execute(string sql)
        {
            return db.execute(sql);
        }

        public DataTable table_transac(string sql, MySqlConnection con)
        {
            return db.table_transac(sql, con);
        }

        public int execute_transac(string sql, MySqlConnection con)
        {
            return db.execute_transac(sql, con);
        }

        public int calc_exec(string sql)
        {
            return db.execute(sql);
        }


    }

    public class pgsql
    {
        dbcon.connect.pgsqldataget db;
        public pgsql()
        {
            dbcon.connect.pgsqldataget db = new connect.pgsqldataget();
        }
        public NpgsqlConnection connection_get()
        {
            return db.connection_get();
        }
        public DataTable table(string sql)
        {
            return db.table(sql);
        }

        public int execute(string sql)
        {
            return db.execute(sql);
        }

        public DataTable table_transac(string sql, NpgsqlConnection con)
        {
            return db.table_transac(sql, con);
        }

        public int execute_transac(string sql, NpgsqlConnection con)
        {
            return db.execute_transac(sql, con);
        }

        public int calc_exec(string sql)
        {
            return db.execute(sql);
        }
    }
}
