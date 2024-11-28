namespace RabbitMqDefault.interfaces;
public abstract class AService{
  public AService(){

  }


  public RMQ_Recieve? consumer {get; private set;}

  public void Start(string connectionString, string routingKey){
    CircuitBreaker circuitBreaker = new CircuitBreaker(10, TimeSpan.FromSeconds(5));
    circuitBreaker.Execute(
      action: ()=>{
        if(!OnInit())
          throw new Exception();
        consumer = new RMQ_Recieve(connectionString, routingKey);
        consumer.StartListening((message)=>{
          ServiceState state = OnInvoke(message);
          return [0xff];
        });
      },
      onFallback: onFallback
    );
  }

  public enum ServiceState{
    Ok = 0xF0,
    Error = 0xF1
  }

  public abstract bool OnInit();
  public abstract ServiceState OnInvoke(byte[] stream);
  public abstract void onFallback();






}