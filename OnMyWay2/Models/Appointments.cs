using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnMyWay2.Models
{
    public class Appointments
    {
       
        public int customer_id { set; get; }
        public int provider_id { set; get; }
        public int appointment_id { set; get; }
		public int service_id { set; get; }

		public string customer_name { set; get; }
        public string customer_phone { set; get; }
        public string customer_gender { set; get; }
        public string provider_name { set; get; }
        public string provider_phone { set; get; }
        public string provider_gender { set; get; }
        public string pickup_location { set; get; }
        public string destination { set; get; }
        public string date { set; get; }
        public string space { set; get; }
        public double latitude { set; get; }
        public double longitude { set; get; }

    }
}
