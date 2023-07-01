namespace Application.ViewModels.UserViewModels
{
    public class UserLoginDTOResponse
    {
        public Guid UserId { get; internal set; }
        public string JWT { get; internal set; }
        public string RefreshToken { get; internal set; }
    }
}
