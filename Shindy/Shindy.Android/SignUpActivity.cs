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
using System.Net;
using static Android.App.DatePickerDialog;
using Newtonsoft.Json.Linq;

namespace Shindy.Droid
{
    [Activity(Label = "SignUpActivity")]
    public class SignUpActivity : Activity, IOnDateSetListener
    {
        private DatePickerDialog datepicker;
        private EditText firstName, lastName, email, birthday, password, confirmPass, zipCode;
        private RadioGroup gender;
        private RadioButton gender_radioButton;
        private int year = DateTime.Today.Year, month = DateTime.Today.Month, day = DateTime.Today.Day;
        private string selectedGender;
        private string date;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetTheme(Resource.Style.ShindySignUpTheme);
            SetContentView(Resource.Layout.SignUp);
            datepicker = new DatePickerDialog(this, this, year, month, day);
            SetFont();
            
        }

        private void Back_button_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private void SetFont()
        {
            selectedGender = "";
            ImageButton back_button = FindViewById<ImageButton>(Resource.Id.back_button);
            back_button.Click += Back_button_Click;
            Typeface font_thin = Typeface.CreateFromAsset(ApplicationContext.Assets, "fonts/Roboto-Light.ttf");
            Typeface font_regular = Typeface.CreateFromAsset(ApplicationContext.Assets, "fonts/Roboto-Regular.ttf");
            TextView[] textviews1 = new TextView[] { FindViewById<TextView>(Resource.Id.title), FindViewById<TextView>(Resource.Id.subtitle) };
            TextView[] textviews2 = new TextView[] { FindViewById<TextView>(Resource.Id.gender_label), FindViewById<TextView>(Resource.Id.email_label), FindViewById<TextView>(Resource.Id.zipcode_label), FindViewById<TextView>(Resource.Id.birthday_label), FindViewById<TextView>(Resource.Id.password_label), FindViewById<TextView>(Resource.Id.birthdayHint_label), FindViewById<TextView>(Resource.Id.passwordHint_label), FindViewById<TextView>(Resource.Id.confirmPassword_field) };
            EditText[] fields = new EditText[] { FindViewById<EditText>(Resource.Id.signupFirstName_field), FindViewById<EditText>(Resource.Id.signupLastName_field), FindViewById<EditText>(Resource.Id.birthday_field), FindViewById<EditText>(Resource.Id.email_field), FindViewById<EditText>(Resource.Id.zipcode_field), FindViewById<EditText>(Resource.Id.password_field) };
            gender = FindViewById<RadioGroup>(Resource.Id.gender);
             firstName = FindViewById<EditText>(Resource.Id.signupFirstName_field);
             lastName = FindViewById<EditText>(Resource.Id.signupLastName_field);
             email = FindViewById<EditText>(Resource.Id.email_field);
             password = FindViewById<EditText>(Resource.Id.password_field);
             confirmPass = FindViewById<EditText>(Resource.Id.confirmPassword_field);
              birthday= FindViewById<EditText>(Resource.Id.birthday_field);
             zipCode = FindViewById<EditText>(Resource.Id.zipcode_field);
            
            Button button = FindViewById<Button>(Resource.Id.signUp_button);
            button.Click += Button_Click;
            button.SetTypeface(font_regular, TypefaceStyle.Normal);
            birthday.Touch += Birthday_Touch;
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

        private void Birthday_Touch(object sender, View.TouchEventArgs e)
        {
            datepicker.Show();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (password.Text == confirmPass.Text)
            {
                int id = gender.CheckedRadioButtonId;
                gender_radioButton = FindViewById<RadioButton>(id);
                selectedGender = gender_radioButton.Text == "Male" ? "0" : gender_radioButton.Text == "Female" ? "1" : "0";
                WebClient client = new WebClient();
                Uri uri = new Uri("http://192.168.1.5/ShindyAdmin/application/controllers/api/users_ws.php");
                client.UploadValuesAsync(uri, new System.Collections.Specialized.NameValueCollection() { { "email", email.Text }, { "intent", "signup" }, { "pass", password.Text }, { "at", "0" }, { "fn", firstName.Text }, { "ln", lastName.Text }, { "gen", selectedGender }, { "bday", date }, { "zip", zipCode.Text } });
                client.UploadValuesCompleted += Client_UploadValuesCompleted;
            }
            else
            {
                Toast.MakeText(this.ApplicationContext, "Password doesn't match", ToastLength.Short).Show();
            }
        }

        private void Client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            string json = Encoding.UTF8.GetString(e.Result);
            var obj = JObject.Parse(json);
            string response = obj["status"].ToString();
            Toast.MakeText(this.ApplicationContext, response, ToastLength.Short).Show();
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            month++;
            this.year = year;
            this.month = month;
            this.day = dayOfMonth;
            birthday.Text= new DateTime(year, month, dayOfMonth).ToString("MMMM dd, yyyy");
            date = new DateTime(year, month, dayOfMonth).ToString("yyyy-MM-dd");
        }
    }
}