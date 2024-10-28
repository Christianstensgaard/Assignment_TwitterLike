using Service;

namespace PostService.Controllers
{
    public class ValidatePost : ServiceFunction
    {
        public override void OnInit(FunctionConfig config)
        {
            config.FunctionName = "ValidatePost";
        }

        public override void OnRequest()
        {
            Console.WriteLine("ValidatePost!");
        }
    }
}