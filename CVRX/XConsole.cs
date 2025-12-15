using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using ABI_RC.Core.Networking;
using Newtonsoft.Json;

namespace CVRX
{
    internal class XConsole
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeConsole();
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        public static void Alloc()
        {
            TextWriter writer = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
            Console.SetOut(writer);
            Console.SetError(writer);
            Console.CursorVisible = false;
            Art();
        }

        public static void Log(string Module, string message, bool warn = false)
        {
            DateTime now = DateTime.Now;
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(now.ToString("HH:mm:ss"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("]");
            Console.Write(" [");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("CVRX");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("]");
            Console.Write(" [");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Module);
            Console.ForegroundColor = ConsoleColor.White;
            if (warn)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write($"] {message}\n");
            Console.ResetColor();
        }

        public static void LogDebug(object obj)
        {
            string log = obj.ToString().Replace("\a", "");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{DateTime.Now.ToShortTimeString()}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] [");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Debug");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"{log}\n");
        }

        private static int[] IgnoredCodes = new int[] { 5019, 20 };
        public static void LogGameEvent(Tags Type, byte[] Bytes, bool RaiseInsteadReceive)
        {
            if (IgnoredCodes.Contains((int)Type)) return;
            string IfBytes = "  |  ";
            IfBytes += string.Join(", ", Bytes);
            IfBytes += $" [L: {Bytes.Length}]";
            LogDebug(string.Concat(new object[]
            {
                Environment.NewLine,
                    $"======= {(RaiseInsteadReceive ? "RAISED" : "RECEIVED" )} GAME EVENT =======", Environment.NewLine,
                    $"TYPE: {Type} / {(int)Type} ", Environment.NewLine,
                    $"DATA SERIALIZED: {JsonConvert.SerializeObject(Bytes, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}{IfBytes}", Environment.NewLine,
                    "======= END =======",
            }));
        }

        public static void Art()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($@" ----------------------------------------------------------------------------
|     ,o888888o.  `8.`888b           ,8' 8 888888888o.   `8.`8888.      ,8'  |
|    8888     `88. `8.`888b         ,8'  8 8888    `88.   `8.`8888.    ,8'   |
| ,8 8888       `8. `8.`888b       ,8'   8 8888     `88    `8.`8888.  ,8'    |
| 88 8888            `8.`888b     ,8'    8 8888     ,88     `8.`8888.,8'     |
| 88 8888             `8.`888b   ,8'     8 8888.   ,88'      `8.`88888'      |
| 88 8888              `8.`888b ,8'      8 888888888P'       .88.`8888.      |
| 88 8888               `8.`888b8'       8 8888`8b          .8'`8.`8888.     |
| `8 8888       .8'      `8.`888'        8 8888 `8b.       .8'  `8.`8888.    |
|    8888     ,88'        `8.`8'         8 8888   `8b.    .8'    `8.`8888.   |
|     `8888888P'           `8.`          8 8888     `88. .8'      `8.`8888.  |
|                                                                  By IL2LX  |
 ----------------------------------------------------------------------------");
            Console.ResetColor();
        }
    }
}
