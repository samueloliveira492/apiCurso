using curso.service.Interfaces;
using curso.service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace curso.application.Controllers
{
    public class CursoController : ControllerBase
    {
        private ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpPost]
        [Route("empresa/{idEmpresa}/curso")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Inserir(int idEmpresa, [FromBody] CursoModel item)
        {
            try
            {
                int id = _cursoService.Salvar(idEmpresa, item);

                return Created("Salvo com Sucesso!", id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                //logar mensagem de erro
                return StatusCode(500);
            }
        }

        [Route("empresa/{idEmpresa}/curso")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Atualizar(int idEmpresa, [FromBody] CursoModel item)
        {
            try
            {
                _cursoService.Atualizar(idEmpresa, item);

                return Ok(item.Id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                //logar mensagem de erro
                return StatusCode(500);
            }
        }

        [Route("empresa/{idEmpresa}/curso/{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Remover(int idEmpresa, int id)
        {
            try
            {
                _cursoService.Remover(idEmpresa, id);

                return Ok();
            }
            catch (Exception ex)
            {
                //logar mensagem de erro
                return StatusCode(500);
            }
        }

        [Route("empresa/{idEmpresa}/curso/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Obter(int idEmpresa, int id)
        {
            try
            {
                CursoModel curso = _cursoService.Obter(idEmpresa, id);
                if (curso == null)
                    return NoContent();
                return Ok(curso);
            }
            catch (Exception ex)
            {
                //logar mensagem de erro
                return StatusCode(500);
            }
        }

        [Route("empresa/{idEmpresa}/cursos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult Listar(int idEmpresa)
        {
            try
            {
                IList<CursoModel> cursos = _cursoService.Listar(idEmpresa);
                if (cursos == null || cursos.Count == 0)
                    return NoContent();

                return Ok(cursos);
            }
            catch(Exception ex)
            {
                //logar mensagem de erro
                return StatusCode(500);
            }
        }
    }
}
