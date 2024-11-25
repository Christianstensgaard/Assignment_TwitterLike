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