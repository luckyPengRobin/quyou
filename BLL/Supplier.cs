using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Supplier
    {
        private readonly DAL.Supplier dal = new DAL.Supplier();

        public KeyValuePair<int, IList<Model.SupplierInfo>> List(string whereclause, int pageIndex, int pageSize, string sortName, string sortType, string search)
        {
            return dal.List(whereclause, pageIndex, pageSize, sortName, sortType, search);
        }

        public IList<Model.SupplierInfo> GetSupplierInfo(string id)
        {
            return dal.GetSupplierInfo(id);
        }

        public int Update(Model.SupplierInfo supplier)
        {
            return dal.Update(supplier);
        }

        public void Delete(int id)
        {
            dal.Delete(id);
        }
    }
}
