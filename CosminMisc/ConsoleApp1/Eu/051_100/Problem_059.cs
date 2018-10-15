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
        static readonly byte[] DECRYPTED_BYTES = new byte[50];

        public static int Solve() {
            byte[] values = File.ReadAllText(CYPHER_FILE).Split(',').Select(v => byte.Parse(v)).ToArray();
            DICTIONARY = File.ReadAllText(DICTIONARY_FILE).Split(',').Select(v => v.Trim('"').ToLower()).ToHashSet();
            byte[] password = new byte[3];

            for (byte i1 = (byte)'a'; i1 <= (byte)'z'; i1++) {
                password[0] = i1;

                for (byte i2 = (byte)'a'; i2 <= (byte)'z'; i2++) {
                    password[1] = i2;

                    for (byte i3 = (byte)'a'; i3 <= (byte)'z'; i3++) {
                        password[2] = i3;

                        bool foundWords = Decrypt(values, password);
                    }
                }
            }

            return 0;
        }


        private static bool Decrypt(byte[] encrypted, byte[] password) {
            for (int i = 0; i < DECRYPTED_BYTES.Length; i++) {
                DECRYPTED_BYTES[i] = (byte)(encrypted[i] ^ password[i % PWD_LENGTH]);
            }

            string decryptedText = new string(DECRYPTED_BYTES.Select(b => (char)b).ToArray()).ToLower();
            for (int i = 3; i < 30; i++) {
                string word = decryptedText.Substring(0, i);
                string word2 = decryptedText.Substring(1, i);
                string word3 = decryptedText.Substring(2, i);

                if (DICTIONARY.Contains(word)) {
                    Console.WriteLine(word);
                    //for (int j = 1; j < 20; j++) {
                    //    if (DICTIONARY.Contains(decryptedText.Substring(0, i)))
                    //}
                }

                if (DICTIONARY.Contains(word2)) {
                    Console.WriteLine($"{decryptedText[0]}{word2}");
                }

                if (DICTIONARY.Contains(word3)) {
                    Console.WriteLine($"{decryptedText[0]}{decryptedText[1]}{word3}");
                }
            }

            return false;
        }
    }
}
