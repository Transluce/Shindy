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
    class InviteCode_dialog:DialogFragment
    {

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Typeface font_light = Typeface.CreateFromAsset(this.Activity.ApplicationContext.Assets, "fonts/Roboto-Light.ttf");
            Typeface font_regular = Typeface.CreateFromAsset(this.Activity.ApplicationContext.Assets, "fonts/Roboto-Regular.ttf");
            Dialog.Window.SetWindowAnimations(Resource.Style.dialogAnimation);
            Dialog.Window.RequestFeature(WindowFeatures.SwipeToDismiss);
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            SetStyle(DialogFragmentStyle.NoTitle, Android.Resource.Style.ThemeHoloDialogNoActionBar);
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.InviteCode_dialogBox,container,false);
            var close_button = view.FindViewById<ImageButton>(Resource.Id.close_button);
            close_button.Click += Close_button_Click;
            var label = view.FindViewById<TextView>(Resource.Id.inviteCode_label);
            var field = view.FindViewById<EditText>(Resource.Id.inviteCode_field);
            label.SetTypeface(font_light, TypefaceStyle.Normal);
            field.SetTypeface(font_regular, TypefaceStyle.Normal);

            return view;
        }

        private void Close_button_Click(object sender, EventArgs e)
        {
            this.Dialog.Dismiss();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
        }
    }
}