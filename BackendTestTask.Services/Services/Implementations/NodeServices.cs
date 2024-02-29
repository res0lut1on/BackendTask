using BackendTestTask.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Services.Implementations
{
    public class NodeServices : INodeServices
    {
        public void LogNode()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("LOG");
        }
    }
}
