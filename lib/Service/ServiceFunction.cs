using System.Threading.Tasks.Dataflow;

namespace  Service;
public abstract class ServiceFunction{

  public class FunctionConfig{
    public string FunctionName { get; set; } = "undefined";
  }

  public string ServiceClientName => ToolBox.RunTime.ServiceClientName;
  public string SerivceFunctionName => Settings.FunctionName;
  public FunctionConfig Settings { get; set; } = new FunctionConfig();

  internal byte[]? FunctionHeader {get;set;} 

  public abstract void OnInit(FunctionConfig config);
  public abstract void OnRequest();

}