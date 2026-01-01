using AI_Document_Automation_Backend.Models;

namespace AI_Document_Automation_Backend.Repository
{
    public interface IDocumentRepository
    {
        void SaveExtractedData(ExtractedData data);
        // Optional: Add methods to get data for Power BI/dashboard
    }
}
