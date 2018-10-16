using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_059
    {
        static readonly string CYPHER_FILE = @"C:\Temp\p059_cipher.txt";
        static readonly string DICTIONARY_FILE = @"C:\Temp\p042_words.txt";
        static HashSet<string> DICTIONARY;
        static readonly int PWD_LENGTH = 3;
        static readonly byte[] DECRYPTED_BYTES = new byte[70];

        public static int Solve() {
            byte[] values = File.ReadAllText(CYPHER_FILE).Split(',').Select(v => byte.Parse(v)).ToArray();
            DICTIONARY = File.ReadAllText(DICTIONARY_FILE).Split(',').Select(v => v.Trim('"').ToLower()).ToHashSet();
            
            int result = 0;

            foreach (byte[] password in GeneratePasswords()) {
                result = Decrypt(values, password);

                if (result > 0)
                    return result;
            }

            return result;
        }

        static IEnumerable<byte[]> GeneratePasswords() {
            byte[] password = new byte[3];
            for (byte i1 = (byte)'a'; i1 <= (byte)'z'; i1++) {
                password[0] = i1;

                for (byte i2 = (byte)'a'; i2 <= (byte)'z'; i2++) {
                    password[1] = i2;

                    for (byte i3 = (byte)'a'; i3 <= (byte)'z'; i3++) {
                        password[2] = i3;

                        yield return password;
                    }
                }
            }
        }

        private static int Decrypt(byte[] encrypted, byte[] password) {
            // Did some trial and error to get these:
            int START = 51;
            int MAX_LEN = 15;

            for (int i = 0; i < DECRYPTED_BYTES.Length; i++) {
                DECRYPTED_BYTES[i] = (byte)(encrypted[START + i] ^ password[i % PWD_LENGTH]);
            }

            string decryptedText = new string(DECRYPTED_BYTES.Select(b => (char)b).ToArray()).ToLower();

            // Look for (longer) words
            for (int start = 0; start <= decryptedText.Length - MAX_LEN; start++) {
                for (int length = 5; length < MAX_LEN; length++) {
                    string word = decryptedText.Substring(start, length);
                    
                    if (DICTIONARY.Contains(word)) {
                        Console.WriteLine($"Found word: {word}");
                        return DecryptAll(encrypted, password);
                    }
                }
            }

            return 0;
        }

        static int DecryptAll(byte[] encrypted, byte[] password) {
            int sum = 0;
            byte[] decrypted_bytes_full = new byte[encrypted.Length];
            for (int i = 0; i < encrypted.Length; i++) {
                decrypted_bytes_full[i] = (byte)(encrypted[i] ^ password[i % PWD_LENGTH]);
                sum += decrypted_bytes_full[i];
            }

            string decryptedTextFull = new string(decrypted_bytes_full.Select(b => (char)b).ToArray()).ToLower();
            Console.WriteLine(decryptedTextFull);
            Console.WriteLine($"Password: {(char)password[0]}{(char)password[1]}{(char)password[2]}");

            return sum;
        }
    }
}
