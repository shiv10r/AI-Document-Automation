using Microsoft.AspNetCore.Mvc;
using AI_Document_Automation_Backend.Business;
using AI_Document_Automation_Backend.Models;

namespace AI_Document_Automation_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("upload")]
        public IActionResult Upload([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var tempPath = Path.Combine(Path.GetTempPath(), file.FileName);
            using (var stream = new FileStream(tempPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            ExtractedData data;
            try
            {
                data = _documentService.ProcessAndSaveDocument(tempPath);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing document: {ex.Message}");
            }
            finally
            {
                if (System.IO.File.Exists(tempPath))
                    System.IO.File.Delete(tempPath);
            }

            return Ok(data);
        }
    }
}
