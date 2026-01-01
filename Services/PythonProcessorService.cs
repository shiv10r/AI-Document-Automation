using System.Diagnostics;
using Newtonsoft.Json;
using AI_Document_Automation_Backend.Models;

namespace AI_Document_Automation_Backend.Services
{
    public class PythonProcessorService
    {
        private readonly string pythonPath = "python";
        private readonly string scriptPath = @"python-ai\extract_data.py";

        public ExtractedData ProcessDocument(string filePath)
        {
            var psi = new ProcessStartInfo
            {
                FileName = pythonPath,
                Arguments = $"{scriptPath} \"{filePath}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            using var reader = process.StandardOutput;
            string result = reader.ReadToEnd();
            process.WaitForExit();

            var data = JsonConvert.DeserializeObject<ExtractedData>(result);
            return data;
        }
    }
}
