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
using Android.Support.V4.View;

namespace Shindy.Droid
{
    [Activity(Label = "Home")]
    public class InviteFriendsActivity : AppCompatActivity
    {
        private InviteFriends_fragment inviteFriends_fragment;
        protected override void AttachBaseContext(Context @base)
        {
             base.AttachBaseContext(CalligraphyContextWrapper.Wrap(@base));
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetTheme(Resource.Style.ShindyFriendsTheme);
            SetContentView(Resource.Layout.Navigation);
            CalligraphyConfig.InitDefault(
                new CalligraphyConfig.Builder()
                  .SetDefaultFontPath("fonts/Roboto-Light.ttf")
                  .SetFontAttrId(Resource.Attribute.fontPath)
                  .Build()
              );
            var trans = SupportFragmentManager.BeginTransaction();
            inviteFriends_fragment = new InviteFriends_fragment();
            trans.Replace(Resource.Id.fragment_container, inviteFriends_fragment);
            trans.Commit();
            
        }

       

        
    }
}