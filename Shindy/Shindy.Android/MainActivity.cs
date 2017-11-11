using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Content;
using Android.Views.InputMethods;
using Android.Preferences;
using Java.IO;

namespace Shindy.Droid
{
	[Activity (Label = "Shindy", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
        LinearLayout mLinearLayout;
        Button signUp_button,login_button;
        public static string ip;
		protected override void OnCreate (Bundle bundle)
		{
			
            
			base.OnCreate (bundle);
            global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new Shindy.App ());
            SetTheme(Resource.Style.ShindyLoginTheme);
            SetContentView(Resource.Layout.Login);
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            SetFont();
            mLinearLayout = FindViewById<LinearLayout>(Resource.Id.login_layout);
            signUp_button = FindViewById<Button>(Resource.Id.signUp_button);
            login_button = FindViewById<Button>(Resource.Id.login_button);
            
            mLinearLayout.Click += MLinearLayout_Click;
            login_button.Click += Login_button_Click;
            signUp_button.Click += MBtn_SignUp_Click;
        }
        private void initializeConfig()
        {
            // Checks if ip is stored
            File config = new File(Application.GetExternalFilesDir(null), "config.txt");
            if (config.Exists())
            {
                BufferedReader read = new BufferedReader(new FileReader(config));
                ip = read.ReadLine();
            }
            else
            {
                try
                {
                    config.CreateNewFile();
                    FileWriter fw = new FileWriter(config);
                    fw.Write("192.168.1.5");
                    fw.Close();
                }
                catch (IOException) { }
                ip = "192.168.1.5";
            }
        }
        //keyboard modifications
        private void MLinearLayout_Click(object sender, EventArgs e)
        {
            InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Activity.InputMethodService);
            inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.None);
        }

        private void Login_button_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(HomeActivity));
            this.OverridePendingTransition(Resource.Animation.slideLeft_tohide, Resource.Animation.slideLeft);
            this.Finish();
        }

        // function for btn signup
        private void MBtn_SignUp_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SignUpActivity));
            this.OverridePendingTransition(Resource.Animation.slideLeft_tohide, Resource.Animation.slideLeft);
                
        }

        

        private void SetFont()
        {
            Typeface font_thin = Typeface.CreateFromAsset(ApplicationContext.Assets, "fonts/Roboto-Light.ttf");
            Typeface font_regular = Typeface.CreateFromAsset(ApplicationContext.Assets, "fonts/Roboto-Regular.ttf");
            TextView[] textviews = new TextView[] { FindViewById<TextView>(Resource.Id.title), FindViewById<TextView>(Resource.Id.subtitle) };
            Button[] buttons1 = new Button[] { FindViewById<Button>(Resource.Id.login_button), FindViewById<Button>(Resource.Id.fbLogin_button) };
            Button[] buttons2 = new Button[] { FindViewById<Button>(Resource.Id.lostPass_button), FindViewById<Button>(Resource.Id.signUp_button) };
            EditText[] fields = new EditText[] { FindViewById<EditText>(Resource.Id.loginEmail_field), FindViewById<EditText>(Resource.Id.loginPassword_field) };
            for (int i = 0; i < buttons1.Length; i++)
            {
                buttons1[i].SetTypeface(font_regular, TypefaceStyle.Normal);
            }
            for (int i = 0; i < buttons2.Length; i++)
            {
                buttons2[i].SetTypeface(font_thin, TypefaceStyle.Normal);
            }
            for (int i = 0; i < fields.Length; i++)
            {
                fields[i].SetTypeface(font_thin, TypefaceStyle.Normal);
            }
            for (int i = 0; i < textviews.Length; i++)
            {
                textviews[i].SetTypeface(font_thin, TypefaceStyle.Normal);
            }
            initializeConfig();
        }
	}
}

