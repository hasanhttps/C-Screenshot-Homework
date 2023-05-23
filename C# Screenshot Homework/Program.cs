using System.Drawing.Imaging;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System;


namespace C__Screenshot {
    internal class Program {
        public static string directoryPath = $"C:\\Users\\{Environment.UserName}\\Desktop\\Images";
        public static string[] fileNames = Directory.GetFiles(directoryPath);

        public static int Menu(List<string> choose) {
            Console.Clear();
            bool entered = false;
            int index = 0;
            while (true) {
                int y = 14 - choose.Count;
                for (int i = 0; i < choose.Count; i++) {
                    Console.SetCursorPosition(55, y + i);
                    if (index == i) Console.BackgroundColor = ConsoleColor.DarkGray;
                    else Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine(choose[i]);
                }
                dynamic ascii = Console.ReadKey();
                if (ascii.Key == ConsoleKey.UpArrow) {
                    if (index > 0) index--;
                    else index = choose.Count - 1;
                }
                else if (ascii.Key == ConsoleKey.DownArrow) {
                    if (index < choose.Count - 1) index++;
                    else index = 0;
                }
                else if (ascii.Key == ConsoleKey.Enter) { 
                    entered = true;
                    break;
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            if (entered) return index;
            return -1;
        }

        public static void showFiles() {
            List<string> filenames = new();
            for(int i = 0; i < fileNames.Length; i++) {
                filenames.Add(fileNames[i]);
            }
            var index = 1;
            index = Menu(filenames);

            try {
                Process.Start($"{filenames[index]}");
            }
            catch (Exception ex) {
                Console.WriteLine("Error opening file: " + ex.Message);
            }
        }
        public static void CaptureMyScreen() {
            try {
                if (!Directory.Exists(directoryPath)) {
                    Directory.CreateDirectory(directoryPath);
                }
                char num;
                if (fileNames.Length > 0) {
                    num = Convert.ToChar(fileNames.Length + 49);
                } else {
                    num = '1';
                }
                Bitmap captureBitmap = new Bitmap(1920, 1080, PixelFormat.Format32bppArgb);
                Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                captureBitmap.Save(directoryPath + $"\\Capture({num}).jpg", ImageFormat.Jpeg);
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }
        static void Main() {

            while (true) {
                Console.Write("Press enter to take screenshot and p to print files in current directory : ");
                dynamic key = Console.ReadKey();

                if (key.Key == ConsoleKey.Escape) break;
                else if (key.Key == ConsoleKey.Enter) {
                    CaptureMyScreen();
                }
                else if (key.Key == ConsoleKey.P) showFiles();
            }
        }
    }
}