using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samuelenera.BlazorAdmin.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Membership
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string excerpt { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public string author { get; set; }
        public string date_gmt { get; set; }
        public string modified { get; set; }
        public string modified_gmt { get; set; }
        public string group { get; set; }
        public string price { get; set; }
        public string period { get; set; }
        public string period_type { get; set; }
        public string signup_button_text { get; set; }
        public bool limit_cycles { get; set; }
        public string limit_cycles_num { get; set; }
        public string limit_cycles_action { get; set; }
        public string limit_cycles_expires_after { get; set; }
        public string limit_cycles_expires_type { get; set; }
        public bool trial { get; set; }
        public string trial_days { get; set; }
        public string trial_amount { get; set; }
        public string trial_once { get; set; }
        public string group_order { get; set; }
        public bool is_highlighted { get; set; }
        public string plan_code { get; set; }
        public string pricing_title { get; set; }
        public bool pricing_show_price { get; set; }
        public string pricing_display { get; set; }
        public string custom_price { get; set; }
        public string pricing_heading_txt { get; set; }
        public string pricing_footer_txt { get; set; }
        public string pricing_button_txt { get; set; }
        public string pricing_button_position { get; set; }
        public List<string> pricing_benefits { get; set; }
        public string register_price_action { get; set; }
        public string register_price { get; set; }
        public string thank_you_page_enabled { get; set; }
        public string thank_you_page_type { get; set; }
        public string thank_you_message { get; set; }
        public string thank_you_page_id { get; set; }
        public string custom_login_urls_enabled { get; set; }
        public string custom_login_urls_default { get; set; }
        public List<object> custom_login_urls { get; set; }
        public string expire_type { get; set; }
        public string expire_after { get; set; }
        public string expire_unit { get; set; }
        public string expire_fixed { get; set; }
        public bool tax_exempt { get; set; }
        public string tax_class { get; set; }
        public bool allow_renewal { get; set; }
        public string access_url { get; set; }
        public bool disable_address_fields { get; set; }
        public bool simultaneous_subscriptions { get; set; }
        public bool use_custom_template { get; set; }
        public string custom_template { get; set; }
        public bool customize_payment_methods { get; set; }
        public List<object> custom_payment_methods { get; set; }
        public bool customize_profile_fields { get; set; }
        public List<object> custom_profile_fields { get; set; }
        public string cannot_purchase_message { get; set; }
    }

    public class Member
    {
        public int id { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string nicename { get; set; }
        public string url { get; set; }
        public string message { get; set; }
        public string registered_at { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string display_name { get; set; }
    }

    public class MemberPressSubscriptionInfoModel
    {
        public bool coupon { get; set; }
        public Membership membership { get; set; }
        public Member member { get; set; }
        public string id { get; set; }
        public string subscr_id { get; set; }
        public string gateway { get; set; }
        public string price { get; set; }
        public string period { get; set; }
        public string period_type { get; set; }
        public string limit_cycles { get; set; }
        public string limit_cycles_num { get; set; }
        public string limit_cycles_action { get; set; }
        public string limit_cycles_expires_after { get; set; }
        public string limit_cycles_expires_type { get; set; }
        public string prorated_trial { get; set; }
        public string trial { get; set; }
        public string trial_days { get; set; }
        public string trial_amount { get; set; }
        public string status { get; set; }
        public string created_at { get; set; }
        public string total { get; set; }
        public string tax_rate { get; set; }
        public string tax_amount { get; set; }
        public string tax_desc { get; set; }
        public string tax_class { get; set; }
        public string cc_last4 { get; set; }
        public string cc_exp_month { get; set; }
        public string cc_exp_year { get; set; }
        public string token { get; set; }
        public string tax_compound { get; set; }
        public string tax_shipping { get; set; }
        public object response { get; set; }
    }

    public class Root
    {
        public List<MemberPressSubscriptionInfoModel> MyArray { get; set; }
    }




    //public class MemberPressSucriptionInfoModel
    //{

    //    public bool coupon { get; set; }
    //    public string subscr_id { get; set; }
    //    public string price { get; set; }
    //    public string period_type { get; set; }
    //    public string  status { get; set; }


    //    public List<Membership> membership { get; set; }
    //    public class Membership
    //    {
    //        public int id { get; set; }
    //        public string title { get; set; }
    //    }
    //}
}
