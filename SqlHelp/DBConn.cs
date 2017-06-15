using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace SqlHelp
{
    public class DBConn
    {
        //private static  MySqlConnection _connect=new MySqlConnection("server=localhost;user id=root;persistsecurityinfo=True;database=yk_test");

        //public static MySqlConnection GetInstance()
        //{
        //    return _connect;
        //}
        private static MySqlConnection _connect=null;
        private  static object obj = new object();

        public static MySqlConnection GetInstance()
        {
            lock (obj)
            {
                if (_connect == null)
                {
                    lock (obj)
                    {
                        try
                        {
                            _connect= new MySqlConnection(ConfigurationManager.ConnectionStrings["mySqlconn"].ToString());
                            return _connect;
                        }
                        catch (Exception ex)
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return _connect;
                }
            }
        }


    }
}
