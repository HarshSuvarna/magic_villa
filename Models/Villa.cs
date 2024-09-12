using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magic_villa.Models
{
    public class Villa
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime createdAt { get; set; }
    }
}