using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FAXCOMLib;
using System.IO;

namespace FaxHelper
{
    class Program
    {
        /// <summary> 
        /// The main entry point for the application. 
        /// </summary> 
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            string server = "";
            string document = "";
            string receiver = "";
            string receiverName = "";
            string sender = "";

            for (var i = 0; i < args.Length-1; i+=2)
            {
                switch(args[i].ToLower())
                {
                    case "-server":
                    case "-s":
                        server = args[i + 1];
                        break;

                    case "-document":
                    case "-d":
                        document = args[i + 1];
                        break;

                    case "-receiver":
                    case "-r":
                        receiver = args[i + 1];
                        break;

                    case "-receivername":
                    case "-rn":
                        receiverName = args[i + 1];
                        break;

                    case "-senderdisplay:":
                    case "-sd":
                        sender = args[i + 1];
                        break;
                }
            }

            if (CheckDocument(document) == false)
            {
                Console.WriteLine("Your document has a unsupported extention. Allowed extentions: .txt, .tif");
                return;
            }

            FaxDocument(server, document, receiver, receiverName, sender);
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Arguments:");
            Console.WriteLine("-s | -server     The IP or Hostname of the remote faxserver (or local machine)");
            Console.WriteLine("-d | -document   The document, that should be transmitted. (.txt or .tif)");
            Console.WriteLine("-r | -receiver   The number to dial of the fax of the receiver");
            Console.WriteLine("-rn | -receivername   The Displayname of the Receiver");
            Console.WriteLine("-sd | -senderdisplay  The Displayname of the Sender");
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static bool CheckDocument(string document)
        {
            string extention = Path.GetExtension(document);
            switch(extention.ToLower())
            {
                case ".txt":
                case ".tif":
                    return true;

                default:
                    return false;
            }
        }

        public static void FaxDocument(string server, string document, string receiver, string receiverName, string sender)
        {
            FaxServer faxServer = new FaxServer();
            FaxDoc doc = null;
            int response = -11;

            try
            {
                faxServer.Connect(server);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to connect to server: " + e.Message);
            }

            try
            {
                doc = (FaxDoc)faxServer.CreateDocument(document);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to create document: " + e.Message);
            }

            try
            {
                doc.FaxNumber = receiver;
                doc.RecipientName = receiverName;
                doc.DisplayName = sender;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to assign Fax Properties: " + e.Message);
            }


            try
            {
                response = doc.Send();
                Console.WriteLine(response + " Send successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(response + e.Message);
            }

            try
            {
                faxServer.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error during disconnect from server: " + e.Message);
            }
            
            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
