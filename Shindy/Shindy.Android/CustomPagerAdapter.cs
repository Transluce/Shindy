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
    public class CustomPagerAdapter:FragmentPagerAdapter
    {
        int PAGE_COUNT;
        private string[] tabTitles;
        readonly Context context;

        public CustomPagerAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public CustomPagerAdapter(Context context, FragmentManager fm,string[] tabTitles,int pageCount) : base(fm)
        {
            this.context = context;
            this.tabTitles = tabTitles;
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
            return PageFragment.newInstance(position + 1);
        }
        public View GetTabView(int position)
        {
            // Given you have a custom layout in `res/layout/custom_tab.xml` with a TextView
            Typeface font_light = Typeface.CreateFromAsset(this.context.Assets, "fonts/Roboto-Light.ttf");
            var title = (TextView)LayoutInflater.From(context).Inflate(Resource.Layout.tab, null);
            title.Text = tabTitles[position];
            title.SetTypeface(font_light, TypefaceStyle.Normal);
            return title;
        }
    }
}
