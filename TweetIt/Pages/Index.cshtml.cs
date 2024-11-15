using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.Json.Serialization;
using TweetIt.Controllers;

public class IndexModel : PageModel
{
    [BindProperty]
    public RM RequestModel { get; set; }

    public string? ServerResponse { get; private set; }

    public void OnGet()
    {
        ServerResponse = null;
        RequestModel = new RM(); // Initialize RequestModel
    }

    public void OnPost()
    {
        // Ensure Action and Payload are provided
        if (string.IsNullOrWhiteSpace(RequestModel?.Action) || string.IsNullOrWhiteSpace(RequestModel?.Payload))
        {
            ServerResponse = "Error: Action or Payload cannot be empty.";
            return;
        }

        // Construct the JSON based on input values
        var jsonInput = new { action = RequestModel.Action, payload = RequestModel.Payload };
        var jsonString = JsonSerializer.Serialize(jsonInput);

        try
        {
            // Log the raw JSON for debugging
            Console.WriteLine($"Generated JSON: {jsonString}");

            // Deserialize the JSON back into the model for validation
            var parsedJson = JsonSerializer.Deserialize<RM>(jsonString);

            // Provide feedback
            if (parsedJson != null)
            {
                ServerResponse = $"JSON received successfully! Action: {parsedJson.Action}, Payload: {parsedJson.Payload}";


                System.Console.WriteLine(parsedJson.Action);

                string[] actions = parsedJson.Action.Split('.');
                string[] payload;

                switch(actions[0]){
                    case "account":
                        System.Console.WriteLine("Running Account Service Handler!");
                        payload = parsedJson.Payload.Split(',');
                        new AccountController().CreateAccount(payload[0], payload[1]);
                    break;

                    case "post":
                        System.Console.WriteLine("Running post Service Handler!");
                        payload = parsedJson.Payload.Split(',');
                        new PostController().Createpost(payload[0], payload[1], payload[2]);
                    break;



                    default:
                     System.Console.WriteLine("Unknown action id");
                     break;
                }


            }
            else
            {
                ServerResponse = "Error: Failed to deserialize JSON.";
            }
        }
        catch (JsonException ex)
        {
            ServerResponse = $"Error: Invalid JSON. {ex.Message}";
        }
    }

    public class RM
{
    [JsonPropertyName("action")]
    public string Action { get; set; }

    [JsonPropertyName("payload")]
    public string Payload { get; set; }
}
}
