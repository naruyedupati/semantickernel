// See https://aka.ms/new-console-template for more information
using Microsoft.SemanticKernel;

#region Using Azure OpenAI Chat Completion Service
// For pre-requisites and Azure Open AI setup instructions, see https://learn.microsoft.com/en-us/azure/ai-services/openai/quickstart?tabs=command-line%2Cpython&pivots=programming-language-studio#prerequisites
string apiKey = "";
string deploymentName = "gpt-35-turbo";
string endpoint = "";

var kernelBuilder = Kernel.CreateBuilder().
    AddAzureOpenAIChatCompletion(deploymentName, endpoint, apiKey);
var kernel = kernelBuilder.Build();
#endregion Using Azure OpenAI Chat Completion Service

#region Using OpenAI Chat Completion Service
//string apiKey = "openai-api-key";
//string model = "gpt3.5-turbo";

//var kernelBuilder = new KernelBuilder().
//    WithOpenAIChatCompletionService(model, apiKey);
#endregion Using OpenAI Chat Completion Service




string mathPrompt = "calculate mean and standard deviation from a  list of numbers.Numbers: ```{{$input}}``` .Do not format or add commentary to your response. No prose.The output should  only return the response as JSON object with keys mean and standardDeviation with no explanantion";
//string mathPrompt = "Find all prime numbers from the provided input list of numbers.Numbers: ```{{$input}}``` .Do not format or add commentary to your response. No prose. ";
var mathFn = kernel.CreateFunctionFromPrompt(mathPrompt);
KernelArguments variables = new KernelArguments
{
    { "input", "1,2,3,4,5,15,19,22,18." }
};
var output = await kernel.InvokeAsync(mathFn, variables);
Console.WriteLine($"Output: {output.GetValue<string>()}");
Console.ReadLine();
Console.WriteLine("Press any key to exit...");
