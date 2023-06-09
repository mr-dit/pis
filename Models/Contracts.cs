﻿using System;
using pis.Services;

namespace pis.Models
{
    public class Contracts
    {
        public int ContractsId { get; set; }
        public DateTime ConclusionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Organisation Performer { get; set; }
        public string Customer { get; set; }

        public Contracts(int conrId, DateTime conclDate, DateTime expDate, Organisation perfomer, string customer)
        {
            ContractsId = conrId;
            ConclusionDate = conclDate;
            ExpirationDate = expDate;
            Performer = perfomer;
            Customer = customer;
        }

        public Contracts()
        {
        }
    }
}