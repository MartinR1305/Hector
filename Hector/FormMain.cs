using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hector
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            // On définit que le treeView de la partie gauche ne pourra pas faire moins de 200 pixels lors de l'exécution.
            splitContainer1.Panel1MinSize = 200;
        }
    }
}
