namespace TransfeloTask.Common
{
    public class ActionResult<T> : ActionResult
    {
        public T Result { get; set; }

        public ActionResult(T value)
        {
            Result = value;
            base.Errors = new List<Error>();
        }

        public ActionResult(List<Error> error)
        {
            base.Errors = error;
        }

        public ActionResult(string errorName, string errorMessage)
        {
            base.Errors = new List<Error>
            {
                new Error(errorName, errorMessage)
            };
        }
    }
}
