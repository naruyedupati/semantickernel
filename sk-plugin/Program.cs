// See https://aka.ms/new-console-template for more information
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;

#region Using Azure OpenAI Chat Completion Service
// For pre-requisites and Azure Open AI setup instructions, see https://learn.microsoft.com/en-us/azure/ai-services/openai/quickstart?tabs=command-line%2Cpython&pivots=programming-language-studio#prerequisites
string apiKey = "";
string deploymentName = "";
string endpoint = "";

var kernelBuilder = new KernelBuilder().
    WithAzureOpenAIChatCompletionService(deploymentName, endpoint, apiKey);
var kernel = kernelBuilder.Build();
#endregion Using Azure OpenAI Chat Completion Service

#region Using OpenAI Chat Completion Service
//string apiKey = "openai-api-key";
//string model = "gpt3.5-turbo";

//var kernelBuilder = new KernelBuilder().
//    WithOpenAIChatCompletionService(model, apiKey);
#endregion Using OpenAI Chat Completion Service
var pluginsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");

var courseModulePlugin= kernel.ImportSemanticFunctionsFromDirectory(pluginsDirectory, "CoursePlan");


var variables = new ContextVariables();
variables.Set("topic", "OpenAI");

var title = await kernel.RunAsync(variables, courseModulePlugin["Title"]); 
Console.WriteLine(title);
variables.Set("title", title.GetValue<string>());

var chapters = await kernel.RunAsync(variables, courseModulePlugin["Chapters"]);
Console.WriteLine(chapters);
variables.Set("chapters", chapters.GetValue<string>());

var studyPlan = await kernel.RunAsync(variables, courseModulePlugin["Plan"]);
Console.WriteLine(studyPlan);

Console.ReadLine();

Console.WriteLine("Press any key to exit...");
