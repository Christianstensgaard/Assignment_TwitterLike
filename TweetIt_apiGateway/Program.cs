using System.Text.Json.Serialization;
using RabbitMqDefault;
using RabbitMqDefault.Converter;
using RabbitMqDefault.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/create", (AccountModel model) =>
{


    return Results.Ok($"Creating a new account for {model.Username}");
})
.WithName("Create new Account").WithOpenApi();



app.MapPost("/logout", (string sessionKey) =>
{
  byte[] bodySteam = JsonStream<DefaultCallModel>.ToStream(new DefaultCallModel{
    sessionID = sessionKey
  });

  new CircuitBreaker(5, TimeSpan.FromSeconds(10)).Execute(
    action: ()=>{
      using var s = RMQ_Send.Send(RouteNames.Account_logout);
      s.Body = bodySteam;

      using var n = RMQ_Send.Send(RouteNames.Notify_user_logout);
      n.Body = bodySteam;
    },
    onFallback: ()=>{
      System.Console.WriteLine("Failed to Do the action");
    }
  );

})
.WithName("Logout").WithOpenApi();

app.MapPost("/friends", (string sessionKey) =>
{
    return Results.Ok("Loading All friends");
})
.WithName("Friends").WithOpenApi();

app.Run();



