namespace Application.ViewModels.FilterModels
{
    public class UserFilteringModel:BaseFilterringModel
    {
        public string?[]? Email { get; set; }
        public string?[]? FullName { get; set; }  //ko check la tim tat ca
        public string?[]? PhoneNumber { get; set; }
    }
}