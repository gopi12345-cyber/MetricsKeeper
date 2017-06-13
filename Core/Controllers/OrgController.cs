using System;
using Core.Context;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Core.Data;
using Core.Repository;

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
        public IEnumerable<Org> Get()
        {
            //return _OrgRepo.GetAll();
            return _OrgRepo.AllIncluding(a =>a.Portfolios);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Org org)
        {
            if (ModelState.IsValid)
            {
                _OrgRepo.Add(org);
                _OrgRepo.Commit();
                return new OkObjectResult(org);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] Org org){
            if (ModelState.IsValid){
                _OrgRepo.Update(org);
                _OrgRepo.Commit();
                return new OkObjectResult(org);
            }
            else{
                return BadRequest(ModelState);
            }

        }

        [HttpGet("{id}")]
        public Org Get(int id){
            return _OrgRepo.GetSingle(id);
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
