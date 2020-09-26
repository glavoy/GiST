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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        PublicVars PubVar = new PublicVars();

        private void newSurveyButton_Click(object sender, EventArgs e)
        {
            PubVar.survey = "gist";
            PubVar.modifyingSurvey = false;
            //new Survey().Show();
   


            Form blah = new Survey();
            blah.ShowDialog();  // process return result if needed
            blah.Dispose();



        }





    }
}
