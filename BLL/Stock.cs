﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class Stock
    {
        private readonly DAL.Stock dal = new DAL.Stock();



        public object List_Sales(string query)
        {
            return dal.list_Users(query);
        }





        //public object calculateInterest(string fileUrl, string rate, int userId)
        //{
        //    throw new NotImplementedException();
        //}

        public List<Model.ProfitInfo> calculateInterest(DataTable dt, string rate, int userId)
        {

            return dal.calculateInterest(dt, rate, userId);

          //  return new KeyValuePair<int, IList<Model.ProfitInfo>>(0, null);
               // return null;

           // return dal.List(whereclause, pageIndex, pageSize, sortName, sortType, search);
        }

        public KeyValuePair<int, IList<Model.ProfitInfo>> getProfit(int userid,int pageIndex,int pageSize,string startDate,string endDate)
        {
            return dal.getProfit(userid,pageIndex,pageSize,startDate,endDate);
        }

        public KeyValuePair<int, IList<Model.ProfitInfo>> getAllProfit(int pageIndex, int pageSize, string startDate,string endDate, int userid)
        {
            return dal.getAllProfit(pageIndex, pageSize, startDate,endDate, userid);
        }

        public bool checkData(int userId)
        {
           return dal.checkData(userId);
        }

        //public void Update(int id, double profit)
        //{
        //    dal.Update(id, profit);
        //}

        public KeyValuePair<int, IList<Model.StockInfo>> GetAllStock(int pageIndex, int pageSize, int userId)
        {
            return dal.getAllStock(pageIndex, pageSize, userId);
        }

        public Dictionary<string, string> GetUserInfo()
        {
            return dal.GetUserInfo();
        }

        public int Update(int userid, int stock, string sku)
        {
          return  dal.Update(userid, stock, sku);
        }
    }
}
