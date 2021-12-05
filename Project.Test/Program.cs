using Project.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Test
{
    class Program
    {

        public Program()
        {

        }
        static void Main(string[] args)
        {
            var password = Console.ReadLine();
            Crypto _crypto = new Crypto();
            var encrypted =  _crypto.Encrypt(password);
            Console.WriteLine(encrypted + " Encrypted");
            var decrypted = _crypto.Decrypt(encrypted);
            Console.WriteLine(decrypted+ " Decrypted");
            Console.ReadLine();
        }
    }
}
