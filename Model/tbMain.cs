//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbMain
    {
        public int Id { get; set; }
        public string PNbr { get; set; }
        public string FBA { get; set; }
        public string Sku { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> OuterBoxDim { get; set; }
        public Nullable<decimal> OuterBoxQty { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public string DestPort { get; set; }
        public string ShipCat { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> Cdt { get; set; }
        public Nullable<System.DateTime> Udt { get; set; }
        public Nullable<decimal> FinalQty { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public Nullable<int> LSupplierId { get; set; }
        public Nullable<decimal> unitCost { get; set; }
    }
}