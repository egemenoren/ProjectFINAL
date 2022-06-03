using ProjectFINAL.ApiModel;
using ProjectFINAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFINAL.ViewModels
{
    public class IndexViewModel
    {
        public WeatherResponseModel Weather { get; set; }
        public List<PlantDTO> PlantInformation { get; set; }
    }
}