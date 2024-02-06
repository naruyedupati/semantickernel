// See https://aka.ms/new-console-template for more information
using Microsoft.SemanticKernel;

#region Using Azure OpenAI Chat Completion Service
// For pre-requisites and Azure Open AI setup instructions, see https://learn.microsoft.com/en-us/azure/ai-services/openai/quickstart?tabs=command-line%2Cpython&pivots=programming-language-studio#prerequisites
string apiKey = "";
string deploymentName = "gpt-35-turbo";
string endpoint = "";

var kernelBuilder = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion(deploymentName, endpoint, apiKey);
var kernel = kernelBuilder.Build();
#endregion Using Azure OpenAI Chat Completion Service

#region Using OpenAI Chat Completion Service
//string apiKey = "openai-api-key";
//string model = "gpt3.5-turbo";

//var kernelBuilder = new KernelBuilder().
//    WithOpenAIChatCompletionService(model, apiKey);
#endregion Using OpenAI Chat Completion Service
var pluginsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Plugins/CoursePlan");

var courseModulePlugin= kernel.ImportPluginFromPromptDirectory(pluginsDirectory, "CoursePlan");


KernelArguments variables = new KernelArguments
{
    { "topic", "OpenAI." }
};

var title = await kernel.InvokeAsync( courseModulePlugin["Title"],variables); 
Console.WriteLine(title);
variables.Add("title", title.GetValue<string>());

var chapters = await kernel.InvokeAsync(courseModulePlugin["Chapters"],variables);
Console.WriteLine(chapters);
variables.Add("chapters", chapters.GetValue<string>());

var studyPlan = await kernel.InvokeAsync(courseModulePlugin["Plan"], variables);
Console.WriteLine(studyPlan);

Console.ReadLine();

Console.WriteLine("Press any key to exit...");
