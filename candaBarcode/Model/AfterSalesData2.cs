using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace candaBarcode.Model
{
    public class AfterSalesData2 : INotifyPropertyChanged
    {
        private static string _FBillNo;
        private static string _Contact;
        private static string _ExpNumback;
        private model _Model;
        [JsonProperty("Creator")]
        public string Creator { get; set; }
        [JsonProperty("NeedUpDateFields")]
        public string[] NeedUpDateFields { get; set; }
        [JsonProperty("NeedReturnFields")]
        public string[] NeedReturnFields { get; set; }
        [JsonProperty("IsDeleteEntry")]
        public bool IsDeleteEntry { get; set; }
        [JsonProperty("SubSystemId")]
        public string SubSystemId { get; set; }
        [JsonProperty("Model")]
        public model Model {
            get { return _Model; }
            set {
                _Model = value;
                OnPropertyChanged("Model");
            }
        }
        public class model
        {
            /// <summary>
            /// 单据编号
            /// </summary>
            [JsonProperty("FBillNo")]
            public string FBillNo
            {
                get { return _FBillNo; }
                set
                {
                    _FBillNo = value;
                }
            }
            /// <summary>
            /// 联系人
            /// </summary>
            [JsonProperty("F_QiH_Contact")]
            public string Contact
            {
                get { return _Contact; }
                set
                {
                    _Contact = value;
                }
            }
            /// <summary>
            /// 退回单号
            /// </summary>
            [JsonProperty("F_XAY_ExpNumback")]
            public string ExpNumback
            {
                get { return _ExpNumback; }
                set
                {
                    _ExpNumback = value;
                }
            }
            /// <summary>
            /// 单据主键
            /// </summary>
            [JsonProperty("FID")]
            public string FID { get; set; }
            /// <summary>
            /// 退回分录
            /// </summary>
            [JsonProperty("FEntityDetection")]            
            public List<Detection> FEntityDetection { get; set; }            
        }
        
       
        public event PropertyChangedEventHandler PropertyChanged;
        public class Detection {
            /// <summary>
            /// 退回内件
            /// </summary>  
            [JsonProperty("F_XAY_REPRODUCT")]
            public string F_XAY_REPRODUCT { get; set; }
            /// <summary>
            /// 入库产品
            /// </summary>
            [JsonProperty("F_XAY_InstockMaterial")]
            public int F_XAY_InstockMaterial { get; set; }
            /// <summary>
            /// 批次
            /// </summary>
            [JsonProperty("F_XAY_Flot")]
            public string F_XAY_Flot { get; set; }
            /// <summary>
            /// 入库数量
            /// </summary>
            [JsonProperty("F_XAY_DetQty")]
            public int F_XAY_DetQty { get; set; }
            /// <summary>
            /// 是否入库
            /// </summary>            
            [JsonProperty("F_XAY_isInStock")]
            public int F_XAY_isInStock { get; set; }
            /// <summary>
            /// 退回仓库
            /// </summary>
            [JsonProperty("F_XAY_inStock")]
            public int F_XAY_inStock { get; set; }
            /// <summary>
            /// 故障类型
            /// </summary>
            [JsonProperty("F_QiH_Faulttypes")]
            public int F_QiH_Faulttypes { get; set; }
            /// <summary>
            /// 故障原因
            /// </summary>
            [JsonProperty("F_QiH_FalutReason")]
            public string F_QiH_FalutReason { get; set; }
            /// <summary>
            /// 处理方式
            /// </summary>
            [JsonProperty("F_XAY_WAY")]
            public int F_XAY_WAY { get; set; }
            /// <summary>
            /// 维修内容
            /// </summary>
            [JsonProperty("F_XAY_ServiceInf")]
            public string F_XAY_ServiceInf { get; set; }
            /// <summary>
            /// 寄回内件
            /// </summary>
            [JsonProperty("F_XAY_MaterialName")]
            public int F_XAY_MaterialName { get; set; }
            /// <summary>
            /// 出库打印
            /// </summary>
            [JsonProperty("F_XAY_OutMaterial")]
            public int F_XAY_OutMaterial {get; set; }
            /// <summary>
            /// 出库数量
            /// </summary>
            [JsonProperty("F_XAY_Qty")]
            public int F_XAY_Qty { get; set; }
            /// <summary>
            /// 出库仓库
            /// </summary>
            [JsonProperty("F_XAY_OutStock")]
            public int F_XAY_OutStock { get; set; }
            /// <summary>
            /// 是否出库
            /// </summary>
            [JsonProperty("F_XAY_isOutStock")]
            public int F_XAY_isOutStock {get; set;}
        }

        public AfterSalesData2() {
            NeedUpDateFields = new string[] { "F_XAY_REPRODUCT", "F_XAY_InstockMaterial", "F_XAY_Flot", "F_XAY_DetQty", "F_XAY_isInStock", "F_XAY_inStock",
                "F_QiH_Faulttypes", "F_QiH_FalutReason", "F_XAY_WAY", "F_XAY_ServiceInf", "F_XAY_MaterialName", "F_XAY_OutMaterial","F_XAY_Qty","F_XAY_OutStock","F_XAY_isOutStock"};
            Model = new model();
            Model.FEntityDetection = new List<Detection>();
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
