﻿using HYPJCW_HSZFT.Entities.Dependencies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Models.Entity_Models
{
    [ToExport]
    public class Managers
    {
        public string Name { get; set; }
        [Key]
        public string ManagerId { get; set; }
        public int BirthYear { get; set; }
        public string StartOfEmployment { get; set; }
        public bool HasMba { get; set; }
        public Managers()
        {
            
        }

        public Managers(string name, string managerId, int birthYear, string startOfEmployment, bool hasMBA)
        {
            Name = name;
            ManagerId = managerId;
            BirthYear = birthYear;
            StartOfEmployment = startOfEmployment;
            HasMba = hasMBA;
        }

        public override string ToString()
        {
            return $"Name: {Name}" +
                $"Manager Id: {ManagerId}" +
                $"Birthyear: {BirthYear}" +
                $"Start of Employment {StartOfEmployment}" +
                $"MBA: {(HasMba ? "Has MBA" : "Does not have MBA")}";
        }
    }
}
