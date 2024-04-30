using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOPENTK
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HOLA..Inicializando OpenTK...");
            BasicWindow miVentana = new BasicWindow();
            miVentana.Run(30.0);// 30 FPS 
        }
    }
}
