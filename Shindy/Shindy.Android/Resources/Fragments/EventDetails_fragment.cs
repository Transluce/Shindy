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
    public class EventDetails_fragment : Android.Support.V4.App.Fragment
    {
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here


        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.EventDetails, container, false);
            var pager = view.FindViewById<ViewPager>(Resource.Id.pager);
            var tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabs);
            var imageSlider = view.FindViewById<ViewPager>(Resource.Id.imageSlider);
            Bitmap[] images = new Bitmap[] { GetImageBitmapFromUrl("http://"+MainActivity.ip+"/shindyAdmin/assets/images/rock.jpg"),GetImageBitmapFromUrl("http://"+MainActivity.ip+"/shindyAdmin/assets/images/car1.jpg"), GetImageBitmapFromUrl("http://" + MainActivity.ip + "/shindyAdmin/assets/images/car2.jpg"), GetImageBitmapFromUrl("http://" + MainActivity.ip + "/shindyAdmin/assets/images/car3.jpg") };
            ImageAdapter imageAdapter = new ImageAdapter(this.Context,images );
            var inviteFriends_button = view.FindViewById<ImageButton>(Resource.Id.sendInvite_button);
            inviteFriends_button.Click += InviteFriends_button_Click;
            imageSlider.Adapter = imageAdapter;
            var adapter = new EventDetailsPagerAdapter(this.Context, Activity.SupportFragmentManager,new String[] { "Details","Discussion","Reviews"},3);
            pager.Adapter = adapter;
            pager.OffscreenPageLimit = 2;
            tabLayout.SetupWithViewPager(pager);
            for (int i = 0; i < tabLayout.TabCount; i++)
            {
                TabLayout.Tab tab = tabLayout.GetTabAt(i);
                tab.SetCustomView(adapter.GetTabView(i));
            }
            return view;
        }

        private void InviteFriends_button_Click(object sender, EventArgs e)
        {
            Activity.StartActivity(typeof(InviteFriendsActivity));
            Activity.OverridePendingTransition(Resource.Animation.slideLeft_tohide, Resource.Animation.slideLeft);
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}