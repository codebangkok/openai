using Azure;
using Azure.AI.OpenAI;

var enpoint = new Uri("");
var key = new AzureKeyCredential("");
var client = new OpenAIClient(enpoint, key);

// var response = await client.GetCompletionsAsync("davinci", "write a tagline for a Coffee Shop");
// var completion = response.Value.Choices[0].Text;
// Console.WriteLine($"Completion: {completion}");

var options = new ChatCompletionsOptions();
options.Messages.Add(new ChatMessage(ChatRole.System, "You are an AI assistant that helps people find information."));
options.Messages.Add(new ChatMessage(ChatRole.User, "รางวัลออสการ์ปี 1997 Best Picture คือเรื่องอะไร"));
options.Messages.Add(new ChatMessage(ChatRole.Assistant, "รางวัลออสการ์ปี 1997 Best Picture ได้รับโดยเรื่อง \"Titanic\" ที่กำกับโดย James Cameron และนำแสดงโดย Leonardo DiCaprio และ Kate Winslet ค่ะ"));
options.Messages.Add(new ChatMessage(ChatRole.User, "นักแสดงนำชายคือใคร"));

var response = await client.GetChatCompletionsAsync("chatgpt", options);
Console.WriteLine($"Response: {response.Value.Choices[0].Message.Content}");