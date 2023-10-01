using System;
using System.Reflection;
using pis.Services;

namespace pis.Models
{
	public class Organisation
	{
		public int OrgId { get; set; }
		public string OrgName { get; set; }

        public string INN { get; set; } 

        public string KPP { get; set; }

        public string AdressReg { get; set; }

        public OrgType OrgType { get; set; }

        public string OrgAttribute { get; set; }

        public string Locality { get; set; }
        
        public Organisation(int orgId, string orgName, string inn, string kpp, string adressReg, OrgType orgType, string orgAttribute, string locality)
        {
	        OrgId = orgId;
            OrgName = orgName;
            INN = inn;
            KPP = kpp;
            AdressReg = adressReg;
            OrgType = orgType;
            OrgAttribute = orgAttribute;
            Locality = locality;
        }

        public Organisation()
		{
		}
	}
}

