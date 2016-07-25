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
using SQLite;
using Android.Util;

namespace BapKBol.MODEL
{
    class DataBase
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool CreateDB()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "My.db")))
                {
                    connection.CreateTable<ItemsDes>();
                    return true;
                }
            }catch(Exception ex)
            {
                Log.Info("Exception", ex.Message);
                return false;
            }
        }
        public bool InsertInTable(ItemsDes itm)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "My.db")))
                {
                    connection.Insert(itm);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Info("Exception", ex.Message);
                return false;
            }
        }
        public List<ItemsDes> SelectFromTable()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "My.db")))
                {
                    return connection.Table<ItemsDes>().ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Info("Exception", ex.Message);
                return null;
            }
        }
    }
}