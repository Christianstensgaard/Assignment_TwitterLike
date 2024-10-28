using PostService.Controllers;
using Service;

ToolBox.RunTime.ServiceClientName = "PostService";
ToolBox.AddService(new ValidatePost());
ToolBox.AddService(new CreatePost());
ToolBox.AddService(new UpdatePost());
ToolBox.AddService(new DeletePost());
ToolBox.RunTime.Start("message_server", 20200);