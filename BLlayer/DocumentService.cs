using AI_Document_Automation_Backend.Models;
using AI_Document_Automation_Backend.Repository;
using AI_Document_Automation_Backend.Services;

namespace AI_Document_Automation_Backend.Business
{
    public class DocumentService : IDocumentService
    {
        private readonly PythonProcessorService _pythonService;
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(PythonProcessorService pythonService, IDocumentRepository documentRepository)
        {
            _pythonService = pythonService;
            _documentRepository = documentRepository;
        }

        public ExtractedData ProcessAndSaveDocument(string filePath)
        {
            // Call Python AI script
            var extractedData = _pythonService.ProcessDocument(filePath);

            // Save to database
            _documentRepository.SaveExtractedData(extractedData);

            return extractedData;
        }
    }
}
