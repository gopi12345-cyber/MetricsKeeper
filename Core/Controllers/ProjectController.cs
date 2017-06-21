using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Repository;
using Core.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Core.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private IProjectRepository _ProjectRepo;

        public ProjectController(IProjectRepository repo)
        {
            _ProjectRepo = repo;
        }

        [HttpGet]
        public IEnumerable<Project> GetAll()
        {
            return _ProjectRepo.GetAll();
        }

        [HttpGet("{id}")]
        public Project GetSingle(int id){
            return _ProjectRepo.GetSingle(x=>x.Id == id);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Project project){
            if (ModelState.IsValid){
                _ProjectRepo.Add(project);
                _ProjectRepo.Commit();
                return new OkObjectResult(project);
            }
            else{
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody]Project project)
        {
            Project item = _ProjectRepo.GetSingle(project.Id);
            if (item == null){
                return NotFound();
            }
            else if (ModelState.IsValid){
                _ProjectRepo.Update(item);
                _ProjectRepo.Commit();
                return new OkObjectResult(item);
            }
            else{
                return BadRequest(ModelState);
            }
        }

    }
}
