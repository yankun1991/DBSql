using MySql.Data.MySqlClient;
using SqlHelp.Attr;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelp.Mysql
{
   public class DbOperation<M>
    {
        private  string TableName = string.Empty;

        private SQLExecute sqlE = new SQLExecute();

        public DbOperation()
        {
            TableName = GetTableName<M>();
        }

        /// <summary>
        /// 根据主键查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetModelByPkId<T>(int id)
        {
            Type type = typeof(T);
            StringBuilder sql = new StringBuilder();
            string pk = PrimaryKey<T>();
            sql.AppendFormat(" select * from {0} where {1}={2} limit 1 ", TableName, pk, id);
           return sqlE.SelectDataOne<T>(sql.ToString());
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<T> GetModels<T>(string where,string orderby,object[] param)
        {
            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(where))
            {
                if (param != null && param.Length > 0)
                {
                    for (var i = 0; i < param.Length; i++)
                    {
                        var index = where.IndexOf('?');
                        if (index > 0)
                        {
                            where = where.Remove(index, 1);
                            if (param[i].GetType() == typeof(int) || param[i].GetType() == typeof(long))
                            {
                                where = where.Insert(index, param[i].ToString());
                            }
                            else
                            {
                                where = where.Insert(index, (string)param[i]);
                            }
                        }
                    }
                    sql.AppendFormat(" select * from {0} where {1}", TableName, where);
                }
                else
                {
                    sql.AppendFormat(" select * from {0} where {1}", TableName, where);
                }
            }
            else
            {
                sql.AppendFormat(" select * from {0}", TableName);
            }
            if (!string.IsNullOrWhiteSpace(orderby))
            {
                sql.Append(orderby);
            }
            return sqlE.SelectDataList<T>(sql.ToString());
        }

        public string GetModelByPage<T>(string where,string orderby, int pageSize, int pageIndex, object[] param)
        {
            StringBuilder sqlOne = new StringBuilder();
            StringBuilder sqlTwo = new StringBuilder();
            int ps = 0;
            if (pageIndex > 0)
            {
                ps = (pageIndex - 1) * pageSize;
            }
            if (param != null && param.Length > 0)
            {
                for (var i = 0; i < param.Length; i++)
                {
                    var index = where.IndexOf('?');
                    if (index > 0)
                    {
                        where = where.Remove(index, 1);
                        if (param[i].GetType() == typeof(int) || param[i].GetType() == typeof(long))
                        {
                            where = where.Insert(index, param[i].ToString());
                        }
                        else
                        {
                            where = where.Insert(index, (string)param[i]);
                        }
                    }
                }
                sqlOne.AppendFormat(" select * from {0} where {1} LIMIT {2},{3}", TableName, where,ps,pageSize);
                sqlTwo.AppendFormat(" select * from {0} where {1}", TableName, where);
            }
            else
            {
                sqlOne.AppendFormat(" select * from {0} where {1} LIMIT {2},{3}", TableName, where,ps,pageSize);
                sqlTwo.AppendFormat(" select * from {0} where {1}", TableName, where);
            }
            if (!string.IsNullOrWhiteSpace(orderby))
            {
                sqlOne.Append(orderby);
            }
            return sqlOne.ToString()+"|"+sqlTwo.ToString();
        }

        /// <summary>
        /// 根据主键删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteModelByPrimaryKey<T>(int id)
        {
            StringBuilder sql = new StringBuilder();
            string pk = PrimaryKey<T>();
            sql.AppendFormat(" delete from {0} where {1}={2} ", TableName, pk, id);
            return sql.ToString();
        }

        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string DeleteModelByWhere<T>(string where, object[] param)
        {
            StringBuilder sql = new StringBuilder();
            if (param != null && param.Length > 0)
            {
                for (var i = 0; i < param.Length; i++)
                {
                    var index = where.IndexOf('?');
                    if (index > 0)
                    {
                        where = where.Remove(index, 1);
                        if (param[i].GetType() == typeof(int) || param[i].GetType() == typeof(long))
                        {
                            where = where.Insert(index, param[i].ToString());
                        }
                        else
                        {
                            where = where.Insert(index, (string)param[i]);
                        }
                    }
                }
                sql.AppendFormat(" delete from  {0} where {1}", TableName, where);
            }
            else
            {
                sql.AppendFormat(" delete from {0} where {1}", TableName, where);
            }
            return sql.ToString();

        }

        /// <summary>
        /// 数据新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddModel<T>(T model)
        {
            Type type = typeof(T);
            StringBuilder sql = new StringBuilder();
            string propers = GetPeopers<T>();
            string value = GetVaules<T>(model);
            sql.AppendFormat("insert into {0} ({1}) value({2})", TableName, propers, value);
            return sql.ToString();
        }

        /// <summary>
        /// 数据更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public string UpdateMode<T>(T model)
        {
            StringBuilder sql = new StringBuilder();
            string peopersAndValue = GetPeopersAndValue<T>(model);
            string primaryInfo = GetPrimaryInfo<T>(model);
            sql.AppendFormat(" update {0} set {1} where {2}", TableName, peopersAndValue, primaryInfo);
            return sql.ToString();
        }

        /// <summary>
        /// 获取对象属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private string GetPeopers<T>()
        {
            StringBuilder sql = new StringBuilder();
            Type type = typeof(T);
            foreach(var item in type.GetProperties())
            {
                if(!IsPrimaryKey<T>(item.Name))
                {
                    sql.Append(item.Name+",");
                }
            }
            string temp = sql.ToString();
            return temp.Trim(',');
        }

        /// <summary>
        /// 获取 字段=值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetPeopersAndValue<T>(T model)
        {
            StringBuilder sql = new StringBuilder();
            Type type = model.GetType();
            foreach (var item in type.GetProperties())
            {
                if (!IsPrimaryKey<T>(item.Name))
                {
                    if (item.PropertyType == typeof(int) || item.PropertyType == typeof(long))
                    {
                        sql.AppendFormat("{0}={1},",item.Name,item.GetValue(model, null));
                    }
                    else
                    {
                        sql.AppendFormat("{0}='{1}',", item.Name, item.GetValue(model, null));
                    }
                }
            }
            string temp = sql.ToString();
            return temp.Trim(',');
        }

        /// <summary>
        /// 获取对象的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        private string GetVaules<T>(T model)
        {
            StringBuilder sql = new StringBuilder();
            Type type = model.GetType();
            foreach (var item in type.GetProperties())
            {
                if (!IsPrimaryKey<T>(item.Name))
                {
                    if (item.PropertyType == typeof(int) || item.PropertyType == typeof(long))
                    {
                        sql.Append(item.GetValue(model, null) + ",");
                    }
                    else
                    {
                        sql.AppendFormat("'{0}',", item.GetValue(model, null));
                    }
                }
            }
            string temp = sql.ToString();
            return temp.Trim(',');
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private string GetTableName<T>()
        {
            Type type = typeof(T);
            string tableName = string.Empty;
            object[] objs = type.GetCustomAttributes(typeof(TableAttribute), true);
            foreach (object obj in objs)
            {
                TableAttribute attr = obj as TableAttribute;
                if (attr != null)
                {

                    tableName = attr.TableName;//表名只有获取一次
                    break;
                }
            }
            return tableName;
        }

        /// <summary>
        /// 获取主键
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private string PrimaryKey<T>()
        {
            string pk = string.Empty;
            Type type = typeof(T);
            foreach (var item in type.GetProperties())
            {
                object[] objAttrs = item.GetCustomAttributes(typeof(PriamKeyAttribute), true);
                if (objAttrs.Length > 0)
                {
                    PriamKeyAttribute attr = objAttrs[0] as PriamKeyAttribute;
                    if (attr != null)
                    {
                        pk = item.Name;
                    }
                }

            }
            return pk;
        }

         /// <summary>
         /// 获取主键键值对  键=值
         /// </summary>
         /// <typeparam name="T"></typeparam>
         /// <param name="model"></param>
         /// <returns></returns>
        public string GetPrimaryInfo<T>(T model)
        {
            StringBuilder sql = new StringBuilder();
            Type type = model.GetType();
            foreach (var item in type.GetProperties())
            {
                if (IsPrimaryKey<T>(item.Name))
                {
                    if (item.PropertyType == typeof(int) || item.PropertyType == typeof(long))
                    {
                        sql.AppendFormat("{0}={1}", item.Name, item.GetValue(model, null));
                    }
                    else
                    {
                        sql.AppendFormat("{0}='{1}'", item.Name, item.GetValue(model, null));
                    }
                    break;
                }
            }
            string temp = sql.ToString();
            return temp;
        }

        /// <summary>
        /// 判断是否为主键
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsPrimaryKey<T>(string name)
        {
            Type type = typeof(T);
            foreach (var item in type.GetProperties())
            {
                object[] objAttrs = item.GetCustomAttributes(typeof(PriamKeyAttribute), true);
                if (objAttrs.Length > 0)
                {
                    PriamKeyAttribute attr = objAttrs[0] as PriamKeyAttribute;
                    if (attr != null)
                    {
                         if(item.Name.Equals(name))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
