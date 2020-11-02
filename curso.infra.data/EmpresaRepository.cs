using curso.domain.Entities;
using curso.domain.Interfaces.Repository;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace curso.infra.data
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly IConfiguration _configuration;

        public EmpresaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Empresa Obter(int idEmpresa)
        {
            var json = File.ReadAllText(_configuration.GetSection("db").Value);
            var jObject = JObject.Parse(json);
            try
            {
                List<Empresa> empresas = JsonConvert.DeserializeObject<List<Empresa>>(jObject["contents"].ToString());
                return empresas.FirstOrDefault(e=> e.Id == idEmpresa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<Empresa> Listar()
        {
            var json = File.ReadAllText(_configuration.GetSection("db").Value);
            var jObject = JObject.Parse(json);
            try
            {
                List<Empresa> empresas = JsonConvert.DeserializeObject<List<Empresa>>(jObject["contents"].ToString());
                foreach(var empresa in empresas)
                {
                    empresas.FirstOrDefault(e=>e.Id == empresa.Id).Courses = empresa.Courses.Where(c => c.Status.Equals("Active")).ToList();
                }
                return empresas.OrderBy(e=> e.Company).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
