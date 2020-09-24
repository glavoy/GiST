using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gist
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }



        private void AddRadioButtons(int count)
        {
            //Controls.Clear();
            for (int i = 0; i <= count; i++)
            {
                RadioButton rdo = new RadioButton();
                rdo.Name = "RadioButton" + i;
                rdo.Text = "Radio Button " + i;
                rdo.Location = new Point(5, 30 * i);
                this.Controls.Add(rdo);
            }

        }

        private void AddTextBox(string question)
        {
            Controls.Clear();
            TextBox textBox1 = new TextBox();

            textBox1.Text = "";
            textBox1.Location = new Point(48, 64);
            textBox1.Size = new Size(104, 16);
            Controls.Add(textBox1);

        }

    }
}
