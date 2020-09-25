using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace gist
{
    public partial class newSurvey : Form
    {

        //private readonly System.Xml.XmlDocument xr = new System.Xml.XmlDocument();



    public newSurvey()
        {
            InitializeComponent();
        }



        private void newSurvey_Load(object sender, EventArgs e)
        {
            CreateQuestion();

            
        }

        private void nextButton_Click(object sender, EventArgs e)
        {

            AddTextBox("Question goes Here");
        }





        private void CreateQuestion()
        {

            
            AddRadioButtons(3);



            // Controls.Clear();
            //InitializeComponent();

            System.Xml.XmlDocument xr = new System.Xml.XmlDocument();

            //xr.LoadXml(My.Resources.ResourceManager.GetObject(Survey))
            //Dim myNode As XmlNode
            //myNode = xr.GetElementsByTagName("question").Item(question_num)



            // The following code works with "gist - works with example.xml"

            //XmlDocument itemDoc = new XmlDocument();
            //itemDoc.Load(@"..\..\xml\gist.xml");
            //Console.WriteLine("DocumentElement has {0} children.", itemDoc.DocumentElement.ChildNodes.Count);

            //// iterate through top-level elements
            //foreach (XmlNode itemNode in itemDoc.DocumentElement.ChildNodes)
            //{
            //    // because we know that the node is an element, we can do this:
            //    XmlElement itemElement = (XmlElement)itemNode;
            //    Console.WriteLine("\n[Item]: {0}\n{1}",
            //        itemElement.Attributes["name"].Value,
            //        itemElement.Attributes["description"].Value);
            //    if (itemNode.ChildNodes.Count == 0)
            //        Console.WriteLine("(No additional Information)\n");
            //    else
            //    {
            //        foreach (XmlNode childNode in itemNode.ChildNodes)
            //        {
            //            if (childNode.Name.ToUpper() == "ATTRIBUTE")
            //            {
            //                Console.WriteLine("{0} : {1}",
            //                    childNode.Attributes["name"].Value,
            //                    childNode.Attributes["value"].Value);
            //            }
            //            else if (childNode.Name.ToUpper() == "SPECIALS")
            //            {
            //                foreach (XmlNode specialNode in childNode.ChildNodes)
            //                {
            //                    Console.WriteLine("*{0}:{1}",
            //                       specialNode.Attributes["name"].Value,
            //                       specialNode.Attributes["description"].Value);
            //                }
            //            }
            //        }
            //    }
            //}
            //Console.ReadLine();







            XmlDocument itemDoc = new XmlDocument();
            itemDoc.Load(@"..\..\xml\gist.xml");
            Console.WriteLine("DocumentElement has {0} questions.", itemDoc.DocumentElement.ChildNodes.Count);

            // iterate through top-level elements
            foreach (XmlNode itemNode in itemDoc.DocumentElement.ChildNodes)
            {
                // because we know that the node is an element, we can do this:
                XmlElement itemElement = (XmlElement)itemNode;
                Console.WriteLine("\n[question]: {0}\n{1}",
                    itemElement.Attributes["type"].Value,
                    itemElement.Attributes["fieldname"].Value);
                //if (itemNode.ChildNodes.Count == 0)
                //    Console.WriteLine("(No additional Information)\n");
                //else
                //{
                //    foreach (XmlNode childNode in itemNode.ChildNodes)
                //    {
                //        if (childNode.Name == "text")
                //        {
                //            Console.WriteLine("{0} : {1}",
                //                childNode.Attributes["name"].Value,
                //                childNode.Attributes["value"].Value);
                //        }
                //        else if (childNode.Name.ToUpper() == "SPECIALS")
                //        {
                //            foreach (XmlNode specialNode in childNode.ChildNodes)
                //            {
                //                Console.WriteLine("*{0}:{1}",
                //                   specialNode.Attributes["name"].Value,
                //                   specialNode.Attributes["description"].Value);
                //            }
                //        }
                //    }
                //}
            }
            Console.ReadLine();











            //Select Case myNode.Attributes("type").Value




            //switch (myNode.Attributes("type").Value)
            //{
            //    case "radio":
            //        AddRadioButtons(myNode)
            //        break;
            //    case "text":
            //        AddTextBox(myNode)
            //        break;
            //    default:
            //        Console.WriteLine("Default case");
            //        break;
            //}



        }



        private void AddRadioButtons(int count)
        {
            responsePanel.Controls.Clear();



            for (int i = 0; i <= count; i++)
            {
                RadioButton rdo = new RadioButton();
                rdo.Name = "RadioButton" + i;
                rdo.Text = "Radio Button " + i;
                rdo.Location = new Point(5, 25 * i);
                responsePanel.Controls.Add(rdo);
            }

        }






        private void AddTextBox(string question)
        {
            //Controls.Clear();
            responsePanel.Controls.Clear();
            TextBox textBox1 = new TextBox
            {
                Text = "",
                Location = new Point(48, 64),
                Size = new Size(104, 16)
            };
            responsePanel.Controls.Add(textBox1);


            prevButton.Show();
            nextButton.Show();

        }


    }
}
