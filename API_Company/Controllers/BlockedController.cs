using System;
using API_Company.Models;
using API_Company.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockedController : ControllerBase
    {
        private readonly BlockedService _blockedService;

        public BlockedController(BlockedService blockedService)
        {
            _blockedService = blockedService;
        }

        [HttpGet("{cnpj}", Name = "GetBlocked")]
        public ActionResult<Blocked> Get(string cnpj)
        {
            cnpj = FormatCnpj(cnpj);

            var blocked = _blockedService.Get(cnpj);
            if (blocked == null)
            {
                return NotFound();
            }

            return Ok(blocked);
        }

        private string FormatCnpj(string cnpj)
        {
                return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }
    }
}
