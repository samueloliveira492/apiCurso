using curso.domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace curso.domain.Interfaces.Repository
{
    public interface IEmpresaRepository
    {
        Empresa Obter(int id);

        IList<Empresa> Listar();
    }
}
