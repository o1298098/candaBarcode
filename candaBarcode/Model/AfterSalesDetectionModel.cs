using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace candaBarcode.Model
{
   public class AfterSalesDetectionModel : INotifyPropertyChanged
    {
        
        private string _product;
        private string _typet;
        private string _typew;
        private string _instock;
        private string _outstock;
        /// <summary>
        /// 退回内件
        /// </summary>  
        [JsonProperty("F_XAY_REPRODUCT")]
        public basefid F_XAY_REPRODUCT { get; set; }
        /// <summary>
        /// 入库产品
        /// </summary>
        [JsonProperty("F_XAY_InstockMaterial")]
        public basemid F_XAY_InstockMaterial { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [JsonProperty("F_XAY_Flot")]
        public string F_XAY_Flot { get; set; }
        /// <summary>
        /// 入库数量
        /// </summary>
        [JsonProperty("F_XAY_DetQty")]
        public string F_XAY_DetQty { get; set; }
        /// <summary>
        /// 是否入库
        /// </summary>            
        [JsonProperty("F_XAY_isInStock")]
        public long F_XAY_isInStock { get; set; }
        /// <summary>
        /// 退回仓库
        /// </summary>
        [JsonProperty("F_XAY_inStock")]
        public basenum F_XAY_inStock { get; set; }
        /// <summary>
        /// 故障类型
        /// </summary>
        [JsonProperty("F_QiH_Faulttypes")]
        public basenum F_QiH_Faulttypes { get; set; }
        /// <summary>
        /// 故障原因
        /// </summary>
        [JsonProperty("F_QiH_FalutReason")]
        public string F_QiH_FalutReason { get; set; }
        /// <summary>
        /// 处理方式
        /// </summary>
        [JsonProperty("F_XAY_WAY")]
        public basenum F_XAY_WAY { get; set; }
        /// <summary>
        /// 维修内容
        /// </summary>
        [JsonProperty("F_XAY_ServiceInf")]
        public string F_XAY_ServiceInf { get; set; }
        /// <summary>
        /// 寄回内件
        /// </summary>
        [JsonProperty("F_XAY_MaterialName")]
        public basefid F_XAY_MaterialName { get; set; }
        /// <summary>
        /// 出库打印
        /// </summary>
        [JsonProperty("F_XAY_OutMaterial")]
        public basemid F_XAY_OutMaterial { get; set; }
        /// <summary>
        /// 出库数量
        /// </summary>
        [JsonProperty("F_XAY_Qty")]
        public string F_XAY_Qty { get; set; }
        /// <summary>
        /// 出库仓库
        /// </summary>
        [JsonProperty("F_XAY_OutStock")]
        public basenum F_XAY_OutStock { get; set; }
        /// <summary>
        /// 是否出库
        /// </summary>
        [JsonProperty("F_XAY_isOutStock")]
        public long F_XAY_isOutStock { get; set; }
        public string F_XAY_Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged("F_XAY_Product");
            }
        }
       
        /// <summary>
        /// 故障类型文本
        /// </summary>
        public string typet
        {
            get { return _typet; }
            set
            {
                _typet = value;
                OnPropertyChanged("typet");
            }
        }
        /// <summary>
        /// 处理方式文本
        /// </summary>
        public string typew
        {
            get { return _typew; }
            set
            {
                _typew = value;
                OnPropertyChanged("typew");
            }
        }
       
        /// <summary>
        /// 入库仓库文本
        /// </summary>
        public string instock
        {
            get { return _instock; }
            set
            {
                _instock = value; 
                OnPropertyChanged("instock");
            }
        }
        /// <summary>
        /// 出库仓库文本
        /// </summary>
        public string outstock
        {
            get { return _outstock; }
            set
            {
                _outstock = value;
                OnPropertyChanged("outstock");
            }
        }
        public class basenum
        {
            /// <summary>
            /// 编码
            /// </summary>
            public string FNumber { set; get; }
        }
        public class basemid
        {
            /// <summary>
            /// 物料ID
            /// </summary>
            public string FMaterialID { set; get; }
        }
        public class basefid
        {
            /// <summary>
            /// 标准ID
            /// </summary>
            public string FID { set; get; }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       public AfterSalesDetectionModel() {
            F_QiH_Faulttypes = new basenum() {FNumber="s" };
            F_XAY_inStock = new basenum() { FNumber = "s" };
            F_XAY_WAY = new basenum() { FNumber = "s" };
            F_XAY_OutStock = new basenum() { FNumber = "s" };
            F_XAY_REPRODUCT = new basefid() { FID = "0" };
            F_XAY_InstockMaterial = new basemid() { FMaterialID = "0" };
            F_XAY_MaterialName = new basefid() { FID = "0" };
            F_XAY_OutMaterial = new basemid() { FMaterialID = "0" };
            F_XAY_Product = "";
        }
    }
}
