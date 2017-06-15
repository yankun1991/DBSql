using MySql.Data.MySqlClient;
using SqlHelp.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelp.Mysql
{
    class SQLExecute: DBConn
    {
        private MySqlConnection _connect = null;

        public SQLExecute():base()
        {
            _connect = GetInstance();
        }
        public T SelectDataOne<T>(string sql)
        {
            Type type = typeof(T);
            object obj = Activator.CreateInstance(type);
            _connect.Open();
            MySqlDataReader dataReader = null;
            MySqlCommand command = null;
            try
            {
                command = _connect.CreateCommand();
                command.CommandText = sql;
                dataReader = command.ExecuteReader();
                obj = Reader2Model.ReaderToModel<T>(dataReader);
                return (T)obj;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (!dataReader.IsClosed)
                {
                    dataReader.Close();
                }
                if (_connect.State == ConnectionState.Open)
                {
                    _connect.Close();
                }
            }
            return default(T);
        }

        public IEnumerable<T> SelectDataList<T>(string sql)
        {
            _connect.Open();
            MySqlDataReader dataReader = null;
            MySqlCommand command = null;
            try
            {
                command = _connect.CreateCommand();
                command.CommandText = sql;
                dataReader = command.ExecuteReader();
                IList<T> result = Reader2Model.ReaderToList<T>(dataReader);
                return result;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (!dataReader.IsClosed)
                {
                    dataReader.Close();
                }
                if (_connect.State == ConnectionState.Open)
                {
                    _connect.Close();
                }
            }
            return null;
        }
    }
}
