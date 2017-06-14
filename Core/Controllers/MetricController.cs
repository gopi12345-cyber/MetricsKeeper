using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Repository;
using Microsoft.AspNetCore.Mvc;
using Core.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Core.Controllers
{
    [Route("api/[controller]")]
    public class MetricController : Controller
    {

        private IMetricRepository _MetricRepo;
        public MetricController(IMetricRepository repo){
            _MetricRepo = repo;
        }

        [HttpGet]
        public IEnumerable<Metric> Get()
        {
            return _MetricRepo.GetAll();
        }


        [HttpPost]
        public IActionResult Post([FromBody]Metric item)
        {
            if (ModelState.IsValid){
                _MetricRepo.Add(item);
                _MetricRepo.Commit();
                return new OkObjectResult(item);
            }
            else{
                return BadRequest(ModelState);
            }
        }


    }
}
