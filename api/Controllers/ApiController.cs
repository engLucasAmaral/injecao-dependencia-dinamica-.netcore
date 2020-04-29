using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using core.InectionDependency;
using core.Models;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet("requestscoped")]
        public object GetRequestScoped([FromServices] ContadorRequestScoped _contadorRequestScoped)
        {   
            _contadorRequestScoped.Incrementar();

            return new
            {
                _contadorRequestScoped._valorAtual,
                instance = _contadorRequestScoped._guid                   
            };           
        }

        [HttpGet("singleton")]
        public object GetSingleton([FromServices] ContadorSingleton _contadorSingleton)
        {
            
            _contadorSingleton.Incrementar();

            return new
            {
                _contadorSingleton._valorAtual,
                instance = _contadorSingleton._guid                   
            };           
        }


         [HttpGet("transient")]
        public object GetTransient([FromServices] ContadorTransient _contadorTransient)
        {
            _contadorTransient.Incrementar();

            return new
            {
                _contadorTransient._valorAtual,
                instance = _contadorTransient._guid                   
            };           
        }

     
    }
}
