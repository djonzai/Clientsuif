using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientsuif
{
    class Connexion
    {
        public SQLiteCommand cmd =new SQLiteCommand();
        public SQLiteCommandBuilder cmdb =new SQLiteCommandBuilder();
        public SQLiteDataAdapter dta =new SQLiteDataAdapter();
        public SQLiteConnection cnx =new SQLiteConnection();
        public SQLiteDataReader dtr ;
        public string str = @"data source=C:\Users\ADELPHE\Documents\sqlite test\DBCPT.db";
        //public string str = @"Data Source=C:\Users\" + Environment.UserName + "\\Documents\\DBCPT.db";
        public void connectionopen()
        {
            if (cnx.State==ConnectionState.Closed)
            {
                SQLiteConnection cnx = new SQLiteConnection();
                cnx.ConnectionString = str;
                cnx.Open();
            }
           
        }
        


    }
}
