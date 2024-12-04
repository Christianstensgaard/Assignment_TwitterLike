using RabbitMqDefault.interfaces;

public class Sidecar<T> : ISidecar where T : ISidecar, new()
{
    private readonly T _factory;

  public Sidecar()
  {
    Initialize();
  }

  public T Get()
  {
    return _factory;
  }

  public void Initialize()
  {
    _factory.Initialize();
  }
}