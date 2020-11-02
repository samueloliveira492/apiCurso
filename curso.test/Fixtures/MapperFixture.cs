using AutoMapper;
using curso.service.Mapping;
namespace curso.test.Fixtures
{
    public class MapperFixture
    {
        public IMapper Mapper { get; }

        public MapperFixture()
        {
            var config = new MapperConfiguration(op => {
                op.AddProfile(new CursoMap());
                op.AddProfile(new EmpresaMap());
            });
            Mapper = config.CreateMapper();
        }
    }
}
