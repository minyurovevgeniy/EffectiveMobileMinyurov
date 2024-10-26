using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery
{
    public class CustomLogger
    {
        private string file;
        public CustomLogger(string filename) 
        {
            file = filename;
        }

        public void writeLog(string logMessage)
        {
            using(StreamWriter writer = new StreamWriter("../../../" + file + ".txt", true))
            {
                writer.Write("\r\nLog Entry : ");
                writer.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                writer.WriteLine(":");
                writer.WriteLine(logMessage);
                writer.WriteLine("-------------------------------");
            }
            
        }
    }
}
