using Service;

namespace PostService.Controllers
{
    public class DeletePost : ServiceFunction
    {
        public override void OnInit(FunctionConfig config)
        {
            config.FunctionName = "DeletePost";
        }

        public override void OnRequest()
        {
            Console.WriteLine("Deleting post");
        }
    }
}