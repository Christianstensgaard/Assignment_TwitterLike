namespace RabbitMqDefault;
public static class RouteNames{
  public const string Account_create = "account.create";
  public const string Account_delete = "account.delete";
  public const string Account_get = "account.get";
  public const string Account_update = "account.update";
  public const string Account_logout = "account.logout";
  public const string Account_login = "account.login";
  public const string Account_validate_new = "account.validate_new";
  public const string Account_validate_login = "account.validate_login";


  public const string Notify_user_logout = "notify.user_logout";


}

public static class Connect{
  public const string ConnectionString = "amqp://guest:guest@rabbitmq:5672";
}