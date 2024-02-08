using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins
{
    public class DocumentPlugin
    {
        [KernelFunction, Description("Write the content to a text document")]
        public static string WriteToDocument([Description("The content to write to a document of")]
                                              string content)
        {
            string fileName = "CoursePlan.txt";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            System.IO.File.WriteAllText(filePath, content);
            return filePath;
        }
    }
}
