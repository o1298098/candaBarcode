﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using candaBarcode.Droid.model;

namespace candaBarcode.Droid
{
    public class SearchAdapter : BaseAdapter<EmsNum>
    {
        List<EmsNum> Items;
        Activity context;


        public SearchAdapter(Activity context, List<EmsNum> items) : base()
        {
            this.context = context;
            this.Items = items;
        }


        public override long GetItemId(int position)
        {
            return position;
        }
        public override EmsNum this[int position]
        {
            get { return Items[position]; }
        }
        public override int Count
        {
            get { return Items.Count; }
        }
        //public override void NotifyDataSetChanged()
        //{
        //    base.NotifyDataSetChanged();
        //}
        public void refresh(List<EmsNum> list)
        {
            Items = list;
            NotifyDataSetChanged();
        }
      
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.SearchAdapter, null);
            view.FindViewById<TextView>(Resource.Id.EMSNUM).Text=Items[position].EMSNUM;
            view.FindViewById<TextView>(Resource.Id.state).Text = Items[position].state;
            view.FindViewById<TextView>(Resource.Id.ScanDate).Text = Items[position].datetime;
            //if (holder == null)
            //{
            //    holder = new ListAdapterViewHolder();
            //    var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
            //    //replace with your item and your holder items
            //    //comment back in
            //    //view = inflater.Inflate(Resource.Layout.item, parent, false);
            //    //holder.Title = view.FindViewById<TextView>(Resource.Id.text);
            //    view.Tag = holder;
            //}


            //fill in your items
            //holder.Title.Text = "new text here";

            return view;
        }

       
        
      
    }

   
}