using System;
using Android.Content;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Android.Graphics;

namespace Shindy.Droid
{
    public class InviteFriendsPagerAdapter:FragmentPagerAdapter
    {
        int PAGE_COUNT;
        private string[] tabTitles;
        private int[] iconDrawables;
        readonly Context context;

        public InviteFriendsPagerAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public InviteFriendsPagerAdapter(Context context, FragmentManager fm,string[] tabTitles,int pageCount,int[] iconDrawables) : base(fm)
        {
            this.context = context;
            this.tabTitles = tabTitles;
            this.iconDrawables = iconDrawables;
            PAGE_COUNT = pageCount;
        }

        public override int Count
        {
            get { return PAGE_COUNT; }
        }

        

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            // Generate title based on item position
            return CharSequence.ArrayFromStringArray(tabTitles)[position];
        }
        public override Fragment GetItem(int position)
        {
            return InviteFriendsPageFragment.newInstance(position + 1);
        }
        public View GetTabView(int position)
        {
            // Given you have a custom layout in `res/layout/custom_tab.xml` with a TextView
            Typeface font_light = Typeface.CreateFromAsset(this.context.Assets, "fonts/Roboto-Light.ttf");
            View v = LayoutInflater.From(context).Inflate(Resource.Layout.iconedTab, null);
            var title = v.FindViewById<TextView>(Resource.Id.tab);
            var icon = v.FindViewById<ImageView>(Resource.Id.tabIcon);
            icon.SetImageResource(iconDrawables[position]);
            icon.SetColorFilter(Color.Rgb(204, 204, 204));
            title.Text = tabTitles[position];
            title.SetTypeface(font_light, TypefaceStyle.Normal);
            return v;
        }
    }
}
