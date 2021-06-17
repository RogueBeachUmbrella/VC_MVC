using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VC_MVC.Models
{

    public class Park
    {
        [JsonProperty("id")]
        public string ParkID { get; set; }
        public string url { get; set; }
        public string fullName { get; set; }
        public string parkCode { get; set; }
        public string description { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string latLong { get; set; }
        //public virtual ICollection<Activity> activities { get; set; }
        //public Topic[] topics { get; set; }
        //public string states { get; set; }
        //public Contacts contacts { get; set; }
        //public Entrancefee[] entranceFees { get; set; }
        //public Entrancepass[] entrancePasses { get; set; }
        //public object[] fees { get; set; }
        //public string directionsInfo { get; set; }
        //public string directionsUrl { get; set; }
        //public Operatinghour[] operatingHours { get; set; }
        //public Address[] addresses { get; set; }
        //public Image[] images { get; set; }
        //public string weatherInfo { get; set; }
        //public string name { get; set; }
        //public string designation { get; set; }
    }

    //public class Contacts
    //{
    //    public Phonenumber[] phoneNumbers { get; set; }
    //    public Emailaddress[] emailAddresses { get; set; }
    //}
    //
    //public class Phonenumber
    //{
    //    public string phoneNumber { get; set; }
    //    public string description { get; set; }
    //    public string extension { get; set; }
    //    public string type { get; set; }
    //}
    //
    //public class Emailaddress
    //{
    //    public string description { get; set; }
    //    public string emailAddress { get; set; }
    //}

    public class Activity
    {
        [JsonProperty("id")]
        public string ActivityID { get; set; }
        public string name { get; set; }
    }

    //public class Topic
    //{
    //    public string id { get; set; }
    //    public string name { get; set; }
    //}
    //
    //public class Entrancefee
    //{
    //    public string cost { get; set; }
    //    public string description { get; set; }
    //    public string title { get; set; }
    //}
    //
    //public class Entrancepass
    //{
    //    public string cost { get; set; }
    //    public string description { get; set; }
    //    public string title { get; set; }
    //}
    //
    //public class Operatinghour
    //{
    //    public object[] exceptions { get; set; }
    //    public string description { get; set; }
    //    public Standardhours standardHours { get; set; }
    //    public string name { get; set; }
    //}
    //
    //public class Standardhours
    //{
    //    public string wednesday { get; set; }
    //    public string monday { get; set; }
    //    public string thursday { get; set; }
    //    public string sunday { get; set; }
    //    public string tuesday { get; set; }
    //    public string friday { get; set; }
    //    public string saturday { get; set; }
    //}
    //
    //public class Address
    //{
    //    public string postalCode { get; set; }
    //    public string city { get; set; }
    //    public string stateCode { get; set; }
    //    public string line1 { get; set; }
    //    public string type { get; set; }
    //    public string line3 { get; set; }
    //    public string line2 { get; set; }
    //}
    //
    //public class Image
    //{
    //    public string credit { get; set; }
    //    public string title { get; set; }
    //    public string altText { get; set; }
    //    public string caption { get; set; }
    //    public string url { get; set; }
    //}

}
