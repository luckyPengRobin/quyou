using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class SKU
    {


        private readonly DAL.SKU dal = new DAL.SKU();

        public KeyValuePair<int, IList<Model.SKUInfo>> List(string whereclause, int pageIndex, int pageSize, string sortName, string sortType, string search)
        {
            return dal.List(whereclause, pageIndex, pageSize, sortName, sortType, search);
        }



        public int Update(Model.SKUInfo sku)
        {
           // throw new NotImplementedException();
            return dal.Update(sku);
        }


        public IList<Model.SKUInfo> GetSKUInfo(string id)
        {
            return dal.GetSKUInfo(id);
        }


        public void Delete(int id)
        {
             dal.Delete(id);
        }
    }
}
