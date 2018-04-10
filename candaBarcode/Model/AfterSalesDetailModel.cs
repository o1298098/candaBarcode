using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace candaBarcode.Model
{
   public  class AfterSalesDetailModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _product;
        private string _batch;
        private string _number;
        private string _typet;
        private string _reasont;
        private string _typew;
        private string _infow;
        private string _instock;
        private string _outstock;
        /// <summary>
        /// 入库产品
        /// </summary>
        public string product {
            get { return _product; }
            set { _product = value;
                OnPropertyChanged("product");
            }
        }
        /// <summary>
        /// 批次
        /// </summary>
        public string batch
        {
            get { return _batch; }
            set
            {
                _batch = value;
                OnPropertyChanged("batch");
            }
        }
        /// <summary>
        /// 入库数量
        /// </summary>
        public string number
        {
            get { return _number; }
            set
            {
                _number = value;
                OnPropertyChanged("number");
            }
        }
        /// <summary>
        /// 故障类型
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
        /// 处理方式
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
        /// 故障原因
        /// </summary>
        public string reasont
        {
            get { return _reasont; }
            set
            {
                _reasont = value;
                OnPropertyChanged("reasont");
            }
        }
        /// <summary>
        /// 维修内容
        /// </summary>
        public string infow
        {
            get { return _infow; }
            set
            {
                _infow = value;
                OnPropertyChanged("infow");
            }
        }
        /// <summary>
        /// 入库仓库
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
        /// 出库仓库
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

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
