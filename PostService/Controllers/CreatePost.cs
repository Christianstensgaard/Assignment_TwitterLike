using Service;

namespace PostService.Controllers
{
    public class CreatePost : ServiceFunction
    {
        public override void OnInit(FunctionConfig config)
        {
            config.FunctionName = "CreatePost";
        }

        public override void OnRequest()
        {
            Console.WriteLine("Creating Post!");
        }
    }
}