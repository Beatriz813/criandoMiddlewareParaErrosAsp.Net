using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace criandoMiddlewareParaErros.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Teste : ControllerBase
    {
        [HttpGet]
        public IActionResult teste()
        {
            throw new Exception("djdgjhfg");
        }
    }
}
