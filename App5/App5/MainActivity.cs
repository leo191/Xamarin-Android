using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using BapKBol.MODEL;
using System.Collections.Generic;
using Android.Support.V7.App;
using AToolBar = Android.Support.V7.Widget.Toolbar;
using Android.Preferences;

namespace BapKBol
{
    [Activity(Label = "BapKBol", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {


        
        ListView lv;
        List<ItemsDes> litm = new List<ItemsDes>();
        List<ItemsDes> sltm = new List<ItemsDes>();
        DataBase db;
        TextView Textv;

        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            var toolbar = FindViewById<AToolBar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "BapKBol";
            
           

            

            db = new DataBase();
            db.CreateDB();
            

            var getItm = FindViewById<EditText>(Resource.Id.itemName_et);
            var getPri = FindViewById<EditText>(Resource.Id.price_et);
            var add_btn = FindViewById<Button>(Resource.Id.addList_btn);
            var show_btn = FindViewById<Button>(Resource.Id.showAll_btn);
            Textv= FindViewById<TextView>(Resource.Id.mnth_amnt_tv);
            lv = FindViewById<ListView>(Resource.Id.listView1);
            Textv.Text = "Monthly Amount";
            var prefs = Application.Context.GetSharedPreferences("BapKBol", FileCreationMode.Private);
            var settings = prefs.GetString("amount", null);
            if (settings != null) { LoadData(); }

            toolbar.MenuItemClick += Toolbar_MenuItemClick;

            
            show_btn.Click += (e, o) =>
              {
                  if (settings != null) { LoadData(); }

              };



            add_btn.Click += (o, e) =>
             {
                 if (retriveSettings() == null) { Toast.MakeText(this, "Set The Initial Amount", ToastLength.Short).Show(); return; }
                 ItemsDes tm = tm = new ItemsDes(); 
                 if (getItm.Text!="" && getPri.Text!="")
                 {
                     tm.Item = getItm.Text;
                     tm.Price = int.Parse(getPri.Text);
                     tm.Date = DateTime.Now.Date.ToString();
       
                 }
                 else
                 {
                     Toast.MakeText(this, "Empty Fields not Allowed", ToastLength.Long).Show();
                     return;
                 }
                 sltm.Add(tm);
                 if (db.InsertInTable(tm))
                     Toast.MakeText(this, "Saved", ToastLength.Short).Show();
                 var adptr = new MyAdapter(this, sltm);
                 lv.Adapter = adptr;
                 getItm.Text = "";
                 getPri.Text = "";
                 getItm.RequestFocus();
                 setText();
             };



            
            

        }

        private void Toolbar_MenuItemClick(object sender, AToolBar.MenuItemClickEventArgs e)
        {
            switch(e.Item.ItemId)
            {
                case Resource.Id.setAmount:
                    {
                        var intent = new Intent(this, typeof(Set));
                        StartActivity(intent);
                    }break;
                case Resource.Id.help:
                    { Toast.MakeText(this, "Parbo na korte sorry", ToastLength.Long).Show(); }
                    break;
                
            }
        }

        private void LoadData()
        {
            litm.Clear();
            litm = db.SelectFromTable();
            if (litm == null)
            { Toast.MakeText(this, "No Data", ToastLength.Short).Show() ; return; }



            setText();
            
            var adptr = new MyAdapter(this,litm);
            lv.Adapter = adptr;
        }

        private void setText()
        {
            Textv.Text = "₹ " + Calc(litm).ToString();

        }

        private int Calc(List<ItemsDes> litm)
        {
            int l = 0;
            for (int i = 0; i < litm.Count; i++)
            {
                l += litm[i].Price;
            }
            return (int.Parse(retriveSettings()) - l);
        }

       
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.action_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }


       private string retriveSettings()
        {
            var prefs = Application.Context.GetSharedPreferences("BapKBol", FileCreationMode.Private);
            var settings = prefs.GetString("amount",null);
            return settings;
        }
    }
}

