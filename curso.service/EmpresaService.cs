using AutoMapper;
using curso.domain.Entities;
using curso.domain.Interfaces.Repository;
using curso.service.Interfaces;
using curso.service.Models;
using System.Collections.Generic;

namespace curso.service
{
    public class EmpresaService: IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly IMapper _mapper;

        public EmpresaService(IEmpresaRepository empresaRepository, IMapper mapper)
        {
            _empresaRepository = empresaRepository;
            _mapper = mapper;
        }

        public EmpresaModel Obter(int id)
        {
            Empresa empresa = _empresaRepository.Obter(id);

            if(empresa == null)
                return null;
            return _mapper.Map<EmpresaModel>(empresa);
        }

        public IList<EmpresaModel> Listar()
        {
            return _mapper.Map<List<EmpresaModel>>(_empresaRepository.Listar());
        }
    }
}
