using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class CardReader
    {
        public string nat_id { get; set; }
        public string title_th { get; set; }
        public string fname_th { get; set; }
        public string mname_th { get; set; }
        public string sname_th { get; set; }
        public string title_en { get; set; }
        public string fname_en { get; set; }
        public string mname_en { get; set; }
        public string sname_en { get; set; }
        public string address_no { get; set; }
        public string address_moo { get; set; }
        public string address_trok { get; set; }
        public string address_soi { get; set; }
        public string address_road { get; set; }
        public string address_tumbol { get; set; }
        public string address_amphor { get; set; }
        public string address_provinice { get; set; }
        public string gender { get; set; }
        public string birthdate { get; set; }
        public string issuer { get; set; }
        public string photo { get; set; }
        public string issue_date { get; set; }
        public string issue_expire { get; set; }
    }
}
