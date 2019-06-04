using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace BLL
{
   public class Purchse
    {
       public readonly static Purchse Instance = new Purchse();
       private readonly DAL.Purchse dal = new DAL.Purchse();

       /// <summary>
       /// 运营操作事件
       /// </summary>
       /// <param name="obj"></param>
       /// <param name="actType"></param>
       /// <param name="notes"></param>
       /// <returns></returns>
       public bool act_yy (Model.tbPO obj,int BPId,string actType,string notes)
       {
           return dal.act_yy(obj, BPId, actType, notes);
       }
        public Model.clsDefine.retutnMsg task_Act(Model.tbPO obj, int BPId, string actType, string notes)
       {
           return dal.task_Act(obj, BPId, actType, notes);
       }
       /// <summary>
       ///  检查采购单号是否存在
       /// </summary>
       /// <param name="pnbr"></param>
       /// <returns></returns>
       public bool Is_exist_PO(string pnbr)
       {
           return dal.Is_exist_PO(pnbr);
       }

        /// <summary>
       /// 删除指定采购单下的一条数据
       /// </summary>
       /// <param name="obj"></param>
       public void delete_po(Model.tbMain obj, int userId)
       {
           dal.delete_po(obj, userId);
       }

       /// <summary>
       /// 保存采购单
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
       public string Add(Model.tbMain obj)
       {
           return dal.Add_tbMain(obj);
       }
       /// <summary>
       /// 批量增加采购量
       /// </summary>
       /// <param name="lstobj"></param>
       /// <returns></returns>
       public string Add_tbMain_list(IList<Model.tbMain> lstobj)
       {
           return dal.Add_tbMain_list(lstobj);
       }

        public Model.tbPO Get_PO(string pnbr)
       {
           return dal.Get_PO(pnbr);
       }
        public Model.tbPO Get_PO_byId(int Id)
        {
            return dal.Get_PO_By_Id(Id);
        }

       /// <summary>
       /// 根据tbBP.Id 或PNbr号获取
       /// </summary>
       /// <param name="pnbr"></param>
       /// <param name="BPId"></param>
       /// <returns></returns>
        public Model.TskForDisplay Get(string pnbr, int BPId)
        {
            return dal.Get(pnbr,BPId);
        }
       /// <summary>
       /// 显示采购单基本信息
       /// </summary>
       /// <param name="pnbr"></param>
       /// <returns></returns>
        public Model.IsTskInLog Get_by_PNbr(string pnbr, int userId)
        {
            return dal.Get_by_PNbr(pnbr, userId);
        }

              /// <summary>
       /// 获取指定采购单号的详细信息
       /// </summary>
       /// <param name="PNbr"></param>
       /// <returns></returns>
        public IList<Model.tbMain> List_tbMain(string PNbr)
        {
            return dal.List_tbMain(PNbr);
        }
       /// <summary>
       /// 更新实际采购数量
       /// </summary>
       /// <param name="Id"></param>
       /// <param name="qty"></param>
       public void update_FinalQty(int Id,string qty)
        {
            dal.update_FinalQty(Id, qty);
        }
        /// <summary>
        /// 获取指定采购单号的详细信息
        /// </summary>
        /// <param name="PNbr"></param>
        /// <returns></returns>
        public IList<Model.sp_proc_Read_PO_Result> List_PO(string PNbr)
        {
            return dal.List_PO(PNbr);
        }

       public IList<Model.TskForDisplay> list_tasks(int userId, string tskName, int pageIndex, int pageSize)
       {
           return dal.list_tasks(userId, tskName, pageIndex, pageSize);
       }

          /// <summary>
       /// 根據BPID和TSK信息获取历史操作记录
       /// </summary>
       /// <param name="PNbr"></param>
       /// <returns></returns>
       public IList<Model.TskForDisplay> Get_Log_By_tsk(int BPId, string tsk)
       {
           return dal.Get_Log_By_tsk(BPId, tsk);
       }
        public string Add_PO(DataTable tb,string PNbr,string actType)
       {
           return dal.Add_PO(tb, PNbr, actType);
       }

       public string Get_PO_Amount(string pnbr)
        {
            return dal.Get_PO_Amount(pnbr);
        }
              /// <summary>
       /// 更新货运公司信息
       /// </summary>
       /// <param name="mainId"></param>
       /// <param name="supplierId"></param>
       public void update_LgstcSupplier(int mainId,int supplierId)
       {
           dal.update_LgstcSupplier(mainId, supplierId);
       }
       public IList<Model.TskForDisplay> SearchList(string pnbr,string lnbr,string status,object userId, int pageIndex, int pageSize)
       {
           return dal.SearchList(pnbr, lnbr, status, userId, pageIndex, pageSize);
       }
       /// <summary>
       /// 删除指定采购单
       /// </summary>
       /// <param name="pnbr"></param>
       /// <param name="userId"></param>
       public void delPO(string pnbr, int userId)
       {
           dal.delPO(pnbr, userId);
       }
    }
}
