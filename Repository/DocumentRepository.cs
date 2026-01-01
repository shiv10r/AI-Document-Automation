using AI_Document_Automation_Backend.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AI_Document_Automation_Backend.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IConfiguration _configuration;

        public DocumentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SaveExtractedData(ExtractedData data)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using var conn = new SqlConnection(connectionString);
            conn.Open();

            var query = @"INSERT INTO ExtractedDocuments
                          (InvoiceNumber, Date, Amount, VendorName, CustomerName)
                          VALUES (@InvoiceNumber, @Date, @Amount, @VendorName, @CustomerName)";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@InvoiceNumber", data.InvoiceNumber ?? string.Empty);
            cmd.Parameters.AddWithValue("@Date", data.Date ?? string.Empty);
            cmd.Parameters.AddWithValue("@Amount", data.Amount ?? string.Empty);
            cmd.Parameters.AddWithValue("@VendorName", data.VendorName ?? string.Empty);
            cmd.Parameters.AddWithValue("@CustomerName", data.CustomerName ?? string.Empty);

            cmd.ExecuteNonQuery();
        }
    }
}
