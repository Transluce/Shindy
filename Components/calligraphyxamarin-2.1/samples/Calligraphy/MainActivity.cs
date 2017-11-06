﻿using Android.App;
using Android.Widget;
using Android.OS;
using UK.CO.Chrisjenx.Calligraphy;

namespace CalligraphySample
{
	[Activity (Label = "Calligraphy", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
            CalligraphyConfig.InitDefault(
               new CalligraphyConfig.Builder()
                 .SetDefaultFontPath("fonts/Roboto-Bold.ttf")
                 .SetFontAttrId(Resource.Attribute.fontPath)
                 .Build()
             );
            // Get our button from the layout resource,
            // and attach an event to it
        }

		//Can put this in Base Activity
		protected override void AttachBaseContext (Android.Content.Context @base)
		{
			base.AttachBaseContext (CalligraphyContextWrapper.Wrap(@base));
		}
	}
}

