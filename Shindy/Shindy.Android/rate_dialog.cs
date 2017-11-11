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
using Shindy.Droid.Resources.Fragments;

namespace Shindy.Droid
{
    class rate_dialog:DialogFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Dialog.Window.SetWindowAnimations(Resource.Style.dialogAnimation);
            Dialog.Window.RequestFeature(WindowFeatures.SwipeToDismiss);
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            SetStyle(DialogFragmentStyle.NoTitle, Android.Resource.Style.ThemeHoloDialogNoActionBar);
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.Rate_dialogBox,container,false);
            var close_button = view.FindViewById<ImageButton>(Resource.Id.close_button);
            var submit_button = view.FindViewById<Button>(Resource.Id.submit_button);
            var submitReview_button = view.FindViewById<Button>(Resource.Id.submitReview_button);
            submitReview_button.Click += SubmitReview_button_Click;
            submit_button.Click += Submit_button_Click;
            close_button.Click += Close_button_Click1;

            return view;
        }

        private void SubmitReview_button_Click(object sender, EventArgs e)
        {
            Dialog.Dismiss();
        }

        private void Submit_button_Click(object sender, EventArgs e)
        {
            var rateLayout = View.FindViewById<LinearLayout>(Resource.Id.reviewRateContainer);
            var reviewLayout = View.FindViewById<LinearLayout>(Resource.Id.reviewTextContainer);
            rateLayout.Visibility = ViewStates.Gone;
            reviewLayout.Visibility = ViewStates.Visible;
        }

        private void Close_button_Click1(object sender, EventArgs e)
        {
            Dialog.Dismiss();
        }

        
        public override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);
        }
    }
}