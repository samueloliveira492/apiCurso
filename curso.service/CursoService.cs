using AutoMapper;
using curso.domain.Entities;
using curso.domain.Interfaces.Repository;
using curso.service.Interfaces;
using curso.service.Models;
using curso.util;
using System;
using System.Collections.Generic;

namespace curso.service
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _repository;
        private readonly IMapper _mapper;

        public CursoService(ICursoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public int Salvar(int idEmpresa, CursoModel cursoModel)
        {
            ValidaRequisicao(cursoModel);
            cursoModel.Status = Constantes.Status.active;
            return _repository.Inserir(idEmpresa, _mapper.Map<Curso>(cursoModel));
        }

        public void Atualizar(int idEmpresa, CursoModel cursoModel)
        {
            ValidaRequisicao(cursoModel);
            _repository.Atualizar(idEmpresa, _mapper.Map<Curso>(cursoModel));
        }

        public void Remover(int idEmpresa, int id)
        {
            _repository.Remover(idEmpresa, id);
        }

        public CursoModel Obter(int idEmpresa, int id)
        {
            Curso curso = _repository.Obter(idEmpresa, id);
            if (curso == null)
                return null;
            return _mapper.Map<CursoModel>(curso);
        }

        public IList<CursoModel> Listar(int idEmpresa)
        {
            return _mapper.Map<List<CursoModel>>(_repository.Listar(idEmpresa));
        }

        private void ValidaRequisicao(CursoModel cursoModel)
        {
            if (cursoModel == null)
                throw new ArgumentException("Curso não pode ser nulo.");

            if (string.IsNullOrEmpty(cursoModel.Name))
                throw new ArgumentException("É necessário informar o nome do curso.");

            if (string.IsNullOrEmpty(cursoModel.Quantity))
                throw new ArgumentException("É ncessário informar a quantidade de alunos.");

            if(!int.TryParse(cursoModel.Quantity, out _))
                throw new ArgumentException("Quantidade deve ser um número.");
        }
    }
}
