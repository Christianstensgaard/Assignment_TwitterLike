using Service;

System.Console.WriteLine("Hello! this is created to just simulate calling the different functions");
Random r = new Random();

ToolBox.RunTime.ServiceClientName = "Simulator";
ToolBox.RunTime.Start("message_server", 20200);

while(true){
  ToolBox.NewRequest("AccountService", "CreateAccount", [0xff,0xff]);
  Sleep(r);
  ToolBox.NewRequest("AccountService", "DeleteAccount", [0xff,0xff]);
  Sleep(r);
  ToolBox.NewRequest("AccountService", "Login", [0xff,0xff]);
  Sleep(r);
  ToolBox.NewRequest("AccountService", "Logout", [0xff,0xff]);
  Sleep(r);
  Sleep(r);
  ToolBox.NewRequest("DatabaseService", "AccountDbManager", [0xff,0xff]);
  Sleep(r);
  ToolBox.NewRequest("DatabaseService", "PostDbManager", [0xff,0xff]);
  Sleep(r);
  Sleep(r);
  ToolBox.NewRequest("PostService", "ValidatePost", [0xff,0xff]);
  Sleep(r);
  ToolBox.NewRequest("PostService", "CreatePost", [0xff,0xff]);
  Sleep(r);
  ToolBox.NewRequest("PostService", "UpdatePost", [0xff,0xff]);
  Sleep(r);
  ToolBox.NewRequest("PostService", "DeletePost", [0xff,0xff]);
  Sleep(r);
  Thread.Sleep(5000);
}



void Sleep(Random r){
  Thread.Sleep(r.Next(500,3200));
}

