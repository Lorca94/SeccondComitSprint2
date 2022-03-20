using ForumBackEnd.Controllers.DTO;
using ForumBackEnd.Data;
using ForumBackEnd.Models;
using ForumBackEnd.Repositories;
using System.Security.Cryptography;

namespace ForumBackEnd.Services
{
    public class UserServices
    {
        // Se intancia el repo
        private IUserRepository userRepository;
        private IRoleRepository roleRepository;

        // Constructor
        public UserServices(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        // Tratado de datos

        // Devuelve todos los users
        public IEnumerable<User> FindAllUser()
        {
            return userRepository.GetUsers();
        }

        // Devuelve un usuario por su id
        public User FindUserById(int userId)
        {
            if (userId < 0)
            {
                return null;
            }
            return userRepository.GetUserById(userId);
        }

        // Añade un usuario a BBDD  si funciona devuelve true, sino false
        public int CreateUser(User user)
        {
            if (user != null)
            {
                // Si role no existe devuelve -1
                if (!roleRepository.RoleExists(user.RoleId))
                {
                    return -1;
                }
                // Si no se puede validar devuelve 0
                if (!ValidateUser(user))
                {
                    return 0;
                }
                // Hash password
                user.Password = EncriptPassword(user);
                user.Role = roleRepository.GetRoleById(user.RoleId);

                // Se inserta en BBDD
                userRepository.InsertUser(user);
                userRepository.Save();
            }
            return 1;
        }

        public bool ModifyUser(User user)
        {
            try
            {
                if (userRepository.UserExists(user.Id))
                {
                    userRepository.UpdateUser(user);
                    return true;
                }
                return false;
            } catch (Exception ex)
            {
                return false;
            }
        }

        // Modificacion y modelados de datos
        // Comprueba que el usuario sea válido (email no repetido / username no repetido) devuelve un bool.
        public bool ValidateUser(User user)
        {
            List<User> usersList = userRepository.GetUsers().ToList();
            foreach (User actualUser in usersList)
            {
                if (actualUser.Email.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }
                if (actualUser.Username.Equals(user.Username, StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }
            }
            return true;
        }

        // Recibe el usuario y encripta la password
        public string EncriptPassword(User user)
        {
            // Variable para hash
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // Tipo de codificación y obtiene el valore del hash
            var pbkdf2 = new Rfc2898DeriveBytes(user.Password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Se combina hash con salt
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Se guarda la pw hasheada en una variable
            return Convert.ToBase64String(hashBytes);
        }

        public bool ExistsUser(int userId)
        {
            return userRepository.UserExists(userId);
        }

        // Login para el user devuelve
        public bool UserLogin(LoginDTO loginDTO)
        {
            List<User> userList = userRepository.GetUsers().ToList();
             foreach(User actualUser in userList)
            {
                // email introducido está guardado en bbdd??
                if(actualUser.Email.Equals(loginDTO.Email, StringComparison.InvariantCultureIgnoreCase))
                {
                    // Se guarda pw de bbdd
                    string dbPassword = actualUser.Password;
                    
                    // Se extra bytes
                    byte[] hashByte = Convert.FromBase64String(dbPassword);
                    
                    // Se obtiene salt
                    byte[] salt = new byte[16];
                    Array.Copy(hashByte, 0, salt, 0, 16);

                    // Se calcula el hash para la contraseña introducida
                    var pbkdf2 = new Rfc2898DeriveBytes(loginDTO.Password, salt, 100000);
                    byte[] hash = pbkdf2.GetBytes(20);

                    // Se comparan las contraseñas
                    for(int i = 0; i < 16; i++)
                    {
                        if(hashByte[i + 16] != hash[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }

            }
            return false;
        }
    }
}
