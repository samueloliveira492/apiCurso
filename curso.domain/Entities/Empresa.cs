using System;
using System.Collections.Generic;
using System.Text;

namespace curso.domain.Entities
{
    public class Empresa: BaseEntity
    {
        public string Company { get; set; }
        public List<Curso> Courses { get; set; }
    }
}
