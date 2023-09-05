#pragma warning disable

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.AI.ImageGeneration;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.Skills.Core;
using Microsoft.SemanticKernel.Skills.Web;
using Microsoft.SemanticKernel.Skills.Web.Bing;
using Microsoft.SemanticKernel.Skills.Web.Google;

async Task Skill() {
    // Native Function
    // var mySkill = new MySkill();
    // var result = mySkill.Uppercase("hello world");
    // Console.WriteLine(result);

    var kernel = new KernelBuilder().Build();

    // Native Skill
    var mySkill = kernel.ImportSkill(new MySkill());
    // var result = await mySkill["Uppercase"].InvokeAsync("hello world");
    // Console.WriteLine(result);

    // Variable
    // var variables = new ContextVariables();
    // variables.Set("firstName", "Surasuk");
    // variables.Set("lastName", "Oakkharaamonphong");
    // var result = await mySkill["FullName"].InvokeAsync(variables);
    // Console.WriteLine(result);

    // Pipeline
    var textSkill = kernel.ImportSkill(new TextSkill());

    var input = "  hello world  ";    
    var result = await kernel.RunAsync(input, 
        textSkill["TrimStart"], 
        textSkill["TrimEnd"],
        mySkill["Uppercase"]);
    Console.WriteLine(result);
}
// await Skill();

async Task OpenAiCompletion() {
    var key = "";
    var model = "gpt-3.5-turbo";

    var kernel = new KernelBuilder().WithOpenAIChatCompletionService(model, key).Build();
    var skFunction = kernel.CreateSemanticFunction("write a tagline for coffee shop");
    var result = await skFunction.InvokeAsync();
    Console.WriteLine(result);
}
// await OpenAiCompletion();

async Task AzureOpenAiCompletion() {
    var key = "";
    var endpoint = "";
    var model = "";

    var kernel = new KernelBuilder().WithAzureChatCompletionService(model, endpoint, key).Build();

    // Semantic Function
    // var metaPrompt = "write a tagline for {{$input}}";
    // var taglineSkill = kernel.CreateSemanticFunction(metaPrompt);
    // var input = "coffee shop";
    // var result = await taglineSkill.InvokeAsync(input);
    // Console.WriteLine(result);

    var textSkill = kernel.ImportSkill(new TextSkill());
    var mySkill = kernel.ImportSkill(new MySkill());

    // Semantic Skill
    var skills = kernel.ImportSemanticSkillFromDirectory(".", "Skills");
    // var input = "coffee shop";
    // var variables = new ContextVariables(input);
    // variables.Set("language", "Japanese");
    // var result = await kernel.RunAsync(variables, 
    //     skills["Tagline"],
    //     textSkill["TrimStart"],
    //     skills["Translate"]);
    // Console.WriteLine(result);

    var input = "Thailand";
    var result = await kernel.RunAsync(input, skills["PrimeMinister"]);
    Console.WriteLine(result);
}
// await AzureOpenAiCompletion();

async Task BingSearch() {
    var bingKey = "";

    var bingConnector = new BingConnector(bingKey);
    var bingEngine = new WebSearchEngineSkill(bingConnector);

    var kernel = new KernelBuilder().Build();
    var bingSkill = kernel.ImportSkill(bingEngine);

    var searchText = "Who is prime minister of Thailand now?";
    var result = await kernel.RunAsync(searchText, bingSkill["Search"]);
    Console.WriteLine(result);
}
// await BingSearch();

async Task GoogleSearch() {
    var googleKey = "";
    var searchEngineId = "";

    var googleConnector = new GoogleConnector(googleKey, searchEngineId: searchEngineId);
    var googleEngine = new WebSearchEngineSkill(googleConnector);

    var kernel = new KernelBuilder().Build();
    var googleSkill = kernel.ImportSkill(googleEngine);

    var searchText = "Who is prime minister of Thailand now?";
    var result = await kernel.RunAsync(searchText, googleSkill["Search"]);
    Console.WriteLine(result);
}
// await GoogleSearch();

async Task SemanticKernelWithSearch() {
    var bingKey = "";
    var googleKey = "";
    var searchEngineId = "";

    var key = "";
    var endpoint = "";
    var model = "";

    var kernel = new KernelBuilder().WithAzureChatCompletionService(model, endpoint, key).Build();
    var skills = kernel.ImportSemanticSkillFromDirectory(".", "Skills");
    var textSkill = kernel.ImportSkill(new TextSkill());
    var mySkill = kernel.ImportSkill(new MySkill());

    var bingConnector = new BingConnector(bingKey);
    var bingEngine = new WebSearchEngineSkill(bingConnector);
    var bingSkill = kernel.ImportSkill(bingEngine);

    var googleConnector = new GoogleConnector(googleKey, searchEngineId: searchEngineId);
    var googleEngine = new WebSearchEngineSkill(googleConnector);
    var googleSkill = kernel.ImportSkill(googleEngine);

    var input = "Who is prime minister of Thailand now?";
    var variables = new ContextVariables(input);
    variables.Set("language", "Thai");

    var result = await kernel.RunAsync(variables,
        googleSkill["Search"],
        skills["Summarize"],
        skills["Translate"]);

    Console.WriteLine(result);
}
// await SemanticKernelWithSearch();

async Task SemanticKernelWithSql() {
    var key = "";
    var endpoint = "";
    var model = "";

    var kernel = new KernelBuilder().WithAzureChatCompletionService(model, endpoint, key).Build();
    var skills = kernel.ImportSemanticSkillFromDirectory(".", "Skills");
    var northwindSkill = kernel.ImportSkill(new NorthwindSkill());

    var variables = new ContextVariables();
    variables.Set("category", "Condiments");
    var result = await kernel.RunAsync(variables,
        northwindSkill["GetProducts"],
        skills["ProductInCategory"]);
    Console.WriteLine(result);
}
// await SemanticKernelWithSql();

async Task ChatGPT() {
    var key = "";
    var endpoint = "";
    var model = "";

    var chatGPT = new AzureChatCompletion(model, endpoint, key);
    var systemMessage = "You are an AI assistant that helps people find information.";
    Console.WriteLine($"System: {systemMessage}");

    var chat = chatGPT.CreateNewChat(systemMessage);
    while(true) {
        Console.Write("User: ");
        var userMessage = Console.ReadLine();
        chat.AddUserMessage(userMessage);

        var reply = await chatGPT.GenerateMessageAsync(chat);
        chat.AddAssistantMessage(reply);

        var message = chat.Messages.Last();
        Console.WriteLine($"{message.Role}: {message.Content}");
    }
}
// await ChatGPT();

async Task DallE() {
    var key = "";
    var endpoint = "";

    var kernel = new KernelBuilder().WithAzureOpenAIImageGenerationService(endpoint, key).Build();
    var dallE = kernel.GetService<IImageGeneration>();

    var prompt = "green field and sheep with cartoon";
    var imageUrl = await dallE.GenerateImageAsync(prompt, 256, 256);

    Console.WriteLine(imageUrl);
}
// await DallE();