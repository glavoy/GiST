using System;
using System.IO;
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

        // data file
        string dataFile = string.Concat(@"..\..\data\gist.txt");




        // New form loading
        private void MainMenu_Load(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(dataFile);

            List<string> uniqueIDs = new List<string>();

            foreach (string ln in lines)
            {
                //string[] ids = ln.Split(',');

                string[] ids = ln.Split(new string[] { ";;;;" }, StringSplitOptions.None);


                if (ids.Length > 1)
                {
                    uniqueIDs.Add(ids[PublicVars.subjidPos]);
                }
            }


            foreach (string id in uniqueIDs)
            {
                string subjid = id.Replace("\"", "");
                if (subjid != "subjid")
                    uniqueIDSComboBox.Items.Add(subjid);
            }


        }


        // Start a new survey
        private void newSurveyButton_Click(object sender, EventArgs e)
        {
            PublicVars.subjid = subjIDTextBox.Text;
            PublicVars.survey = "gist";
            PublicVars.modifyingSurvey = false;

            Form survey = new Survey();
            survey.ShowDialog();
            survey.Dispose();
        }


        // Modify existing survey
        private void modifySurveyButton_Click(object sender, EventArgs e)
        {
            PublicVars.subjid = uniqueIDSComboBox.Text;
            PublicVars.survey = "gist";
            PublicVars.modifyingSurvey = true;

            Form survey = new Survey();
            survey.ShowDialog();
            survey.Dispose();
        }



        // End the program
        private void exitButton_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }


    }
}
