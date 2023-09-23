using static WetterKarte.DL.DTO.Class;

namespace WetterKarte.DL.Services.Interfaces
{
    public interface IUserService
    {
        public ResultViewModel GetCity(string City);
        public bool GetToken(string token);

        /// <summary>
        /// Wenn möchten Sie von Databank Anwenden
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        // public User LoginUser(LoginViewModel login)

    }
}
