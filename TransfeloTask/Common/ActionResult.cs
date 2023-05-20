namespace TransfeloTask.Common
{
    public class ActionResult
    {
        public List<Error> Errors { get; set; }

        public bool HasError => Errors.Any();

        public ActionResult()
        {
            Errors = new List<Error>();
        }

        public ActionResult(List<Error> errors)
        {
            Errors = errors;
        }

        public ActionResult(string errorName, string errorMessage)
        {
            Errors = new List<Error>
            {
                new Error(errorName, errorMessage)
            };
        }
    }
}
