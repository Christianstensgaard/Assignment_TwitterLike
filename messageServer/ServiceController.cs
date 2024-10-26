using System.Net.Sockets;
using System.Text.RegularExpressions;
using BitToolbox;

public class ServiceController{
  public ServiceController(){
    bufferController = new ByteBufferController();
    connections = new List<SlaveConnection>();
    new Thread(BackgroundWorker).Start();
  }
  public static object _lock = new();
  byte[] buffer = new byte[2000];
  public void NewConnection(TcpClient socket){
    lock(_lock){
      connections.Add(new SlaveConnection(){
        Socket = socket
      });
    }
  }
  public void ConnectionHandler(){
    lock(_lock){
      //- Check for Data awaible
    }
  }

  void HandleStream(ByteArray? stream, SlaveConnection connection){
    if(stream == null)
      return;
    System.Console.WriteLine(stream[0]);
    switch(stream[0]){
      case 0xee:
        if(stream[1] != 0xff)
          break;
        connection.Header = stream.ToArray();
        TraceAndLogHandler(null, connection.Header, 2 );
        System.Console.WriteLine("New Subscribtion Created");
      return;
    }

    if(connection.Header == null)
      return;

    string[] buf = HeaderManager.ConverToString(stream.ToArray());
    System.Console.WriteLine();
    System.Console.WriteLine(buf[0]);
    System.Console.WriteLine(buf[1]);
    System.Console.WriteLine();
    InvokeSlave(stream);
  }
  void BackgroundWorker(){
    while(true){
      SlaveConnection? pSlace = null;
      lock(_lock){
        foreach (var item in connections)
        {
          if(item.Socket.Available > 0){
            System.Console.WriteLine("Reading Data from Client");
            int size = item.Socket.GetStream().Read(buffer);

            //- Unpack the stream.
            int currentPosition = 0;
            while(true){
              int delta = size - currentPosition - sizeof(Int32);
              if(delta <= 0)
                break;

              int packageSize = BitConverter.ToInt32(buffer, currentPosition);
              //GET   |x| - - - - - - - - [packageSize]
              //Copy {|x| - - - - - - - - [packageSize]}
              //Continue while there is data to be read.
              currentPosition += sizeof(Int32);
              HandleStream(bufferController.Copy(buffer, currentPosition, packageSize), item);
              currentPosition += packageSize;
              //NOTE This function can be changed, to use the bufferController class only. 
              //- made a function to handle the networkStream directly instead of creating a new byte[]
            }
          }
        }

        if(pSlace != null)
          connections.Remove(pSlace);
      }
      Thread.Sleep(250);
    }
  }
  void InvokeSlave(ByteArray stream){
    SlaveConnection? pSlave = null;
      byte[] streamBuffer = stream.ToArray();
      lock(_lock){
        foreach (var s in connections)
        {

          if(HeaderManager.EqualServiceName(streamBuffer, s.Header)){
            pSlave = s;
            System.Console.WriteLine("Service Found!");
            try
            {
              s.Socket.GetStream().Write(PackageManager.Pack(streamBuffer, new byte[2]));
              s.Socket.GetStream().Flush();

              TraceAndLogHandler(streamBuffer, s.Header, 0);
              return;
            }
            catch (System.Exception)
            {
              TraceAndLogHandler(streamBuffer, s.Header, 4);
              continue;
            }
          }
        }
        System.Console.WriteLine("No header match found");
        TraceAndLogHandler(streamBuffer, null, 0);
      }
  }

    private void TraceAndLogHandler(byte[]? streamBuffer, byte[]? header, int TraceTypeID)
    {
      System.Console.WriteLine("Handling Tracing and logging");
      //- Function is still missing.
    }

    public class SlaveConnection{
    public byte[] Header {get;set;}
    public TcpClient? Socket {get; set; }
  }

    readonly List<SlaveConnection> connections;
    readonly ByteBufferController bufferController;
}