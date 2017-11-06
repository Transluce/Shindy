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

namespace Shindy.Droid
{
    [Activity(Label = "SignUpActivity")]
    public class SignUpActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetTheme(Resource.Style.ShindySignUpTheme);
            SetContentView(Resource.Layout.SignUp);
            
            SetFont();
            
        }

        private void Back_button_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private void SetFont()
        {
            ImageButton back_button = FindViewById<ImageButton>(Resource.Id.back_button);
            back_button.Click += Back_button_Click;
            Typeface font_thin = Typeface.CreateFromAsset(ApplicationContext.Assets, "fonts/Roboto-Light.ttf");
            Typeface font_regular = Typeface.CreateFromAsset(ApplicationContext.Assets, "fonts/Roboto-Regular.ttf");
            TextView[] textviews1 = new TextView[] { FindViewById<TextView>(Resource.Id.title), FindViewById<TextView>(Resource.Id.subtitle) };
            TextView[] textviews2 = new TextView[] { FindViewById<TextView>(Resource.Id.gender_label), FindViewById<TextView>(Resource.Id.email_label), FindViewById<TextView>(Resource.Id.zipcode_label), FindViewById<TextView>(Resource.Id.birthday_label), FindViewById<TextView>(Resource.Id.password_label), FindViewById<TextView>(Resource.Id.birthdayHint_label), FindViewById<TextView>(Resource.Id.passwordHint_label), FindViewById<TextView>(Resource.Id.confirmPassword_field) };
            EditText[] fields = new EditText[] { FindViewById<EditText>(Resource.Id.signupFirstName_field), FindViewById<EditText>(Resource.Id.signupLastName_field), FindViewById<EditText>(Resource.Id.birthday_field), FindViewById<EditText>(Resource.Id.email_field), FindViewById<EditText>(Resource.Id.zipcode_field), FindViewById<EditText>(Resource.Id.password_field) };
            Button button = FindViewById<Button>(Resource.Id.signUp_button);
            button.SetTypeface(font_regular, TypefaceStyle.Normal);
            for (int i = 0; i < textviews1.Length; i++)
            {
                textviews1[i].SetTypeface(font_thin, TypefaceStyle.Normal);
            }
            for (int i = 0; i < textviews2.Length; i++)
            {
                textviews2[i].SetTypeface(font_regular, TypefaceStyle.Normal);
            }
            for (int i = 0; i < fields.Length; i++)
            {
                fields[i].SetTypeface(font_regular, TypefaceStyle.Normal);
            }
        }
    }
}