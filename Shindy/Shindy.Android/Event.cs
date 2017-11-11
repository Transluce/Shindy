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

namespace Shindy.Droid
{
    public class Event
    {
        public int event_ID { get; set; }
        public string host { get; set; }
        public string collaborators { get; set; }
        public string event_name { get; set; }
        public string time { get; set; }
        public string date { get; set; }
        public string details { get; set; }
        public string max_female { get; set; }
        public string max_male { get; set; }
        public string guest_invitation { get; set; }
        public string price { get; set; }
        public string expiry { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string event_image { get; set; }
    }
}