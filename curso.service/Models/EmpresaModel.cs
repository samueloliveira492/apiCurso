using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curso.service.Models
{
    public class EmpresaModel
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public IList<CursoModel> Courses { get; set; }
    }
}
