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
        public async Task<IEnumerable<Portfolio>> GetAll([FromQuery]bool expand = false){
            if (expand == true){
                return await _PortfolioRepo.AllIncluding(a => a.Projects);


            }
            else{
                return await _PortfolioRepo.GetAll();
            }
        }

        [HttpGet("{id}")]
        public Portfolio GetSingle(int id){
            return _PortfolioRepo.GetSingle(i => i.Id == id, a => a.Projects);
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

        [HttpPut]
        public IActionResult Update([FromBody]Portfolio item){
            if (_PortfolioRepo.GetSingle(item.Id) == null){
                return NotFound();
            }
            else if (ModelState.IsValid){
                _PortfolioRepo.Update(item);
                _PortfolioRepo.Commit();
                return new OkObjectResult(item);
            }
            else{
                return BadRequest(ModelState);
            }            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            var item = _PortfolioRepo.GetSingle(id);
            if (item == null){
                return NotFound();
            }
            else{
                _PortfolioRepo.Delete(item);
                _PortfolioRepo.Commit();
                return new OkResult();
            }
        }

    }
}
