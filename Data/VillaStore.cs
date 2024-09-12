using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using magic_villa.Models.Dto;

namespace magic_villa.Data
{
    public class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
          new VillaDto{Id=2, Name="Pool View"},
          new VillaDto{Id=1, Name="Beach View"}
        };
    }
}