using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Android.Graphics;
using System.Net;
using Android.Animation;
using Android.Content;
using Android.Util;

namespace Shindy.Droid
{
    public class InviteFriendsPageFragment : Fragment
    {
        const string ARG_PAGE = "ARG_PAGE";
        private int mPage;

        public static InviteFriendsPageFragment newInstance(int page)
        {
            var args = new Bundle();
            args.PutInt(ARG_PAGE, page);
            var fragment = new InviteFriendsPageFragment();
            fragment.Arguments = args;
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            mPage = Arguments.GetInt(ARG_PAGE);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view;
            Typeface font_italic = Typeface.CreateFromAsset(this.Activity.ApplicationContext.Assets, "fonts/Roboto-LightItalic.ttf");
            Typeface font_light = Typeface.CreateFromAsset(this.Activity.ApplicationContext.Assets, "fonts/Roboto-Light.ttf");
            Typeface font_regular = Typeface.CreateFromAsset(this.Activity.ApplicationContext.Assets, "fonts/Roboto-Regular.ttf");
            switch (mPage)
            {
                case 1: view = inflater.Inflate(Resource.Layout.SelectFriends, container, false);
                    return view;
                case 2:
                    view = inflater.Inflate(Resource.Layout.MyShindigs, container, false);
                    return view;
                case 3:
                    view = inflater.Inflate(Resource.Layout.InAppFBLogin, container, false);
                    return view;
                case 4:
                    view = inflater.Inflate(Resource.Layout.MyShindigs, container, false);
                    return view;
                default:return null;
            }
            
            
            
        }

        
    }
}