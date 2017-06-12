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
            return _OrgRepo.GetAll();
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

    }
}
