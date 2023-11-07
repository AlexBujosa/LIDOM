using BCrypt.Net;

namespace LIDOM.utils
{
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            // Genera un hash seguro para la contraseña
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Verifica si la contraseña coincide con el hash almacenado
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
