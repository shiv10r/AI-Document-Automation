using AI_Document_Automation_Backend.Models;

namespace AI_Document_Automation_Backend.Business
{
    public interface IDocumentService
    {
        ExtractedData ProcessAndSaveDocument(string filePath);
    }
}
