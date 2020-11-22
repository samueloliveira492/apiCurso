using curso.domain.Entities;
using curso.service.Models;
using AutoMapper;
using System.Collections.Generic;

namespace curso.service.Mapping
{
    public class EmpresaMap : Profile
    {
        public EmpresaMap()
        {
            CreateMap<Empresa, EmpresaModel>()
                .ForMember(dest => dest.Courses, act => act.MapFrom(src => src.Courses));
            CreateMap<EmpresaModel, Empresa>()
                .ForMember(dest => dest.Courses, act => act.MapFrom(src => src.Courses));
        }
    }
}
