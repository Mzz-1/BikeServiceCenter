using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BikeServiceCenter.Data
{
    public static class Utils
    {
        private const char _segmentDelimiter = ':';

        // Method to enrypt the password
        public static string HashSecret(string input)
        {
            var saltSize = 16;
            var iterations = 100_000;
            var keySize = 32;
            HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(input, salt, iterations, algorithm, keySize);

            return string.Join(
                _segmentDelimiter,
                Convert.ToHexString(hash),
                Convert.ToHexString(salt),
                iterations,
                algorithm
            );
        }

        // Method to check the password provided by user
        public static bool VerifyHash(string input, string hashString)
        {
            string[] segments = hashString.Split(_segmentDelimiter);
            byte[] hash = Convert.FromHexString(segments[0]);
            byte[] salt = Convert.FromHexString(segments[1]);
            int iterations = int.Parse(segments[2]);
            HashAlgorithmName algorithm = new(segments[3]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                iterations,
                algorithm,
                hash.Length
            );

            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }

        // Method to get the path of the folder where the json file will be stored
        public static string GetAppDirectoryPath()
        {
            return "C:\\Users\\user\\Desktop\\New folder (4)\\20048956 Pratham Maharjan\\BikeServiceCenter\\BikeServiceCenter\\wwwroot\\json";
        }

        // Method to get the path of the json file where users data is stored
        public static string GetAppUsersFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "users.json");
        }

        // Method to get the path of the json file where items data is stored
        public static string GetItemsFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "items.json");
        }

        // Method to get the path of the json file where inventory data is stored
        public static string GetItemsRecordFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "itemsRecord.json");
        }
    }
}