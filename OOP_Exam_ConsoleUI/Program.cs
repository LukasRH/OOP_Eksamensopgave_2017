using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using OOP_ExamClassLibary;

namespace OOP_Exam_ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ILineSystem lineSystem = new LineSystem();
            ILineSystemUI ui = new LineSystemCLI("Console UI", lineSystem);
            SystemController sc = new SystemController(ui, lineSystem);

            ui.Start();
        }
    }
}
