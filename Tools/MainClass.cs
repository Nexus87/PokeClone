using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Eto.Forms;
namespace Tools
{
    public class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new Application().Run(new MainForm());
        }
    }
}
