using EWallet.API.Swagger.Filters;
using EWallet.Application;
using EWallet.Infrastructure;
using EWallet.Infrastructure.Common.Filters;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddControllers(options =>
        {
            options.Filters.Add<BaseResponseActionResultFilter>();
        });

    builder.Services.AddHttpContextAccessor();
        
    builder.Services.AddFluentValidationAutoValidation();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.OperationFilter<RequiredHeaderParameter>();
    });

    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();
}

var app = builder.Build();
{
    await app.Services.InitializeDatabaseAsync();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.Use(async (context, next) =>
    {
        context.Request.EnableBuffering();
        await next();
    });
    
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
