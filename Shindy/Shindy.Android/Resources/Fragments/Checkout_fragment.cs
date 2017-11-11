using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System.Net;
using SupportFragment = Android.Support.V4.App.Fragment;
using System.Collections.Specialized;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using UK.CO.Chrisjenx.Calligraphy;

namespace Shindy.Droid.Resources.Fragments
{
    public class Checkout_fragment : Android.Support.V4.App.Fragment
    {
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here


        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.PurchaseEvent, container, false);

            return view;
        }
    }
}