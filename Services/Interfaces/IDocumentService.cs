using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SportSync.Models;
using SportSync.Models.Common;

namespace SportSync.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<OperationResult> UploadDocumentAsync(string userId, IFormFile file, string documentType, string description);
        Task<OperationResult> ValidateDocumentAsync(int documentId, string validatedBy);
        Task<OperationResult> RejectDocumentAsync(int documentId, string rejectionReason);
        Task<OperationResult> DeleteDocumentAsync(int documentId);
        Task<Document> GetDocumentByIdAsync(int documentId);
        Task<IEnumerable<Document>> GetUserDocumentsAsync(string userId);
        Task<IEnumerable<Document>> GetPendingDocumentsAsync();
        Task<IEnumerable<Document>> GetDocumentsByTypeAsync(string documentType);
    }
}
