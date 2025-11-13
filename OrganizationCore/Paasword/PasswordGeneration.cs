
namespace OrganizationCore.Paasword
{
    public class PasswordGeneration : IPasswordGenerationInterface
    {
        public string GeneratePasswordAsync(int length = 12)
        {
            var valueChar = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";

            var random = new Random();
            var password = new char[length];

            for (int  i = 0; i < length; i++)
            {
                password[i] = valueChar[random.Next(valueChar.Length)];
            }

            return new string(password);
        }
    }
}
