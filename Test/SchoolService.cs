using SqlHelp.Mysql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
  public  class SchoolService: DbOperation<School>
    {
        public SchoolService():base() {

        }
        public School GetModel(int id)
        {
           return base.GetModelByPkId<School>(id);
        }

        public void AddModel(School sc)
        {
           Console.WriteLine(base.AddModel<School>(sc));
        }

        public IEnumerable<School> GetModels(string where, string orderby, object[] param)
        {
            return base.GetModels<School>( where,  orderby, param);
        }

        public void GetModelByPage(string where, string orderby, int pageSize, int pageIndex, object[] param)
        {
            Console.WriteLine(base.GetModelByPage<SchoolService>(where,  orderby,  pageSize,  pageIndex, param));
        }
    }
}
