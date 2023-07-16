namespace OneFrameCGPTPlayground.WebAPI.Model.ChatGpt
{
    public class UploadModel
    {
        public IFormFile SourceFile { get; set; }

        public IFormFile TargetFile { get; set; }
    }
}
