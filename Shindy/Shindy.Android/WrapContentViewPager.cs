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
using Java.Util.Jar;
using Android.Util;

namespace Shindy.Droid
{
    [Register("com.shindy.WrapContentViewPager")]
    public class WrapContentViewPager : ViewPager
    {
        Context mContext;
        public WrapContentViewPager(Context context) :
			base(context)
			{
            
        }
        public WrapContentViewPager(Context context, IAttributeSet attrs) :
			base(context, attrs)
			{
            mContext = context;
            
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            int height = 0;
            for(int i=0;i<ChildCount;i++)
            {
                View child = GetChildAt(i);
                child.Measure(widthMeasureSpec, MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));
                int h = child.MeasuredHeight;
                if(h>height)
                {
                    height = h;
                }

            }
            if (height != 0)
            {
                heightMeasureSpec = MeasureSpec.MakeMeasureSpec(height, MeasureSpecMode.Exactly);
            }
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }

        
    }
}