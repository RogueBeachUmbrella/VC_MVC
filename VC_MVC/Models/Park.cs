using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VC_MVC.Models
{

    public class Park
    {
        [JsonProperty("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ParkId { get; set; }
        public string url { get; set; }
        public string fullName { get; set; }
        public string parkCode { get; set; }
        public string description { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string latLong { get; set; }
        public virtual ICollection<Activity> activities { get; set; }
        public virtual ICollection<Topic> topics { get; set; }
        public string states { get; set; }
        public Contacts contacts { get; set; }
        public ICollection<Entrancefee> entranceFees { get; set; }
        public ICollection<Entrancepass> entrancePasses { get; set; }
        //public object[] fees { get; set; }
        public string directionsInfo { get; set; }
        public string directionsUrl { get; set; }
        public ICollection<Operatinghour> operatingHours { get; set; }
        [JsonProperty("addresses")]
        public ICollection<Address> address { get; set; }
        public ICollection<Image> images { get; set; }
        public string weatherInfo { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
    }

    public class Contacts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ContactId { get; set; }
        public string ParkId { get; set; }
        public ICollection<Phonenumber> phoneNumbers { get; set; }
        public ICollection<Emailaddress> emailAddresses { get; set; }
    }
    
    public class Phonenumber
    {
        public string ContactId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PhonenumberId { get; set; }
        public string phoneNumber { get; set; }
        public string description { get; set; }
        public string extension { get; set; }
        public string type { get; set; }
    }
    
    public class Emailaddress
    {
        public string ContactId { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string EmailaddressId { get; set; }
        public string description { get; set; }
        public string emailAddress { get; set; }
    }

    public class Activity
    {
        [JsonProperty("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ActivityId { get; set; }
        public string name { get; set; }
        public string ParkId { get; set; }
        public Park park { get; set; }
    }

    public class Topic
    {
        [JsonProperty("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TopicId { get; set; }
        public string name { get; set; }
        public string ParkId { get; set; }
    }
    
    public class Entrancefee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string EntrancefeeId { get; set; }
        public string ParkId { get; set; }
        public string cost { get; set; }
        public string description { get; set; }
        public string title { get; set; }
    }
    
    public class Entrancepass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string EntrancepassId { get; set; }
        public string ParkId { get; set; }
        public string cost { get; set; }
        public string description { get; set; }
        public string title { get; set; }
    }
    
    public class Operatinghour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string OperatinghourId { get; set; }
        public string ParkId { get; set; }
        //public object[] exceptions { get; set; }
        public string description { get; set; }
        public Standardhours standardHours { get; set; }
        public string name { get; set; }
    }
    
    public class Standardhours
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string StandardhoursId { get; set; }
        public string OperatinghourId { get; set; }
        public string wednesday { get; set; }
        public string monday { get; set; }
        public string thursday { get; set; }
        public string sunday { get; set; }
        public string tuesday { get; set; }
        public string friday { get; set; }
        public string saturday { get; set; }
    }
    
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AddressId { get; set; }
        public string ParkId { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string stateCode { get; set; }
        public string line1 { get; set; }
        public string type { get; set; }
        public string line3 { get; set; }
        public string line2 { get; set; }
    }
    
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ImageId { get; set; }
        public string ParkId { get; set; }
        public string credit { get; set; }
        public string title { get; set; }
        public string altText { get; set; }
        public string caption { get; set; }
        public string url { get; set; }
    }

}
