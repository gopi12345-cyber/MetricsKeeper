using System;
using Core.Context;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Core.Data;
using Core.Repository;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using Core.Tools;
using Microsoft.Extensions.Logging;
using AdysTech.InfluxDB.Client.Net;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    public class OrgController : Controller
    {
        private IOrgRepository _OrgRepo;

        public OrgController(IOrgRepository orgrepo)
        {
            _OrgRepo = orgrepo;

                  
        }

        [HttpGet]
        public async Task<IEnumerable<Org>> Get([FromQuery]bool expand = false)
        {
            if (expand == true)
            {
                return await _OrgRepo.AllIncluding(a => a.Portfolios);

            }
            else{
                return await _OrgRepo.GetAll();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Org org)
        {
            if (ModelState.IsValid)
            {
                _OrgRepo.Add(org);
                _OrgRepo.Commit();
                ConfigurationReader conf = new ConfigurationReader();
				using (InfluxDBClient _influxClient = new InfluxDBClient(conf.Configuration["Data:InfluxDBHost"],
										 conf.Configuration["Data:InfluxDBLogin"],
                                                           conf.Configuration["Data:InfluxDBPassword"])){
                    if (await _influxClient.CreateDatabaseAsync("org"+org.Id.ToString()) == true){
						return new OkObjectResult(org);
                    }
                    else{
                        _OrgRepo.Delete(org);
                        _OrgRepo.Commit();
                        return new ObjectResult("Could not geneate metrics storage database, the organization was not created!");
                    }
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] Org org){
            if (_OrgRepo.GetSingle(org.Id) == null){
                return NotFound();
            }
            else if (ModelState.IsValid){
                _OrgRepo.Update(org);
                _OrgRepo.Commit();
                return new OkObjectResult(org);
            }
            else{
                return BadRequest(ModelState);
            }

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id){
            Org item = _OrgRepo.GetSingle(x=>x.Id == id, a => a.Portfolios);
            if (item == null){
                return NotFound();
            }
            else{
                return new ObjectResult(item);
            }
        }

        [Route("count")]
        [HttpGet]
        public async Task<int> Count(){
            return await _OrgRepo.Count();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            Org _ItemToDeleted = _OrgRepo.GetSingle(id);
            if (_ItemToDeleted == null){
                return NotFound();
            }
            else{
                _OrgRepo.Delete(_ItemToDeleted);
                _OrgRepo.Commit();
                return new OkResult();
            }
        }
    }
}
