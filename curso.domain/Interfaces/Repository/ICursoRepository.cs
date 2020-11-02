using curso.domain.Entities;
using System.Collections.Generic;

namespace curso.domain.Interfaces.Repository
{
    public interface ICursoRepository
    {
        int Inserir(int idEmpresa, Curso curso);

        void Atualizar(int idEmpresa, Curso curso);

        void Remover(int idEmpresa, int id);

        Curso Obter(int idEmpresa, int id);

        IList<Curso> Listar(int idEmpresa);
    }
}
