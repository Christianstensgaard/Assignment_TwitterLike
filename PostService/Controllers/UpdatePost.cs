using Service;

namespace PostService.Controllers
{
    public class UpdatePost : ServiceFunction
    {
        public override void OnInit(FunctionConfig config)
        {
            config.FunctionName = "UpdatePost";
        }

        public override void OnRequest()
        {
            Console.WriteLine("Updating post!");
        }
    }
}