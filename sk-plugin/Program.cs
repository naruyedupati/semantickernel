// See https://aka.ms/new-console-template for more information
using Microsoft.SemanticKernel;
using Plugins;


// For pre-requisites and Azure Open AI setup instructions, see https://learn.microsoft.com/en-us/azure/ai-services/openai/quickstart?tabs=command-line%2Cpython&pivots=programming-language-studio#prerequisites
string apiKey = "";
string deploymentName = "gpt-35-turbo";
string endpoint = "";

var kernelBuilder = Kernel.CreateBuilder()
    .AddAzureOpenAIChatCompletion(deploymentName, endpoint, apiKey);

var pluginsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Plugins/CoursePlan");
kernelBuilder.Plugins.AddFromPromptDirectory(pluginsDirectory, "CoursePlan");

kernelBuilder.Plugins.AddFromType<DocumentPlugin>();
var kernel = kernelBuilder.Build();


if (!kernel.Plugins.TryGetPlugin("CoursePlan", out var courseModulePlugin))
{
    Console.WriteLine("CoursePlan plugin not found.");
    return;
}

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


var docFunction = kernel.Plugins.GetFunction("DocumentPlugin", "WriteToDocument");
var filePath = await kernel.InvokeAsync(
docFunction,
    new() {
        { "content", studyPlan }
    }
);

Console.WriteLine(filePath);

Console.ReadLine();
Console.WriteLine("Press any key to exit...");

