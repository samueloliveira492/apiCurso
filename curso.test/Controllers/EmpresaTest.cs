using curso.application.Controllers;
using curso.domain.Entities;
using curso.domain.Interfaces.Repository;
using curso.service;
using curso.service.Interfaces;
using curso.service.Models;
using curso.test.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace curso.test
{
    [Collection("Mapper")]
    public class EmpresaTest
    {
        private readonly MapperFixture _mapper;

        public EmpresaTest(MapperFixture mapper)
        {
            _mapper = mapper;
        }

        [Fact]
        public void ObterEmpresa_Sucesso()
        {
            var empresaRepository = new Mock<IEmpresaRepository>();
            empresaRepository.Setup(repo => repo.Obter(1)).Returns(new Empresa()
            {
                Company = "testeCompany",
                Id = 1,
                Courses = new List<Curso>() {
                    new Curso(1, "cursoTeste", "descricaoTeste", "Active", "1"),
                    new Curso(2, "cursoTeste2", "descricaoTeste2", "Active", "51")
                }
            });
            var empresaService = new EmpresaService(empresaRepository.Object, _mapper.Mapper);
            var controller = new EmpresaController(empresaService);
            var resultado = controller.Obter(1);
            var objetoResultado = GetOkObject<EmpresaModel>(resultado);

            Assert.IsType<OkObjectResult>(resultado);
            Assert.Equal("testeCompany", objetoResultado.Company);
            Assert.Equal(1, objetoResultado.Id);
            Assert.True(objetoResultado.Courses.Count == 2);
        }

        [Fact]
        public void ObterEmpresa_NoContent()
        {
            var empresaRepository = new Mock<IEmpresaRepository>();
            empresaRepository.Setup(repo => repo.Obter(1)).Returns((Empresa) null);
            var empresaService = new EmpresaService(empresaRepository.Object, _mapper.Mapper);
            var controller = new EmpresaController(empresaService);
            var resultado = controller.Obter(1);

            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public void ListarEmpresa_Sucesso()
        {
            var empresaRepository = new Mock<IEmpresaRepository>();
            empresaRepository.Setup(repo => repo.Listar()).Returns(new List<Empresa>()
            {
                {
                    new Empresa()
                    {
                        Company = "testeCompany",
                        Id = 1,
                        Courses = new List<Curso>()
                    }
                },
                {
                    new Empresa()
                    {
                        Company = "testeCompany2",
                        Id = 1,
                        Courses = new List<Curso>()
                    }
                }
            });
            var empresaService = new EmpresaService(empresaRepository.Object, _mapper.Mapper);
            var controller = new EmpresaController(empresaService);
            var resultado = controller.Listar();
            var objetoResultado = GetOkObject<List<EmpresaModel>>(resultado);

            Assert.IsType<OkObjectResult>(resultado);
            Assert.True(objetoResultado.Count == 2);

        }

        [Fact]
        public void ListarCurso_NoContent()
        {
            var empresaRepository = new Mock<IEmpresaRepository>();
            empresaRepository.Setup(repo => repo.Listar()).Returns(new List<Empresa>());
            var empresaService = new EmpresaService(empresaRepository.Object, _mapper.Mapper);
            var controller = new EmpresaController(empresaService);
            var resultado = controller.Listar();

            Assert.IsType<NoContentResult>(resultado);

        }

        private T GetOkObject<T>(IActionResult result)
        {
            var okObjectResult = (OkObjectResult)result;
            return (T)okObjectResult.Value;
        }

    }
}
