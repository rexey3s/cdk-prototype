namespace RosenCDK.DTO
{
    public class ResponseMessageDTO
    {
        public ResponseMessageDTO()
        {
            
        }

        public ResponseMessageDTO(bool status, string message)
        {
            Status = status;
            Message = message;
        }

        public bool Status { get; set; }

        public string Message { get; set; }

    }
}