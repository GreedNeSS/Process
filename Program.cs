using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProcessManipulator
{
    class Program
    {
        static void Main(string[] args)
        {
            ListAllRuningProcesses();

            Console.Write("Write Process ID for EnumThreadsForPid(): ");
            string pID1 = Console.ReadLine();
            int intPID1 = int.Parse(pID1);

            EnumThreadsForPid(intPID1);

            Console.Write("Write Process ID for EnumThreadsForPid(): ");
            string pID2 = Console.ReadLine();
            int intPID2 = int.Parse(pID2);

            EnumModelsForPid(intPID2);
            StartAndKillProcess();

            Console.ReadLine();
        }

        static void ListAllRuningProcesses()
        {
            var processes = from pc in Process.GetProcesses()
                            orderby pc.Id
                            select pc;

            Console.WriteLine("***** Processes List *****\n");

            foreach (Process process in processes)
            {
                Console.WriteLine($" => Id: {process.Id} Name: {process.ProcessName}");
            }

            Console.WriteLine("*****************************\n");
        }

        static void EnumThreadsForPid(int pID)
        {
            Process process;

            try
            {
                process = Process.GetProcessById(pID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine($"***** Process: {process.ProcessName} Threads:\n");

            ProcessThreadCollection threads =  process.Threads;

            foreach (ProcessThread thread in threads)
            {
                Console.WriteLine($" => ID: {thread.Id} StartTime: {thread.StartTime} PriorityLevel: {thread.PriorityLevel}");
            }

            Console.WriteLine("***********************************************\n");
        }

        static void EnumModelsForPid(int pID)
        {
            Process process;

            try
            {
                process = Process.GetProcessById(pID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine($"***** Process: {process.ProcessName} Models:\n");

            ProcessModuleCollection moduleCollection =  process.Modules;

            foreach (ProcessModule module in moduleCollection)
            {
                Console.WriteLine($" => Module Name: {module.ModuleName} file: {module.FileName}");
            }

            Console.WriteLine("***********************************************\n");
        }

        static void StartAndKillProcess()
        {
            Process gProcess;

            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo("chrome.exe", "www.facebook.com")
                {
                    WindowStyle = ProcessWindowStyle.Maximized
                };

                gProcess = Process.Start(processStartInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.Write("--> Hit enter to kill {0}...", gProcess.ProcessName);
            Console.ReadLine();

            try
            {
                gProcess.Kill();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}
