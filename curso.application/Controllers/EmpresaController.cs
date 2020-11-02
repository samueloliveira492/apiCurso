using curso.service.Interfaces;
using curso.service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace curso.application.Controllers
{
    [Route("empresa")]
    public class EmpresaController: ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Obter(int id)
        {
            try
            {
                EmpresaModel empresa = _empresaService.Obter(id);
                if (empresa == null)
                    return NoContent();
                return Ok(empresa);
            }
            catch (Exception ex)
            {
                //logar mensagem de erro
                return StatusCode(500);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Listar()
        {
            try
            {
                IList<EmpresaModel> empresas = _empresaService.Listar();
                if (empresas == null || empresas.Count == 0)
                    return NoContent();
                return Ok(empresas);
            }
            catch (Exception ex)
            {
                //logar mensagem de erro
                return BadRequest(ex.Message);
            }
        }
    }
}
