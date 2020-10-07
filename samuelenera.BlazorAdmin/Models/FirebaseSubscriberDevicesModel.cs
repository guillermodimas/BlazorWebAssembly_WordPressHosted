using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace samuelenera.BlazorAdmin.Models
{
    public class FirebaseSubscriberDevicesModel
    {
        public FirebaseSubscriberDevicesModel()
        {
          Devices = new List<Device>();
        }
        public class Device
        {
            [Required]
            public string IMEI { get; set; }
            [Required]
            public string Model { get; set; }
        }


            public List<Device> Devices { get; set; }
            public string EmailAddress { get; set; }
            public string FirstName { get; set; }
          
            public string LastName { get; set; }

    }
}
