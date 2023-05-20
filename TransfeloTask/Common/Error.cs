namespace TransfeloTask.Common
{
    public class Error
    {
        public string Name { get; set; }

        public string Message { get; set; }

        public Error()
        {
        }

        public Error(string name, string message)
        {
            Name = name;
            Message = message;
        }
    }
}
