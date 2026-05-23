using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace GVStartupLauncher
{
    class Program
    {
        

        const int GWL_STYLE = -16;

        const int WS_SYSMENU = 0x80000;
        const int WS_MINIMIZEBOX = 0x20000;
        const int WS_MAXIMIZEBOX = 0x10000;


        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowLong(
            IntPtr hWnd,
            int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(
            IntPtr hWnd,
            int nIndex,
            int dwNewLong);

        [DllImport("user32.dll")]
        static extern bool MoveWindow(
            IntPtr hWnd,
            int X,
            int Y,
            int nWidth,
            int nHeight,
            bool bRepaint);

        static string ConfigPath =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Config.ini");

        static void Main(string[] args)
        {
            try
            {
                IntPtr handle =
                GetConsoleWindow();

                int style =
                    GetWindowLong(
                        handle,
                        GWL_STYLE);

                style &= ~WS_SYSMENU;
                style &= ~WS_MINIMIZEBOX;
                style &= ~WS_MAXIMIZEBOX;

                SetWindowLong(
                    handle,
                    GWL_STYLE,
                    style);

                MoveWindow(
                    handle,
                    400,
                    200,
                    800,
                    500,
                    true);

                if (!File.Exists(ConfigPath))
                    return;

                Dictionary<string, string> config =
                    ReadConfig(ConfigPath);

                int startupDelay =
                    Convert.ToInt32(
                    config["StartupDelay"]);

                int programDelay =
                    Convert.ToInt32(
                    config["ProgramDelay"]);

                Console.Title = "GaugeView System Startup";

                Console.BackgroundColor = ConsoleColor.Red;

                Console.ForegroundColor = ConsoleColor.White;

                Console.Clear();

                Console.WriteLine("#############################################################");
                Console.WriteLine("#                                                           #");
                Console.WriteLine("#    ██████  █████  ██    ██ ████████ ██  ██████  ███    ██ #");
                Console.WriteLine("#   ██      ██   ██ ██    ██    ██    ██ ██    ██ ████   ██ #");
                Console.WriteLine("#   ██      ███████ ██    ██    ██    ██ ██    ██ ██ ██  ██ #");
                Console.WriteLine("#   ██      ██   ██ ██    ██    ██    ██ ██    ██ ██  ██ ██ #");
                Console.WriteLine("#    ██████ ██   ██  ██████     ██    ██  ██████  ██   ████ #");
                Console.WriteLine("#                                                           #");
                Console.WriteLine("#                 DO NOT TOUCH SYSTEM                      #");
                Console.WriteLine("#                                                           #");
                Console.WriteLine("#                GaugeView Is Starting...                  #");
                Console.WriteLine("#                                                           #");
                Console.WriteLine("#############################################################");


                Thread.Sleep(startupDelay);

                for (int i = 1; i <= 100; i++)
                {
                    string key = "Path" + i;

                    if (!config.ContainsKey(key))
                        break;

                    string exePath =
                        config[key];

                    StartProgram(exePath);

                    Thread.Sleep(programDelay);
                }

                //Console.Beep(1000, 500);
                Console.Beep(1000, 500);
                Console.Beep(1200, 500);
                Console.Beep(1400, 500);

                // Start your applications here
                // StartProgram(...);

                // Close console automatically
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                //Log(ex.ToString());
                Console.WriteLine($"Sorry for Inconvneince something went wrong {ex}");
            }
        }

        static void StartProgram(string exePath)
        {
            try
            {
                if (!File.Exists(exePath))
                {
                    Console.WriteLine($"GaugeView Application not found : {Path.GetFileName(exePath)}");

                    return;
                }

                ProcessStartInfo psi =
                    new ProcessStartInfo();

                psi.FileName = exePath;

                psi.WorkingDirectory =
                    Path.GetDirectoryName(exePath);

                psi.UseShellExecute = true;

                Process.Start(psi);

                Console.WriteLine($"GaugeView Application Started : {Path.GetFileName(exePath)}");
                //Log("Started: "
                //    + exePath);
            }
            catch (Exception ex)
            {
                //Log(ex.ToString());
                Console.WriteLine($"Some error occur {ex}, Try run GaugeView Manually : {Path.GetFileName(exePath)}");
            }
        }

        static Dictionary<string, string>
            ReadConfig(string path)
        {
            Dictionary<string, string> data =
                new Dictionary<string, string>();

            string[] lines =
                File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string temp = line.Trim();

                if (temp.StartsWith("["))
                    continue;

                if (temp.StartsWith(";"))
                    continue;

                if (!temp.Contains("="))
                    continue;

                string[] parts =
                    temp.Split(new char[] { '=' }, 2);

                data[parts[0].Trim()] =
                    parts[1].Trim();
            }

            return data;
        }

        //static void Log(string msg)
        //{
        //    try
        //    {
        //        File.AppendAllText(
        //            @"C:\Launcher\LauncherLog.txt",

        //            DateTime.Now.ToString(
        //            "yyyy-MM-dd HH:mm:ss")

        //            + " : "

        //            + msg

        //            + Environment.NewLine);
        //    }
        //    catch
        //    {
        //    }
        //}
    }
}