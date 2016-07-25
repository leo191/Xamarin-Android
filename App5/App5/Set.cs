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
using Android.Support.V7.App;
using AToolBar = Android.Support.V7.Widget.Toolbar;
namespace BapKBol
{
    [Activity(Label = "Set", ParentActivity = typeof(MainActivity))]
    public class Set : AppCompatActivity
    {
        EditText edt;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SetAmountLay);

            // Create your application here
            AToolBar toolbar = FindViewById<AToolBar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Set Init Amount";

            //toolbar.SetNavigationIcon(Resource.Drawable.ko);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //toolbar.NavigationClick += Toolbar_NavigationClick;
            edt = FindViewById<EditText>(Resource.Id.editText1);
            var btn = FindViewById<Button>(Resource.Id.button1);
            btn.Click += (e, o) =>
            {
                if (edt.Text == "")
                    Toast.MakeText(this, "Enter the Value", ToastLength.Long).Show();
                saveSet();
            };

        }
        protected override void OnDestroy()
        {
            if (edt.Text != "")
            { saveSet(); }
            base.OnDestroy();
        }

        private void saveSet()
        {
            var prefs = Application.Context.GetSharedPreferences("BapKBol", FileCreationMode.Private);
            var prefEditor = prefs.Edit();
            prefEditor.PutString("amount",edt.Text);
            prefEditor.Commit();
            Toast.MakeText(this,"Amount Set",ToastLength.Short).Show();
        }

        //private void Toolbar_NavigationClick(object sender, AToolBar.NavigationClickEventArgs e)
        //{
        //    OnBackPressed();
        //}
    }
}