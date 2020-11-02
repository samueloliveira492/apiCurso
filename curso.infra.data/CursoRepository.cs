using curso.domain.Entities;
using curso.domain.Interfaces.Repository;
using curso.util;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace curso.infra.data
{
    public class CursoRepository : ICursoRepository 
    {
        private readonly IConfiguration _configuration;
        public CursoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int Inserir(int idEmpresa, Curso curso)
        {
            var json = File.ReadAllText(_configuration.GetSection("db").Value);
            var jObject = JObject.Parse(json);
            try
            {
                List<Empresa> empresas = JsonConvert.DeserializeObject<List<Empresa>>(jObject["contents"].ToString());
                Empresa empresa = empresas.FirstOrDefault(e => e.Id == idEmpresa);
                if (empresa == null)
                    throw new Exception("Empresa não existe.");
                curso.Id = empresa.Courses.OrderByDescending(c=>c.Id).FirstOrDefault().Id+1;
                empresa.Courses.Add(curso);
                string novoArquivo = JsonConvert.SerializeObject(empresas, Formatting.Indented);
                File.WriteAllText(_configuration.GetSection("db").Value, "{\"contents\":" + novoArquivo + "}");
                return curso.Id;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Atualizar(int idEmpresa, Curso curso)
        {
            var json = File.ReadAllText(_configuration.GetSection("db").Value);
            var jObject = JObject.Parse(json);
            try
            {
                List<Empresa> empresas = JsonConvert.DeserializeObject<List<Empresa>>(jObject["contents"].ToString());
                Empresa empresa = empresas.FirstOrDefault(e => e.Id == idEmpresa);
                if (empresa == null)
                    throw new Exception("Empresa não existe.");
                    
                if(empresa.Courses.FirstOrDefault(c => c.Id == curso.Id) == null )
                {
                    throw new Exception("Curso não existe");
                } else
                {
                    Curso cursoSalvo = empresa.Courses.FirstOrDefault(c => c.Id == curso.Id);
                    cursoSalvo.Name = curso.Name;
                    cursoSalvo.Description = curso.Description;
                    cursoSalvo.Quantity = curso.Quantity;
                }

                empresas.FirstOrDefault(e => e.Id == idEmpresa).Courses = empresa.Courses;
                string novoArquivo = JsonConvert.SerializeObject(empresas, Formatting.Indented);
                File.WriteAllText(_configuration.GetSection("db").Value, "{\"contents\":" + novoArquivo + "}");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remover(int idEmpresa, int id)
        {
            var json = File.ReadAllText(_configuration.GetSection("db").Value);
            var jObject = JObject.Parse(json);
            try
            {
                List<Empresa> empresas = JsonConvert.DeserializeObject<List<Empresa>>(jObject["contents"].ToString());
                Empresa empresa = empresas.FirstOrDefault(e => e.Id == idEmpresa);
                if (empresa == null)
                    throw new Exception("Empresa não existe.");

                empresa.Courses.FirstOrDefault(c => c.Id == id).Status = Constantes.Status.inactive;
                if (empresa.Courses.FirstOrDefault(c => c.Id == id) == null)
                {
                    throw new Exception("Curso não existe");
                } else
                {
                    empresas.FirstOrDefault(e => e.Id == idEmpresa).Courses = empresa.Courses;
                    string novoArquivo = JsonConvert.SerializeObject( empresas, Formatting.Indented);
                    File.WriteAllText(_configuration.GetSection("db").Value, "{\"contents\":" + novoArquivo + "}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Curso Obter(int idEmpresa, int id)
        {
            var json = File.ReadAllText(_configuration.GetSection("db").Value);
            var jObject = JObject.Parse(json);
            try
            {
                List<Empresa> empresas = JsonConvert.DeserializeObject<List<Empresa>>(jObject["contents"].ToString());
                Empresa empresa = empresas.FirstOrDefault(e => e.Id == idEmpresa);
                if (empresa == null)
                    throw new Exception("Empresa não existe");
                return empresa.Courses.FirstOrDefault(c => c.Id == id && c.Status.Equals("Active"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<Curso> Listar(int idEmpresa)
        {
            var json = File.ReadAllText(_configuration.GetSection("db").Value);
            var jObject = JObject.Parse(json);
            try
            {
                List<Empresa> empresas = JsonConvert.DeserializeObject<List<Empresa>>(jObject["contents"].ToString());
                Empresa empresa = empresas.FirstOrDefault(e => e.Id == idEmpresa);
                if (empresa == null)
                    throw new Exception("Empresa não existe");
                return empresa.Courses.Where(c=> c.Status.Equals("Active")).OrderBy(c=>c.Name).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
