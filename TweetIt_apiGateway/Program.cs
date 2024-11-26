using System.Text;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using RabbitMqDefault;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("ocelot.json",false,false);

builder.Services.AddOcelot(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseOcelot().Wait();
app.UseHttpsRedirection();




app.MapPost("/account/create", async (string accountname, string passHash) => {
    string returnMessage = "";
    CircuitBreaker circuitBreaker = new CircuitBreaker(5, TimeSpan.FromSeconds(3));
    await circuitBreaker.ExecuteAsync(async () =>
    {
      //- Checking the account name, and return the validation state of the service call
      using RMQ_Send accValidate = RMQ_Send.Send(RouteNames.Account_validate_new);
      accValidate.Body = Encoding.UTF8.GetBytes(accountname);
      string validation = await accValidate.SendAndAwaitResponseAsync(TimeSpan.FromSeconds(2));

      //- creating the account
      using RMQ_Send accCreate = RMQ_Send.Send(RouteNames.Account_create);
      accCreate.Body = Encoding.UTF8.GetBytes(accountname + passHash);
      string state = await accCreate.SendAndAwaitResponseAsync(TimeSpan.FromSeconds(2));
      returnMessage = "Account created!";
    },
    onFallback: () =>
    {
      returnMessage = "failed to create account!";

    });
    return Results.Ok(returnMessage);
});






app.MapGet("/ping", (string e) =>
{
    return "ping";
})
.WithName("Create new Account").WithOpenApi();


app.MapGet("/pong", (string e) =>
{
    return "pong";
})
.WithName("Create new Account").WithOpenApi();

app.Run();



