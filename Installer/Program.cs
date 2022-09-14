using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using IWshRuntimeLibrary;

namespace Installer
{
    internal class Program
    {
        static void CreateLink(string PATH)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            object shDesktop = (object)"Desktop";
            WshShell shell = new WshShell();
            string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\BloxFlip.lnk";
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "New shortcut for BloxFlip.";
            shortcut.Hotkey = "Ctrl+Shift+N";
            shortcut.IconLocation = AppDomain.CurrentDomain.BaseDirectory + "BloxClient\\img.ico";
            shortcut.TargetPath = PATH;
            shortcut.Save();
        }

        static void moveDirectory(string oldPath, string newPath)
        {
            if (!System.IO.Directory.Exists(newPath))
            {
                System.IO.Directory.CreateDirectory(newPath);
            }
            String[] files = Directory.GetFiles(oldPath);
            String[] directories = Directory.GetDirectories(oldPath);
            foreach (string str in files)
            {
                System.IO.File.Copy(str, Path.Combine(newPath, Path.GetFileName(str)), true);
            }
            foreach (string dir in directories)
            {
                moveDirectory(Path.Combine(oldPath, Path.GetFileName(dir)), Path.Combine(newPath, Path.GetFileName(dir)));
            }

        }

        static void Main(string[] args)
        {
            string Logo = @"
----------------------------------------------------------------------------
  /$$$$$$  /$$ /$$                       /$$     /$$$$$$$$ /$$ /$$          
 /$$__  $$| $$|__/                      | $$    | $$_____/| $$|__/          
| $$  \__/| $$ /$$  /$$$$$$  /$$$$$$$  /$$$$$$  | $$      | $$ /$$  /$$$$$$ 
| $$      | $$| $$ /$$__  $$| $$__  $$|_  $$_/  | $$$$$   | $$| $$ /$$__  $$
| $$      | $$| $$| $$$$$$$$| $$  \ $$  | $$    | $$__/   | $$| $$| $$  \ $$
| $$    $$| $$| $$| $$_____/| $$  | $$  | $$ /$$| $$      | $$| $$| $$  | $$
|  $$$$$$/| $$| $$|  $$$$$$$| $$  | $$  |  $$$$/| $$      | $$| $$| $$$$$$$/
 \______/ |__/|__/ \_______/|__/  |__/   \___/  |__/      |__/|__/| $$____/ 
                                                                  | $$      
                                                                  | $$      
                                                                  |__/      
                            CLIENT FLIP v1.0.0.0
----------------------------------------------------------------------------";
            


            Console.Title = "BloxFlip - Installer";
            Console.WriteLine(Logo);
            Console.WriteLine("BloxFlip : Moving Dir Files..");

            string ClientFlip = AppDomain.CurrentDomain.BaseDirectory + "BloxClient\\BloxFlip";
            string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string PF;
            string BloxFlipDir;

            if (8 == IntPtr.Size || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                PF = Environment.GetEnvironmentVariable("ProgramFiles(x86)");
                BloxFlipDir = Environment.GetEnvironmentVariable("ProgramFiles(x86)") + "\\BloxFlip\\BloxLoad.bat";
            } 
            else
            {
                PF = Environment.GetEnvironmentVariable("ProgramFiles");
                BloxFlipDir = Environment.GetEnvironmentVariable("ProgramFiles") + "\\BloxFlip\\BloxLoad.bat";
            }

            Console.WriteLine("BloxFlip : DIR 1 : " + ClientFlip);
            Console.WriteLine("BloxFlip : DIR 2 : " + PF);
            Console.WriteLine("BloxFlip : Path Dir : " + BloxFlipDir);

            try
            {
                moveDirectory(ClientFlip, PF + "\\BloxFlip");
            }
            catch (IOException exp)
            {
                Console.WriteLine("BloxFlip : " + exp.Message);
            }

            Console.WriteLine("Creating Shortcut..");
            CreateLink(BloxFlipDir);

            Console.WriteLine("Press any key to exit application..");
            Console.ReadKey();
        }
    }
}
