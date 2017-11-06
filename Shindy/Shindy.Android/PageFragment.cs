using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Android.Graphics;
using System.Net;
using Android.Animation;

namespace Shindy.Droid
{
    public class PageFragment : Fragment
    {
        const string ARG_PAGE = "ARG_PAGE";
        private int mPage;

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
                    bool[] isExpanded = new bool[5];
                    for (int i = 0; i < 5; i++)
                    {

                        ViewGroup eventContainer = (ViewGroup)view.FindViewById<LinearLayout>(Resource.Id.events_container);
                        ViewGroup inviteContainer = (ViewGroup)view.FindViewById<LinearLayout>(Resource.Id.invites_container);
                        View eventLayout = LayoutInflater.From(this.Activity).Inflate(Resource.Layout.Event_template, null);
                        ViewGroup eventDetailsContainer = (ViewGroup)eventLayout.FindViewById<LinearLayout>(Resource.Id.bar);
                        ViewGroup eventTemplate = (ViewGroup)eventLayout.FindViewById<LinearLayout>(Resource.Id.eventExpandContainer);
                        View details_expand = LayoutInflater.From(this.Activity).Inflate(Resource.Layout.EventDetails_expand, null);
                        // details_expand.TranslationY = -100;
                        // details_expand.TranslationZ = -100;
                        // eventDetailsContainer.TranslationY = -100;
                        // eventDetailsContainer.TranslationZ = -100;
                        details_expand.Visibility = ViewStates.Gone;
                        var interpolator = new Android.Views.Animations.OvershootInterpolator(1);
                        eventDetailsContainer.AddView(details_expand);
                        isExpanded[i] = false;
                        eventLayout.Tag = i;
                        eventLayout.Click+=(sender,e) =>
                        {
                           switch(isExpanded[(int)eventLayout.Tag])
                            {
                                case false:
                                    isExpanded[(int)eventLayout.Tag] = true;
                                    details_expand.Visibility = ViewStates.Visible;
                                    int widthSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                                    int heightSpec = View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);
                                    details_expand.Measure(widthSpec, heightSpec);
                                    ValueAnimator expandAnimator = slideAnimator(0, details_expand.MeasuredHeight, details_expand);
                                    expandAnimator.Start();

                                    break;
                                case true:
                                    isExpanded[(int)eventLayout.Tag] = false;
                                    int finalHeight = details_expand.Height;
                                    ValueAnimator collapseAnimator = slideAnimator(finalHeight, 0,details_expand);
                                    collapseAnimator.Start();
                                    collapseAnimator.AnimationEnd += (sender2, e2) =>
                                      {
                                          details_expand.Visibility = ViewStates.Gone;
                                      };

                                    // eventDetailsContainer.RemoveView(details_expand);
                                    break;
                            }
                          
                        };
                        View inviteLayout= LayoutInflater.From(this.Activity).Inflate(Resource.Layout.Invites_template, null);
                        // ImageView eventImage = eventLayout.FindViewById<ImageView>(Resource.Id.eventPic);
                        // eventImage.SetImageBitmap(GetImageBitmapFromUrl("https://coresites-cdn.factorymedia.com/mpora_new/wp-content/uploads/2015/12/iStock_000052710440_Medium.jpg"));
                        // eventLayout.LayoutParameters = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
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