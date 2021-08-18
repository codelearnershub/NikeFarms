﻿using NikeFarms.v2._0.Models;
using NikeFarms.v2._0.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikeFarms.v2._0.Interface
{
    public interface ISalesService
    {
        public Sales Add(SalesDTO salesDTO);

        public Sales FindById(int id);

        public Sales Update(int salesId, SalesDTO salesDTO);

        public void Delete(int id);
    }
}
