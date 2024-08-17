using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;


namespace BankingControlPanel_Models.ValidateModelAttributes
{
    public class ValidationResultModel
    {
        public bool IsSeccuss { get; set; }
        public object Value { get; set; }
        public string DisplayMessage { get; set; } = "";
        public List<ValidationError> ErrorMessages { get; set; }
        public int StatusCode { get; set; }

        public ValidationResultModel(ModelStateDictionary modelState)
        {
            IsSeccuss = false;
            StatusCode = 422;
            ErrorMessages = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
    }

    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }
        public string Message { get; }
        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }


}
