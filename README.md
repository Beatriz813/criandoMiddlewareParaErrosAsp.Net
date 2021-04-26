# Criando Middleware para tratar erros no Asp.net core

- O projeto está usando a convenção por nome.

### Passo 1:
  Criar uma classe ExceptionMiddleware.cs que vai ter a lógica do middleware.
 
 ```C#
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
 ```

### Passo 2:
  Criar uma classe ExceptionMiddlewareExtension.cs para criar um método de extensão que irá injetar a classe ExceptionMiddleware na thread de execução do asp.net.
  
  ```C#
  public static class ExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
  ```
  
  ### Passo 3:
    Chamar o método de extensão UseExceptionMiddleware() na classe Startup. 
    O middleware de exception deve ser chamado antes dos outros middlewares.
    
 ```C#
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseExceptionMiddleware();       // Método de extensão feito na classe ExceptionMiddleWareExtension.cs
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
```
