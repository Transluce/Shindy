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
using Android.Support.V4.View;
using Java.Lang;
using Android.Graphics;
using Android.Util;

namespace Shindy.Droid
{
    public class ImageAdapter : PagerAdapter
    {
        private Context context;
        private Bitmap[] images;

        public ImageAdapter(Context context,Bitmap[] images)
        {
            this.context = context;
            this.images = images;
        }
        public override int Count
        {
            get
            {
                return images.Length;
            }
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object objectValue)
        {
            return view == ((ImageView)objectValue);
        }

        public override Java.Lang.Object InstantiateItem(View container, int position)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            int width = metrics.WidthPixels;
            int height = metrics.HeightPixels;
            ImageView imageView = new ImageView(context);
            imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
            imageView.SetImageBitmap(images[position]);
            ((ViewPager)container).AddView(imageView, 0);
            return imageView;
        }

        public override void DestroyItem(View container, int position, Java.Lang.Object objectValue)
        {
            ((ViewPager)container).RemoveView((ImageView)objectValue);
        }
    }
}