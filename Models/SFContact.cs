using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FXPostgresMap.Models
{
    public class SFContact
    {
        public string masterrecordid { get; set; }
        public string sfid { get; set; }
        public string firstname { get; set; }
        public DateTime createddate { get; set; }
        public string cookieid__c { get; set; }
        public string name { get; set; }
        public DateTime systemmodstamp { get; set; }
        public bool isdeleted { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public int id { get; set; }
        public string _hc_err { get; set; }
        public bool responded_to_vip_survey__c { get; set; }
        public bool followed_vip_on_facebook__c { get; set; }
        public string eventsource__c { get; set; }
        public string accountid { get; set; }
        public bool entrolled_in_vip__c { get; set; }
        public bool browsed_vip_website__c { get; set; }
    }
}
