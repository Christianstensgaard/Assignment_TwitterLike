using RabbitMqDefault.interfaces;

namespace PostService.Service;
public class CreatePost : AService
{
  public CreatePost(){
    // database = new MySqlDatabase("postuser", "postpassword","api_network", "PostServiceDb");
  }
  public override void onFallback()
  {

  }

  public override bool OnInit()
  {
    return true;
  }

  public override ServiceState OnInvoke(byte[] stream)
  {
    System.Console.WriteLine("Create Post Invoked!");
    string accountId   = "1";
    string postType    = "1";
    string postMessage = "Having a great day";

    Dictionary<string, object> columns = new Dictionary<string, object>
    {
      { "AccountId", accountId },
      { "PostMessage", postMessage },
      { "PostType", postType }
    };

    string insertQuery = database.CreateInsertQuery("Posts", columns);
    int rowsAffected = database.ExecuteNonQuery(insertQuery);

    if (rowsAffected > 0)
    {
        return ServiceState.Ok;
    }
    else
    {
        return ServiceState.Error;
    }
  }


  public MySqlDatabase database { get; set; }


}