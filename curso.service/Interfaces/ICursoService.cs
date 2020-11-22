using curso.domain.Entities;
using curso.service.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace curso.service.Interfaces
{
    public interface ICursoService
    {
        int Salvar(int idEmpresa, CursoModel cursoModel);

        void Atualizar(int idEmpresa, CursoModel cursoModel);

        void Remover(int idEmpresa, int id);

        CursoModel Obter(int idEmpresa, int id);

        IList<CursoModel> Listar(int idEmpresa);
    }
}
