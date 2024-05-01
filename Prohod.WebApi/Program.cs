using System.Text.Json.Serialization;
using Prohod.WebApi.Accounts.Configuration;
using Prohod.WebApi.Configuration;
using Prohod.WebApi.Errors;
using Prohod.WebApi.Forms;
using Prohod.WebApi.Users;
using Prohod.WebApi.VisitRequests.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCors()
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services
    .AddSwagger()
    .AddAccountsServices()
    .AddPostgresDbContext(builder.Configuration)
    .AddOperationErrorVisitor()
    .AddVisitRequestsServices()
    .AddUsersServices()
    .AddFormsServices();

var app = builder.Build();


app.UseHsts();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors(builder => builder.AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();