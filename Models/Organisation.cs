using System;
using System.Reflection;

namespace pis.Models
{
	public class Organisation
	{
        public string OrgName { get; set; } = "";

        public int INN { get; set; } 

        public int KPP { get; set; }

        public string AdressReg { get; set; } = "";

        public string TypeOrg { get; set; } = "";

        public string OrgAttribute { get; set; } = "";//ИП/Юр.лицо

        public string Locality { get; set; } = "";

        public Organisation(string orgName, int inn, int kpp, string adressReg, string typeOrg, string orgAttribute, string locality)
        {
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

