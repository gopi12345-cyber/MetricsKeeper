using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Data;
using Core.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Core.Controllers
{
    [Route("api/[controller]")]
    public class PortfolioController : Controller
    {
        private IPortfolioRepository _PortfolioRepo; 
        public PortfolioController(IPortfolioRepository repository){
            _PortfolioRepo = repository;
        }

        [HttpGet]
        public IEnumerable<Portfolio> GetAll(){
            return _PortfolioRepo.AllIncluding(a=>a.Organization);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Portfolio item){
            if (ModelState.IsValid){
                _PortfolioRepo.Add(item);
                _PortfolioRepo.Commit();
                return new OkObjectResult(item);
            }
            else{
                return BadRequest(ModelState);
            }
        }


    }
}
