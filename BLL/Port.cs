using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Port
    {
        private readonly DAL.Port dal = new DAL.Port();

        public KeyValuePair<int, IList<Model.PortInfo>> List(string whereclause, int pageIndex, int pageSize, string sortName, string sortType, string search)
        {
            return dal.List(whereclause, pageIndex, pageSize, sortName, sortType, search);
        }

        public int Update(Model.PortInfo port)
        {
            return dal.Update(port);
        }

        public void Delete(int id)
        {
             dal.Delete(id);
        }
    }
}
