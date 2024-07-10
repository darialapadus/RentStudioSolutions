using System;
using System.Security.Cryptography;
using System.Text;

namespace RentStudio.Helpers
{
    public class PasswordGenerator
    {
        public static string GenerateGuidPasswordHash()
        {
            // Generate a new GUID
            Guid guid = Guid.NewGuid();
            string guidString = guid.ToString();

            // Convert the GUID to a byte array
            byte[] guidBytes = Encoding.UTF8.GetBytes(guidString);

            // Create a SHA256 hash of the GUID byte array
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(guidBytes);

                // Convert the hash bytes to a hex string
                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2"));
                }

                return hashString.ToString();
            }
        }
    }
}
