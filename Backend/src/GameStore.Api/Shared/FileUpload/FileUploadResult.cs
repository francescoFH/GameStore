namespace GameStore.Api.Shared.FileUpload;

public class FIleUploadResult
{
    public bool IsSuccess { get; set; }

    public string? FIleUrl { get; set; }

    public string? ErrorMessage { get; set; }
}