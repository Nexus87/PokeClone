using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Eto.Forms;
using Eto.Drawing;

namespace Tools
{
    class MainForm : Form
    {
        public MainForm()
        {
            this.Size = new Size(600, 400);
            this.Content = new TableLayout
            {
                Rows = {
                    new TableRow(new CharBuilderForm()),
                    new TableRow(new MoveBuilderForm()),
                }
            };
            this.Padding = new Padding(5, 5);
        }
    }
}
