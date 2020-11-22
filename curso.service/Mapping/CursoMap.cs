using AutoMapper;
using curso.domain.Entities;
using curso.service.Models;

namespace curso.service.Mapping
{
    public class CursoMap: Profile
    {
        public CursoMap()
        {
            CreateMap<Curso, CursoModel>();
            CreateMap<CursoModel, Curso>();
        }
        
    }
}
