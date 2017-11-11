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
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shindy.Droid
{
    public class PageFragment : Fragment
    {
        const string ARG_PAGE = "ARG_PAGE";
        private int mPage;
        private List<Event> EventList;
        public static PageFragment newInstance(int page)
        {
            var args = new Bundle();
            args.PutInt(ARG_PAGE, page);
            var fragment = new PageFragment();
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
                case 1: view = inflater.Inflate(Resource.Layout.MyShindigs, container, false); TextView[] textviews = new TextView[] {  view.FindViewById<TextView>(Resource.Id.events_label), view.FindViewById<TextView>(Resource.Id.invites_label) };
                    for (int i = 0; i < textviews.Length; i++)
                    {
                        textviews[i].SetTypeface(font_light, TypefaceStyle.Normal); 
                    }
                    TextView shindigTip = view.FindViewById<TextView>(Resource.Id.myShindigsTip_label);
                    shindigTip.SetTypeface(font_italic, TypefaceStyle.Normal);
                    //View
                    
                   // Uri uri = new Uri("http://192.168.1.5/ShindyAdmin/application/controllers/api/events_ws.php");
                   // WebClient client = new WebClient();
                   // client.UploadValuesAsync(uri,new System.Collections.Specialized.NameValueCollection() { { "intent","evtsrch"},{ "receive","jmbolibas@yahoo.com"} });
                   // client.UploadValuesCompleted += Client_UploadValuesCompleted;
                    for (int i = 0; i < 5; i++)
                    {

                        ViewGroup eventContainer = (ViewGroup)view.FindViewById<LinearLayout>(Resource.Id.events_container);
                        ViewGroup inviteContainer = (ViewGroup)view.FindViewById<LinearLayout>(Resource.Id.invites_container);
                        View eventLayout = LayoutInflater.From(this.Activity).Inflate(Resource.Layout.Event_template, null);
                        ViewGroup eventDetailsContainer = (ViewGroup)eventLayout.FindViewById<LinearLayout>(Resource.Id.detailContainer);
                        ViewGroup eventTemplate = (ViewGroup)eventLayout.FindViewById<LinearLayout>(Resource.Id.eventExpandContainer);
                        View details_expand = LayoutInflater.From(this.Activity).Inflate(Resource.Layout.EventDetails_expand, null);
                        ImageButton details_button = details_expand.FindViewById<ImageButton>(Resource.Id.details_button);
                        details_button.Click += Details_button_Click;
                        details_expand.Visibility = ViewStates.Gone;
                        bool[] isExpandedEvent = new bool[5];
                        bool[] isExpandedInvite = new bool[5];
                        eventDetailsContainer.AddView(details_expand);
                        isExpandedEvent[i] = false;
                        isExpandedInvite[i] = false;
                        eventLayout.Tag = i;
                        eventLayout.Click += (sender, e) =>
                          {
                              switch (isExpandedEvent[(int)eventLayout.Tag])
                              {
                                  case false:
                                      isExpandedEvent[(int)eventLayout.Tag] = true;
                                      details_expand.Visibility = ViewStates.Visible;
                                      int widthSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                                      int heightSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                                      details_expand.Measure(widthSpec, heightSpec);
                                      ValueAnimator expandAnimator = slideAnimator(0, details_expand.MeasuredHeight, details_expand);
                                      expandAnimator.Start();

                                      break;
                                  case true:
                                      isExpandedEvent[(int)eventLayout.Tag] = false;
                                      int finalHeight = details_expand.Height;
                                      ValueAnimator collapseAnimator = slideAnimator(finalHeight, 0, details_expand);
                                      collapseAnimator.Start();
                                      collapseAnimator.AnimationEnd += (sender2, e2) =>
                                        {
                                            details_expand.Visibility = ViewStates.Gone;
                                        };
                                      break;
                              }

                          };
                        View inviteLayout = LayoutInflater.From(this.Activity).Inflate(Resource.Layout.Invites_template, null);
                        inviteLayout.Tag = i;
                        ViewGroup inviteDetailsContainer = (ViewGroup)(inviteLayout.FindViewById<LinearLayout>(Resource.Id.detailContainer));
                        View inviteDetails_expand = LayoutInflater.From(this.Activity).Inflate(Resource.Layout.EventDetails_expand, null);
                        inviteDetails_expand.Visibility = ViewStates.Gone;
                        inviteDetailsContainer.AddView(inviteDetails_expand);
                        inviteLayout.Click += (sender3, e3) =>
                          {
                             // Toast.MakeText(this.Context, inviteLayout.Tag.ToString(), ToastLength.Short).Show();
                              switch (isExpandedInvite[(int)inviteLayout.Tag])
                              {
                                  case false:
                                      
                                      isExpandedInvite[(int)inviteLayout.Tag] = true;
                                      inviteDetails_expand.Visibility = ViewStates.Visible;
                                      int widthSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                                      int heightSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                                      inviteDetails_expand.Measure(widthSpec, heightSpec);
                                      ValueAnimator expandAnimator = slideAnimator(0, inviteDetails_expand.MeasuredHeight, inviteDetails_expand);
                                      expandAnimator.Start();

                                      break;
                                  case true:
                                      isExpandedInvite[(int)inviteLayout.Tag] = false;
                                      int finalHeight = inviteDetails_expand.Height;
                                      ValueAnimator collapseAnimator = slideAnimator(finalHeight, 0, inviteDetails_expand);
                                      collapseAnimator.Start();
                                      collapseAnimator.AnimationEnd += (sender2, e2) =>
                                      {
                                          inviteDetails_expand.Visibility = ViewStates.Gone;
                                      };
                                      break;
                              }
                              // ImageView eventImage = eventLayout.FindViewById<ImageView>(Resource.Id.eventPic);
                              // eventImage.SetImageBitmap(GetImageBitmapFromUrl("https://coresites-cdn.factorymedia.com/mpora_new/wp-content/uploads/2015/12/iStock_000052710440_Medium.jpg"));
                              // eventLayout.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
                             
                          };
                        eventContainer.AddView(eventLayout);
                        inviteContainer.AddView(inviteLayout);
                    }
                    Button inviteCode_button = view.FindViewById<Button>(Resource.Id.inviteCode_button);
                    inviteCode_button.Click += InviteCode_button_Click;
                    return view;
                case 2: view = inflater.Inflate(Resource.Layout.MyHostedEvents, container, false);
                    TextView eventsHosted_title = view.FindViewById<TextView>(Resource.Id.eventsHostedTitle_label);
                    TextView eventHostedTip = view.FindViewById<TextView>(Resource.Id.hostedEventsTip_label);
                    eventsHosted_title.SetTypeface(font_regular, TypefaceStyle.Normal);
                    eventHostedTip.SetTypeface(font_italic, TypefaceStyle.Normal);
                    ViewGroup eventHostedContainer = (ViewGroup)view.FindViewById<LinearLayout>(Resource.Id.hostedEvents_container);
                    View eventHostedLayout = LayoutInflater.From(this.Activity).Inflate(Resource.Layout.HostedEvent_template, null);
                    eventHostedContainer.AddView(eventHostedLayout);
                    return view;
                default:return null;
            }
            
            
            
        }

        private void Client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            string json = Encoding.UTF8.GetString(e.Result);
            EventList = JsonConvert.DeserializeObject<List<Event>>(json);
            bool[] isExpandedEvent = new bool[5];
            bool[] isExpandedInvite = new bool[5];
            for (int i = 0; i < EventList.Count; i++)
            {
                ViewGroup eventContainer = (ViewGroup)View.FindViewById<LinearLayout>(Resource.Id.events_container);
                ViewGroup inviteContainer = (ViewGroup)View.FindViewById<LinearLayout>(Resource.Id.invites_container);
                View eventLayout = LayoutInflater.From(this.Activity).Inflate(Resource.Layout.Event_template, null);
                ViewGroup eventDetailsContainer = (ViewGroup)eventLayout.FindViewById<LinearLayout>(Resource.Id.detailContainer);
                ViewGroup eventTemplate = (ViewGroup)eventLayout.FindViewById<LinearLayout>(Resource.Id.eventExpandContainer);
                View details_expand = LayoutInflater.From(this.Activity).Inflate(Resource.Layout.EventDetails_expand, null);
                ImageButton details_button = details_expand.FindViewById<ImageButton>(Resource.Id.details_button);
                TextView title, inviter, date, expDate, reqMale, reqFemale;
                title = eventLayout.FindViewById<TextView>(Resource.Id.eventTitle);
                inviter = eventLayout.FindViewById<TextView>(Resource.Id.eventInviter);
                date = eventLayout.FindViewById<TextView>(Resource.Id.eventDate);
                expDate = eventLayout.FindViewById<TextView>(Resource.Id.eventExpiration);
                reqMale = eventLayout.FindViewById<TextView>(Resource.Id.reqMale);
                reqFemale = eventLayout.FindViewById<TextView>(Resource.Id.reqFemale);
                //Passing Data
                title.Text = EventList[i].event_name;

                details_button.Click += Details_button_Click;
                details_expand.Visibility = ViewStates.Gone;
                eventDetailsContainer.AddView(details_expand);
                isExpandedEvent[i] = false;
                isExpandedInvite[i] = false;
                eventLayout.Tag = i;
                eventLayout.Click += (senderr, e1) =>
                {
                    switch (isExpandedEvent[(int)eventLayout.Tag])
                    {
                        case false:
                            isExpandedEvent[(int)eventLayout.Tag] = true;
                            details_expand.Visibility = ViewStates.Visible;
                            int widthSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                            int heightSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                            details_expand.Measure(widthSpec, heightSpec);
                            ValueAnimator expandAnimator = slideAnimator(0, details_expand.MeasuredHeight, details_expand);
                            expandAnimator.Start();

                            break;
                        case true:
                            isExpandedEvent[(int)eventLayout.Tag] = false;
                            int finalHeight = details_expand.Height;
                            ValueAnimator collapseAnimator = slideAnimator(finalHeight, 0, details_expand);
                            collapseAnimator.Start();
                            collapseAnimator.AnimationEnd += (sender2, e2) =>
                            {
                                details_expand.Visibility = ViewStates.Gone;
                            };
                            break;
                    }

                };
                View inviteLayout = LayoutInflater.From(this.Activity).Inflate(Resource.Layout.Invites_template, null);
                inviteLayout.Tag = i;
                ViewGroup inviteDetailsContainer = (ViewGroup)(inviteLayout.FindViewById<LinearLayout>(Resource.Id.detailContainer));
                View inviteDetails_expand = LayoutInflater.From(this.Activity).Inflate(Resource.Layout.EventDetails_expand, null);
                inviteDetails_expand.Visibility = ViewStates.Gone;
                inviteDetailsContainer.AddView(inviteDetails_expand);
                inviteLayout.Click += (sender3, e3) =>
                {
                    // Toast.MakeText(this.Context, inviteLayout.Tag.ToString(), ToastLength.Short).Show();
                    switch (isExpandedInvite[(int)inviteLayout.Tag])
                    {
                        case false:

                            isExpandedInvite[(int)inviteLayout.Tag] = true;
                            inviteDetails_expand.Visibility = ViewStates.Visible;
                            int widthSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                            int heightSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                            inviteDetails_expand.Measure(widthSpec, heightSpec);
                            ValueAnimator expandAnimator = slideAnimator(0, inviteDetails_expand.MeasuredHeight, inviteDetails_expand);
                            expandAnimator.Start();

                            break;
                        case true:
                            isExpandedInvite[(int)inviteLayout.Tag] = false;
                            int finalHeight = inviteDetails_expand.Height;
                            ValueAnimator collapseAnimator = slideAnimator(finalHeight, 0, inviteDetails_expand);
                            collapseAnimator.Start();
                            collapseAnimator.AnimationEnd += (sender2, e2) =>
                            {
                                inviteDetails_expand.Visibility = ViewStates.Gone;
                            };
                            break;
                    }
                    // ImageView eventImage = eventLayout.FindViewById<ImageView>(Resource.Id.eventPic);
                    // eventImage.SetImageBitmap(GetImageBitmapFromUrl("https://coresites-cdn.factorymedia.com/mpora_new/wp-content/uploads/2015/12/iStock_000052710440_Medium.jpg"));
                    // eventLayout.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);

                };
                eventContainer.AddView(eventLayout);
                inviteContainer.AddView(inviteLayout);
            }
        }

        private void Client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            
        }

        private void Details_button_Click(object sender, EventArgs e)
        {

            Activity.StartActivity(typeof(EventDetailsActivity));
            Activity.OverridePendingTransition(Resource.Animation.slideLeft_tohide, Resource.Animation.slideLeft);
        }

        private ValueAnimator slideAnimator(int start, int end,View Layout)
        {

            ValueAnimator animator = ValueAnimator.OfInt(start, end);
            //ValueAnimator animator2 = ValueAnimator.OfInt(start, end);
            //  animator.AddUpdateListener (new ValueAnimator.IAnimatorUpdateListener{
            animator.Update +=
                (object sender, ValueAnimator.AnimatorUpdateEventArgs e) => {
                //  int newValue = (int)
                //e.Animation.AnimatedValue; // Apply this new value to the object being animated.
                //  myObj.SomeIntegerValue = newValue; 
                var value = (int)animator.AnimatedValue;
                    ViewGroup.LayoutParams layoutParams = Layout.LayoutParameters;
                    layoutParams.Height = value;
                    Layout.LayoutParameters = layoutParams;

                };
            return animator;
        }
        private void EventLayout_Click(object sender, EventArgs e)
        {
            View eventContainer = LayoutInflater.From(this.Activity).Inflate(Resource.Layout.Event_template, null);
            ViewGroup eventDetailsContainer = (ViewGroup)eventContainer.FindViewById<LinearLayout>(Resource.Id.eventExpandContainer);
            View details_expand= LayoutInflater.From(this.Activity).Inflate(Resource.Layout.EventDetails_expand, null);
            eventDetailsContainer.AddView(new Button(this.Context));
            Toast.MakeText(this.Context, "sadsadasd", ToastLength.Short).Show();
        }

        private void InviteCode_button_Click(object sender, EventArgs e)
        {
            Android.App.FragmentManager manager = this.Activity.FragmentManager;
            Android.App.FragmentTransaction transaction = manager.BeginTransaction();
            InviteCode_dialog inviteCode_dialog=new InviteCode_dialog();
            inviteCode_dialog.Show(transaction,"dialog fragment");
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