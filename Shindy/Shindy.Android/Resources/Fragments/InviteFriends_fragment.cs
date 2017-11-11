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
    public class InviteFriends_fragment : Android.Support.V4.App.Fragment
    {
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here


        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.InviteFriends, container, false);
            var pager = view.FindViewById<ViewPager>(Resource.Id.pager);
            var tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabs);
            var adapter = new InviteFriendsPagerAdapter(this.Context, Activity.SupportFragmentManager,new String[] { "SEARCH","INTERESTS","FB","INVITE"},4,new int[] { Resource.Drawable.ic_search,Resource.Drawable.ic_heart_outline,Resource.Drawable.ic_fb,Resource.Drawable.ic_search});
            pager.Adapter = adapter;
            tabLayout.SetupWithViewPager(pager);
            tabLayout.SetSelectedTabIndicatorColor(Color.Rgb(253, 202, 46));
            pager.OffscreenPageLimit = 2;
            for (int i = 0; i < tabLayout.TabCount; i++)
            {
                TabLayout.Tab tab = tabLayout.GetTabAt(i);
                tab.SetCustomView(adapter.GetTabView(i));
            }
            return view;
        }
    }
}