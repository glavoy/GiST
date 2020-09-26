using gist.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace gist
{
    public partial class Survey : Form
    {
        
        public Survey()
        {
            InitializeComponent();
        }

        // PublicVars has all the global public variables
        PublicVars PubVar = new PublicVars();

        // Maximum number of responses for 'radio' and 'checkbox' type questions
        readonly int maxResponses = 24; // maybe don't need this


        //struct QuestionInfo
        //{
        //    public int quesNum;        // Question number from the xml file
        //    public string fieldName;   // Field name from the xml file
        //    public string fieldType;   // Field type from the xml file (string, integer, decimal, date, N/A - for Information screens)


        //    //Dim QuesType As String          'Type of question [radio button (single response), checkbox (multiple response), or text (open text))
        //    //Dim Response As String          'Text response.  All fields set to "-9" initially
        //    //Dim Value As String             'used to store the numeric 'value' of responses.  Using a string to store multiple values. e.g. 1,3,5
        //    //Dim PrevQues As Integer         'the previous question before this one - in case someone hits the 'Previous' button. Default = -9
        //    //Dim HasBeenAnswered As Boolean  'has question been answered before?  If so display the previous answer
        //};
        //public class XmlFile
        //{
        //    XmlDocument curSurvey = new XmlDocument();
        //    curSurvey.Load(@"..\..\xml\gist.xml");
        //    Console.WriteLine("DocumentElement has {0} questions.", curSurvey.DocumentElement.ChildNodes.Count);
        //}

        // Public variable for the xml document
        readonly XmlDocument xmlDoc = new XmlDocument();


        public class QuestionInfo
        {
            public int quesNum;        // Question number from the xml file
            public string fieldName;   // Field name from the xml file
            public string fieldType;   // Field type from the xml file (string, integer, decimal, date, N/A - for Information screens)
        }



            //Dim QuesType As String          'Type of question [radio button (single response), checkbox (multiple response), or text (open text))
            //Dim Response As String          'Text response.  All fields set to "-9" initially
            //Dim Value As String             'used to store the numeric 'value' of responses.  Using a string to store multiple values. e.g. 1,3,5
            //Dim PrevQues As Integer         'the previous question before this one - in case someone hits the 'Previous' button. Default = -9
            //Dim HasBeenAnswered As Boolean  'has question been answered before?  If so display the previous answer




        // Need to get the number of questions to set the aray size
        //QuestionInfo[] QuestionInfoArray = new QuestionInfo[2];


        public List<QuestionInfo> QuestionInfoList = new List<QuestionInfo>();
        int numQuestions;


        private void Survey_Load(object sender, EventArgs e)
        {
            // Load the xml document for the current survey
            xmlDoc.Load(@"..\..\xml\gist.xml");

            // Get the total number of questions
            // Total number of nodes named 'question' 
            numQuestions = xmlDoc.DocumentElement.ChildNodes.Count;
            

            // If it is an existing survey...
            if (PubVar.modifyingSurvey == true)
            {
                // Populate the 'QuestionInfoList' with the previous responses from the database
                GetResponsesFromPreviousSurvey();
            }
            else
            {
                // This is a new survey
                InitializeSurvey();
                //CreateQuestion();
            }


            

            
        }



        private void GetResponsesFromPreviousSurvey()
        {
            //PubVar.modifyingSurvey = false;

        }

        private void InitializeSurvey()
        {
            //PubVar.modifyingSurvey = true;
            //for (int i = 0; i < 2; i++)
            //{

            //    QuestionInfoArray[i].quesNum = i;
            //    QuestionInfoArray[i].fieldName = String.Concat("Name", Convert.ToString(i));
            //    QuestionInfoArray[i].fieldType = String.Concat("Type", Convert.ToString(i));

            //}

            for (int i = 0; i < numQuestions; i++)
            {
                var question = new QuestionInfo
                {
                    quesNum = i,
                    fieldName = String.Concat("Name", Convert.ToString(i)),
                    fieldType = String.Concat("Type", Convert.ToString(i))
                };

                QuestionInfoList.Add(question);
            }


        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            //XmlDocument itemDoc = new XmlDocument();
            //itemDoc.Load(@"..\..\xml\gist.xml");
            //Console.WriteLine("DocumentElement has {0} questions.", itemDoc.DocumentElement.ChildNodes.Count);



            XmlNode myNode;
            //myNode = xr.GetElementsByTagName("question").Item(0);
            //myNode = curSurvey.GetElementsByTagName("question").Item(1);



            //AddTextBox(myNode);
            this.Close();
        }





        private void CreateQuestion()
        {

            

            // Controls.Clear();
            //InitializeComponent();

            //System.Xml.XmlDocument xr = new System.Xml.XmlDocument();

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



            XmlNode myNode;
            //myNode = xr.GetElementsByTagName("question").Item(0);
            myNode = itemDoc.GetElementsByTagName("question").Item(0);


            // this looks like it may work as well
            //XDocument survey = XDocument.Parse(gist.Properties.Resources.gist);


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

            AddRadioButtons(myNode);
        }



        private void AddRadioButtons(XmlNode question)
        {
            responsePanel.Controls.Clear();

            questionLabel.Text = question.SelectSingleNode("text").InnerText;
  
            string[,] ResponseArray = new string[maxResponses, 2];

            int ResponseArraySize = 0;   //number of responses

            foreach (XmlNode node in question.SelectNodes("responses/response"))
            {
                ResponseArray[ResponseArraySize, 0] = node.InnerText;
                ResponseArray[ResponseArraySize, 1] = node.Attributes["value"].Value;
                ResponseArraySize++;
            }

            

            for (int i = 0; i < ResponseArraySize; i++)
            {
                RadioButton rdo = new RadioButton
                {
                    Text = ResponseArray[i, 0],
                    Tag = ResponseArray[i, 1],
                    Location = new Point(5, 20 * i)
                };
                responsePanel.Controls.Add(rdo);
            }

        }






        private void AddTextBox(XmlNode question)
        {
            questionLabel.Text = question.SelectSingleNode("text").InnerText;

            responsePanel.Controls.Clear();
            TextBox textBox1 = new TextBox
            {
                Text = "",
                Location = new Point(48, 64),
                Size = new Size(104, 16)
            };
            responsePanel.Controls.Add(textBox1);
        }
    }
}
