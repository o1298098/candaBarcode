﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace candaBarcode.Droid
{
   public class Myhandler:Handler
    {
        private Activity Activity;
        public Myhandler(Activity Activity)
        {
            this.Activity = Activity;
        }
        public override void HandleMessage(Message msg)
        {
            base.HandleMessage(msg);           
        }
    }
}