﻿using Project.Business.Middleware;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business
{
    public class WateringHistoryService : BaseService<WateringHistory>
    {
        public ServiceResult<bool> CheckIfHasBeenWatered(int plantId)
        {
            DateTime date = DateTime.Now.AddDays(-1);
            var entity = _repo.GetAll(x => x.CreatedTime.Day > date.Day && x.PlantId == plantId);
            if (entity.ToList().Count >= 1)
                return new ServiceResult<bool>(true);
            return new ServiceResult<bool>(false);
        }
    }
}