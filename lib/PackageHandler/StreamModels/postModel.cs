namespace PackageHandler.StreamModels;
public class PostModel{
  public int Id { get; set; }
  public string Title { get; set; }
  LinkedList<CommentModel> comments {get;set;}
}
