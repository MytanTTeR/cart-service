using Microsoft.AspNetCore.Builder;

namespace CartService.Extensions
{
    internal static class ApplicationExtensions
    {
        internal static void UseSwaggerCustom(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
