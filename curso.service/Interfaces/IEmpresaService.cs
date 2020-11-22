using curso.domain.Entities;
using curso.service.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace curso.service.Interfaces
{
    public interface IEmpresaService
    {
        EmpresaModel Obter(int id);

        IList<EmpresaModel> Listar();
    }
}
