using System;
using System.Linq;
using Focus.Business.Interface;
using Focus.Domain.Entities;

namespace Focus.Business.WareHouseTransfers.Queries.GetWareHouseTransferList
{

    public class WareHouseTransferLookUpModel 
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string FromWareHouse { get; set; }
        public string ToWareHouse { get; set; } 
        public string FromBranch { get; set; }
        public string ToBranch { get; set; }
        public string Date { get; set; }


     
        //public static WareHouseTransferLookUpModel Create(WareHouseTransfer wareHouseTransferOrder)
        //{
          
        //    var lookUpModel = new WareHouseTransferLookUpModel
        //    {
        //        Id = wareHouseTransferOrder.Id,
        //        Code= wareHouseTransferOrder.Code,
        //        FromWareHouse = wareHouseTransferOrder.Warehouse.Name,
        //        ToWareHouse  = wareHouseTransferOrder.Warehouse.Name,
        //        Date = wareHouseTransferOrder.Date.ToString("dd/MM/yyyy")
        //    };
        //    return lookUpModel;
        //}
    }
}
