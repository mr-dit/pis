﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using NUnit.Framework;
using pis.Services;
using pis_web_api.Services;

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
        public OrgType? OrgType { get; set; }

        public int LocalityId { get; set; }
        public Locality? Locality { get; set; }

        public List<User>? Users { get; set; }

        public List<Contract>? ContractsAsPerformer { get; set; }
        public List<Contract>? ContractsAsCustomer { get; set; }


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
        public Organisation() { }

        public Organisation(string orgName, string iNN, string kPP, string adressReg, int orgTypeId, int localityId)
        {
            OrgName = orgName;
            INN = iNN;
            KPP = kPP;
            AdressReg = adressReg;
            OrgTypeId = orgTypeId;
            LocalityId = localityId;
        }

        public bool HasUser(int userId)
        {
            var user = new UserService().GetEntry(userId);
            var users = new UserService().GetUsersByOrganisation(this.OrgId);

            if (users is null || users.Count() == 0)
                throw new Exception("В экземпляре организации нет пользователей");
            return users.Contains(user);
        }
    }
}

