using api.models;

namespace api.interfaces {
    public interface ITokenService {
        string CreateToken(AppUser user);
    }
}