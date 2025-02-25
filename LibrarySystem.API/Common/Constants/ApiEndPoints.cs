namespace LibrarySystem.API.Common.Constants;
internal static class ApiEndPoints
{
    internal const string BaseController = "/api/v1/[controller]";
    internal class Auth
    {
        private const string Authentication = "/auth";
        internal const string RefreshToken = Authentication + "/refresh-token";
    }
    internal class Books
    {
        internal const string GetById = BaseController + "/{id}";
        internal const string Delete = BaseController + "/{id}";
        internal const string DeleteBookImages = BaseController + "/book-images";
        internal const string UpdateBookImages = BaseController + "/update-book-images";
    }
    internal class Person
    {
        internal const string ConfirmEmailAddress = BaseController + "/confirm-email/{id}";
        internal const string Register = BaseController + "/register";
        internal const string Login = BaseController + "/login";
        internal const string ChangePassword = BaseController + "/change-password";
        internal const string VerifyResetPasswordToken = BaseController + "/verify-password-reset-token";
        internal const string ResetPasswordRequest = BaseController + "/reset-password-request";
        internal const string Logout = BaseController + "/logout";
        internal const string RemoveProfilePicture = BaseController + "/remove-profile-picture";
        internal const string RefreshToken = BaseController + "/refresh-token";
    }
}