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

        // Public variable for the xml document
        readonly XmlDocument xmlSurvey = new XmlDocument();

        public class QuestionInfo
        {
            public int quesNum;             // Question number from the xml file
            public string fieldName;        // Field name from the xml file
            public string fieldType;        // Field type from the xml file (string, integer, decimal, date, N/A - for Information screens)
            public string quesType;         // Type of question [radio button (single response), checkbox (multiple response), or text (open text))
            public string response;         // Text response.  All fields set to "-9" initially
            public string value;            // used to store the numeric 'value' of responses.  Using a string to store multiple values. e.g. 1,3,5
            public int prevQues;            // the previous question before this one - in case someone hits the 'Previous' button. Default = -9
            public Boolean hasBeenAnswered; // Has question been answered before?  If so display the previous response

        }

        public List<QuestionInfo> QuestionInfoList = new List<QuestionInfo>();
        
        int numQuestions;

        int currentQuestion;
        int previousQuestion;




        private void Survey_Load(object sender, EventArgs e)
        {
            // Load the xml document for the current survey
            xmlSurvey.Load(@"..\..\xml\gist.xml");

            // Get the total number of questions
            // Total number of nodes named 'question' 
            numQuestions = xmlSurvey.DocumentElement.ChildNodes.Count;
            

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
            }
        }



        private void GetResponsesFromPreviousSurvey()
        {
            //PubVar.modifyingSurvey = false;

        }

        private void InitializeSurvey()
        {
            // For each question in the xml file, add the question information
            // to the QuestionInfoList
            XmlNodeList question = xmlSurvey.GetElementsByTagName("question");
            for (int i = 0; i < question.Count; i++)
            {
                var curQuestion = new QuestionInfo
                {
                    quesNum = i,
                    fieldName = question[i].Attributes["fieldname"].Value,
                    fieldType = question[i].Attributes["fieldtype"].Value,
                    quesType = question[i].Attributes["type"].Value,
                    response = "-9",
                    value = "-9",
                    prevQues = -9,
                    hasBeenAnswered = false
                };

                // Add the question to the list
                QuestionInfoList.Add(curQuestion);
            }


            //set Current and Previous question to 0 and create the first question
            previousQuestion = -1;
            currentQuestion = 0;
            QuestionInfoList[currentQuestion].prevQues = previousQuestion;
            CreateQuestion(currentQuestion, false);
        }




        private void NextButton_Click(object sender, EventArgs e)
        {
            ShowNextQuestion();
        }

        private void ShowNextQuestion()
        {
            currentQuestion += 1;
            CreateQuestion(currentQuestion, QuestionInfoList[currentQuestion].hasBeenAnswered);
        }



        private void CreateQuestion(int questionNum, Boolean ShowPreviousResponse)
        {
            // This gets the entire Node List, but we only need a single node
            XmlNodeList question = xmlSurvey.GetElementsByTagName("question");

            // We need something like this
//            XmlNode myNode = xmlSurvey.GetElementsByTagName("question").Item(questionNum);

            //disable the "Previous" button if we are at the beginning of the survey
            if (currentQuestion > 0)
            {
                PrevButton.Enabled = true;
            }

            switch (question[questionNum].Attributes["type"].Value)
            {
                case "radio":
                    AddRadioButtons(question[questionNum]);
                    break;
                case "checkbox":
                    AddCheckBoxes(question[questionNum]);
                    break;
                case "text":
                    AddTextBox(question[questionNum]);
                    break;
            }

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



        private void AddCheckBoxes(XmlNode question)
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
                CheckBox chkbox = new CheckBox
                {
                    Text = ResponseArray[i, 0],
                    Tag = ResponseArray[i, 1],
                    Location = new Point(5, 20 * i)
                };
                responsePanel.Controls.Add(chkbox);
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
