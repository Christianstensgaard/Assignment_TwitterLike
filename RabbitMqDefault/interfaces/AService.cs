namespace RabbitMqDefault.interfaces;
public abstract class AService{
  public AService(){

  }

  public RMQ_Recieve? consumer {get; private set;}

  //- Running the virtual voids, and analyze the return of each, inside the circuit-breaker, The fallback. will call the error on the broker sender. 
  public void Start(string connectionString, string routingKey){

    CircuitBreaker circuitBreaker = new CircuitBreaker(10, TimeSpan.FromSeconds(5));
    circuitBreaker.Execute(
      action: ()=>{
        if(!OnInit()) //- Invoke the Init void
          throw new Exception();
        consumer = new RMQ_Recieve(connectionString, routingKey);
        consumer.StartListening((message)=>{
          if(OnInvoke(message) == ServiceState.Ok) //- Invoke the Main void
            return payload;
          else return Error_return;
        });
      },
      onFallback: onFallback //- Invoke the fallback void
    );
  }

  public enum ServiceState{
    Ok = 0xF0,
    Error = 0xF1,
    Ok_payload = 0xF2,
    Error_payload = 0xF3,
  }

  public static byte[] Error_return = [0xff, 0xe2, 0x00];


  public byte[] payload { get; set; }

  public abstract bool OnInit();
  public abstract ServiceState OnInvoke(byte[] stream);
  public abstract void onFallback();
}