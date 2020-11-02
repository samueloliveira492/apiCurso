using curso.application.Controllers;
using curso.domain.Entities;
using curso.domain.Interfaces.Repository;
using curso.service;
using curso.service.Models;
using curso.test.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace curso.test
{
    [Collection("Mapper")]
    public class CursoTest
    {
        private readonly MapperFixture _mapper;

        public CursoTest(MapperFixture mapper)
        {
            _mapper = mapper;
        }

        [Fact]
        public void ObterCurso_Sucesso()
        {
            var cursoRepository = new Mock<ICursoRepository>();
            cursoRepository.Setup(repo => repo.Obter(1, 1)).Returns(new Curso(1, "testeName", "testeDescription", "Active", "1"));
            var cursoService = new CursoService(cursoRepository.Object, _mapper.Mapper);
            var controller = new CursoController(cursoService);
            var resultado = controller.Obter(1, 1);
            var objetoResultado = GetOkObject<CursoModel>(resultado);

            Assert.IsType<OkObjectResult>(resultado);
            Assert.Equal("testeName", objetoResultado.Name);
            Assert.Equal("testeDescription", objetoResultado.Description);
            Assert.Equal("Active", objetoResultado.Status);
            Assert.Equal("1", objetoResultado.Quantity);
        }

        [Fact]
        public void ObterCurso_NoContent()
        {
            var cursoRepository = new Mock<ICursoRepository>();
            cursoRepository.Setup(repo => repo.Obter(1, 1)).Returns((Curso)null);
            var cursoService = new CursoService(cursoRepository.Object, _mapper.Mapper);
            var controller = new CursoController(cursoService);
            var resultado = controller.Obter(1, 1);

            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact]
        public void ListarCurso_Sucesso()
        {
            var cursoRepository = new Mock<ICursoRepository>();
            cursoRepository.Setup(repo => repo.Listar(1)).Returns(new List<Curso>() {
                new Curso(1, "testeName", "testeDescription", "Active", "1"),
                new Curso(2, "testeName2", "testeDescription2", "Active", "3"),
            });
            var cursoService = new CursoService(cursoRepository.Object, _mapper.Mapper);
            var controller = new CursoController(cursoService);
            var resultado = controller.Listar(1);
            var objetoResultado = GetOkObject<List<CursoModel>>(resultado);

            Assert.IsType<OkObjectResult>(resultado);
            Assert.True(objetoResultado.Count == 2);
           
        }

        [Fact]
        public void ListarCurso_NoContent()
        {
            var cursoRepository = new Mock<ICursoRepository>();
            cursoRepository.Setup(repo => repo.Listar(1)).Returns(new List<Curso>());
            var cursoService = new CursoService(cursoRepository.Object, _mapper.Mapper);
            var controller = new CursoController(cursoService);
            var resultado = controller.Listar(1);

            Assert.IsType<NoContentResult>(resultado);

        }

        [Fact]
        public void InserirCurso_Sucesso()
        {
            var cursoRepository = new Mock<ICursoRepository>();
            cursoRepository.Setup(repo => repo.Inserir(1, It.IsAny<Curso>())).Returns(3);
            var cursoService = new CursoService(cursoRepository.Object, _mapper.Mapper);
            var controller = new CursoController(cursoService);
            var resultado = controller.Inserir(1, new CursoModel()
            {
                Name = "name",
                Description = "description",
                Quantity = "1"
            });
            var objetoResultado = GetCreatedObject<int>(resultado);

            Assert.IsType<CreatedResult>(resultado);
            Assert.True(objetoResultado == 3);

        }

        [Fact]
        public void InserirCurso_BadRequest_Name()
        {
            var cursoService = new CursoService(null, _mapper.Mapper);
            var controller = new CursoController(cursoService);
            var resultado = controller.Inserir(1, new CursoModel()
            {
                Description = "description",
                Quantity = "1"
            });

            Assert.IsType<BadRequestObjectResult>(resultado);
        }

        [Fact]
        public void InserirCurso_BadRequest_Quantity()
        {
            var cursoService = new CursoService(null, _mapper.Mapper);
            var controller = new CursoController(cursoService);
            var resultado = controller.Inserir(1, new CursoModel()
            {
                Name = "name",
                Description = "description"
            });

            Assert.IsType<BadRequestObjectResult>(resultado);
        }

        [Fact]
        public void AlterarCurso_BadRequest_Name()
        {
            var cursoService = new CursoService(null, _mapper.Mapper);
            var controller = new CursoController(cursoService);
            var resultado = controller.Atualizar(1, new CursoModel()
            {
                Description = "description",
                Quantity = "1"
            });

            Assert.IsType<BadRequestObjectResult>(resultado);

        }

        [Fact]
        public void AlterarCurso_BadRequest_Quantity()
        {
            var cursoService = new CursoService(null, _mapper.Mapper);
            var controller = new CursoController(cursoService);
            var resultado = controller.Atualizar(1, new CursoModel()
            {
                Name = "name",
                Description = "description"
            });

            Assert.IsType<BadRequestObjectResult>(resultado);
        }

        private T GetOkObject<T>(IActionResult result)
        {
            var okObjectResult = (OkObjectResult)result;
            return (T)okObjectResult.Value;
        }

        private T GetCreatedObject<T>(IActionResult result)
        {
            var createdObjectResult = (CreatedResult)result;
            return (T)createdObjectResult.Value;
        }

    }
}
