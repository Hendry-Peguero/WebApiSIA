namespace WebApiSIA.Core.Application.Dtos.Common
{
    /// <summary>
    /// DTO estándar para respuestas de error
    /// </summary>
    public class ErrorResponseDto
    {
        public string Error { get; set; } = string.Empty;

        public ErrorResponseDto(string error)
        {
            Error = error;
        }
    }
}
