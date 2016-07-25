using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BapKBol.MODEL
{
    
    class MyAdapter:BaseAdapter<ItemsDes>
    {
        Activity act;List<ItemsDes> Ltm;
        public MyAdapter(Activity act,List<ItemsDes> Ltm):base()
        {
            this.act = act;
            this.Ltm = Ltm;
        }

        public override ItemsDes this[int position]
        {
            get
            {
                return Ltm[position];
            }
        }

        public override int Count
        {
            get
            {
                return Ltm.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View v = convertView ?? act.LayoutInflater.Inflate(Resource.Layout.list_view_lay, parent, false);
            v.FindViewById<TextView>(Resource.Id.textView1).Text = Ltm[position].Item;
            v.FindViewById<TextView>(Resource.Id.textView2).Text = Ltm[position].Price.ToString();
            v.FindViewById<TextView>(Resource.Id.textView3).Text = Ltm[position].Date;
            return v;
        }
    }
}