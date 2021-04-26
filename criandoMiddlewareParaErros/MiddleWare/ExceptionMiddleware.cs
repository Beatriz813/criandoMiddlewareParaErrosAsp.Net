using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace criandoMiddlewareParaErros.MiddleWare
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);   // Chama a Proxima thread
            } catch(Exception e)
            {
                var obj = new
                {
                    messageErro = e.Message
                };
                var message = JsonConvert.SerializeObject(obj);
               await context.Response.WriteAsync($"Deu um Erro {message}");
            }
        }
    }
}
