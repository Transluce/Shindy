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
using Android.Graphics;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using UK.CO.Chrisjenx.Calligraphy;
using Android.Support.V7.App;
using Shindy.Droid.Resources.Fragments;

namespace Shindy.Droid
{
    [Activity(Label = "Home")]
    public class HomeActivity : AppCompatActivity
    {
        private Home_fragment home_fragment;
        protected override void AttachBaseContext(Context @base)
        {
             base.AttachBaseContext(CalligraphyContextWrapper.Wrap(@base));
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetTheme(Resource.Style.ShindyTheme);
            SetContentView(Resource.Layout.Navigation);
            var trans = SupportFragmentManager.BeginTransaction();
            home_fragment = new Home_fragment();
            trans.Replace(Resource.Id.fragment_container, home_fragment);
            trans.Commit();
            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            CalligraphyConfig.InitDefault(
                new CalligraphyConfig.Builder()
                  .SetDefaultFontPath("fonts/Roboto-Regular.ttf")
                  .SetFontAttrId(Resource.Attribute.fontPath)
                  .Build()
              );

        }

       

        
    }
}