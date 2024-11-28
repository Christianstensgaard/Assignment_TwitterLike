namespace RabbitMqDefault.Models;
public class AccountModel{
  public int ID { get; set; }
  public string SessionKey { get; set; } = "";
  public string Username { get; set; } = "";
  public int State { get; set; }
}