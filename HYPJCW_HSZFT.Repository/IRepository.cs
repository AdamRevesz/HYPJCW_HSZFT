﻿using HYPJCW_HSZFT.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Repository
{
        public interface IRepository<T> where T : class
        {
            List<T> ReadAll();
            T Read(string id);
            void Create(T item);
            void Update(T item);
            void Delete(string id);
        Task Create(EmployeeDto employeeDto);
    }
}
