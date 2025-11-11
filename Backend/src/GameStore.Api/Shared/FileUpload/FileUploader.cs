using GameStore.Api.Shared.FileUpload;

namespace GameStore.Api.FileUpload;

public class FileUploader(
    IWebHostEnvironment environment,
    IHttpContextAccessor httpContextAccessor)
{
    public async Task<FIleUploadResult> UploadFileAsync(
        IFormFile file,
        string folder)
    {
        FIleUploadResult result = new();

        if (file == null || file.Length == 0)
        {
            result.IsSuccess = false;
            result.ErrorMessage = "No file uploaded";
            return result;
        }

        if (file.Length > 10 * 1024 * 1024)
        {
            result.IsSuccess = false;
        }

        string[] permittedExtensions = [".jpg", ".jpeg", ".png"];
        string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (
            string.IsNullOrEmpty(fileExtension)
            || !permittedExtensions.Contains(fileExtension))
        {
            result.IsSuccess = false;
            result.ErrorMessage = "Invalid file type.";
            return result;
        }

        string uploadFolder = Path.Combine(environment.WebRootPath, folder);
        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        string safeFileName = $"{Guid.NewGuid()}{fileExtension}";
        string fullPAth = Path.Combine(uploadFolder, safeFileName);

        using FileStream stream = new(fullPAth, FileMode.Create);
        await file.CopyToAsync(stream);

        HttpContext? httpContext = httpContextAccessor.HttpContext;
        string fileUrl = $"{httpContext?.Request.Scheme}://{httpContext?.Request.Host}/{folder}/{safeFileName}";

        result.IsSuccess = true;
        result.FIleUrl = fileUrl;
        return result;
    }
}