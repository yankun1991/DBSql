using MySql.Data.MySqlClient;
using SqlHelp;
using SqlHelp.Common;
using SqlHelp.Mysql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectTest();
            Console.ReadKey();
        }

        public static  void ConnectTest()
        {
           var _connect= DBConn.GetInstance();
            SchoolService ss = new SchoolService();
            object[] param = new object[1] { 1};
           var sql= ss.GetModels("","", param);
            Console.WriteLine();
        }
    }
}
