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
    public class Home_fragment : Android.Support.V4.App.Fragment
    {
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here


        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Home, container, false);
            var pager = view.FindViewById<ViewPager>(Resource.Id.pager);
            var tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabs);
            var adapter = new CustomPagerAdapter(this.Context, Activity.SupportFragmentManager,new String[] { "My Shindigs","My Hosted Events"},2);
            pager.Adapter = adapter;
            tabLayout.SetupWithViewPager(pager);
            for (int i = 0; i < tabLayout.TabCount; i++)
            {
                TabLayout.Tab tab = tabLayout.GetTabAt(i);
                tab.SetCustomView(adapter.GetTabView(i));
            }
            return view;
        }
    }
}