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
using Android.Util;
using Android.Gms.Maps;

namespace Shindy.Droid
{
    public class TouchableMapFragment : MapFragment
    {
        public event EventHandler TouchDown;
        public event EventHandler TouchUp;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = base.OnCreateView(inflater, container, savedInstanceState);
            var wrapper = new TouchableWrapper(Activity);
            wrapper.SetBackgroundColor(Resources.GetColor(Android.Resource.Color.Transparent));
            ((ViewGroup)root).AddView(wrapper,
              new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));

            wrapper.TouchUp = () =>
            {
                if (TouchUp != null)
                    TouchUp(this, EventArgs.Empty);
            };
            wrapper.TouchDown = () =>
            {
                if (TouchDown != null)
                    TouchDown(this, EventArgs.Empty);
            };

            return root;
        }

        class TouchableWrapper : FrameLayout
        {
            public Action TouchDown;
            public Action TouchUp;

            #region ctors
            protected TouchableWrapper(IntPtr javaReference, JniHandleOwnership transfer)
              : base(javaReference, transfer) { }
            public TouchableWrapper(Context context)
              : this(context, null) { }
            public TouchableWrapper(Context context, IAttributeSet attrs)
              : this(context, attrs, 0) { }
            public TouchableWrapper(Context context, IAttributeSet attrs, int defStyle)
              : base(context, attrs, defStyle) { }
            #endregion

            public override bool DispatchTouchEvent(MotionEvent e)
            {
                switch (e.Action)
                {
                    case MotionEventActions.Down:
                        if (TouchDown != null)
                            TouchDown();
                        break;
                    case MotionEventActions.Cancel:
                    case MotionEventActions.Up:
                        if (TouchUp != null)
                            TouchUp();
                        break;
                }

                return base.DispatchTouchEvent(e);
            }
        }
    }
}