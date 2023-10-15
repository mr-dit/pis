using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using NUnit.Framework;
using pis.Services;

namespace pis.Models
{
	public class Organisation
	{
        [Key]
        public int OrgId { get; set; }

		public string OrgName { get; set; }

        public string INN { get; set; } 

        public string KPP { get; set; }

        public string AdressReg { get; set; }

        public int OrgTypeId { get; set; }
        public OrgType OrgType { get; set; }

        public int LocalityId { get; set; }
        public Locality Locality { get; set; }

        public List<User> Users { get; set; }

        public List<Contract> Contracts { get; set; }


        //public Organisation(int orgId, string orgName, string iNN, string kPP, 
        //    string adressReg, OrgType orgType, Locality locality, List<User> users, List<Contract> contracts)
        //{
        //    OrgId = orgId;
        //    OrgName = orgName;
        //    INN = iNN;
        //    KPP = kPP;
        //    AdressReg = adressReg;
        //    OrgType = orgType;
        //    Locality = locality;
        //    Users = users;
        //    Contracts = contracts;
        //}
    }
}

