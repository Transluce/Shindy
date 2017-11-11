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
using Android.Support.V4.View;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace Shindy.Droid
{
    public class EventDetailsPageFragment : Fragment,IOnMapReadyCallback
    {
        private GoogleMap map;
        const string ARG_PAGE = "ARG_PAGE";
        private int mPage;

        public static EventDetailsPageFragment newInstance(int page)
        {
            var args = new Bundle();
            args.PutInt(ARG_PAGE, page);
            var fragment = new EventDetailsPageFragment();
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
                case 1: view = inflater.Inflate(Resource.Layout.EventDetailsDescription_template, container, false);
                    
                    ScrollView scrollView = this.Activity.FindViewById<ScrollView>(Resource.Id.scroll);
                    var mapFragment = (TouchableMapFragment)Activity.FragmentManager.FindFragmentById(Resource.Id.map);
                    mapFragment.TouchUp += (sender, args) =>
                    {
                        scrollView.RequestDisallowInterceptTouchEvent(false);
                    };
                    mapFragment.TouchDown += (sender, args) =>
                    {
                        scrollView.RequestDisallowInterceptTouchEvent(true);
                    };
                    mapFragment.GetMapAsync(this);

                    return view;
                case 2: view = inflater.Inflate(Resource.Layout.EventReviews, container, false);
                    var rate_button = view.FindViewById<Button>(Resource.Id.rate_button);
                    rate_button.Click += Rate_button_Click;
                    return view;
                case 3:
                view = inflater.Inflate(Resource.Layout.EventDiscussion, container, false);

                    return view;
                default:return null;
            }
            
            
            
        }

        private void Rate_button_Click(object sender, EventArgs e)
        {
            Android.App.FragmentManager manager = this.Activity.FragmentManager;
            Android.App.FragmentTransaction transaction = manager.BeginTransaction();
            rate_dialog rate_dialog = new rate_dialog();
            rate_dialog.Show(transaction, "dialog fragment");
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            map.MapType = GoogleMap.MapTypeNormal;
            LatLng location = new LatLng(14.5366196, 120.9821070);
            MarkerOptions markerOptions = new MarkerOptions();
            markerOptions.SetPosition(location);
            markerOptions.SetTitle("SM Mall of Asia");
            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(18);
            builder.Bearing(155);
            CameraPosition cameraPosition = builder.Build();
            CameraUpdate cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            map.MoveCamera(cameraUpdate);
            map.AddMarker(markerOptions);
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