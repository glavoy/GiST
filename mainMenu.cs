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

        PublicVars PublicVars = new PublicVars();

        private void newSurveyButton_Click(object sender, EventArgs e)
        {
            PublicVars.subjid = subjIDTextBox.Text;
            PublicVars.survey = "gist";
            PublicVars.modifyingSurvey = false;

            Form survey = new Survey();
            survey.ShowDialog();
            survey.Dispose();


            // for testing purposes - will delete
            subjIDLabel.Text = PublicVars.subjid;



        }



        // End the program
        private void exitButton_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
