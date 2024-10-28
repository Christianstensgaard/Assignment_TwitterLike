using Service;

namespace DbHandler.controllers
{
    public class PostDbManager : ServiceFunction
    {
        public override void OnInit(FunctionConfig config)
        {
            config.FunctionName = "PostDbManager";
        }

        public override void OnRequest()
        {
            Console.WriteLine("Post Db Manager");
        }
    }
}