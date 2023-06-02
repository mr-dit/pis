using System;
using System.Reflection;
using pis.Services;

namespace pis.Models
{
	public class Organisation
	{
		public int OrgId { get; set; }
		public string OrgName { get; set; } = "";

        public long INN { get; set; } 

        public long KPP { get; set; }

        public string AdressReg { get; set; } = "";

        public string TypeOrg { get; set; } = "";

        public string OrgAttribute { get; set; } = "";

        public string Locality { get; set; } = "";
        
        public Organisation(int orgId, string orgName, long inn, int kpp, string adressReg, string typeOrg, string orgAttribute, string locality)
        {
	        OrgId = orgId;
            OrgName = orgName;
            INN = inn;
            KPP = kpp;
            AdressReg = adressReg;
            TypeOrg = typeOrg;
            OrgAttribute = orgAttribute;
            Locality = locality;
        }

        public Organisation()
		{
		}
	}
}

