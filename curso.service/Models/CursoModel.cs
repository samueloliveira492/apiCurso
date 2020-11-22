using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso.service.Models
{
    public class CursoModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
    }
}
