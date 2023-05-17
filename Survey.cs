using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Configuration;


namespace gist
{
    public partial class Survey : Form
    {
        // Class constructor
        public Survey()
        {
            InitializeComponent();
        }


        /************************************************************
         This section is used to configure a specific survey
         ************************************************************/
        
        // Define any field names and minimum length expected
        // The next button will not become visible until the 
        // minimum lenght is reached
        readonly Tuple<string, int>[] minLengths =
              { new Tuple<string, int>("intid", 2),
                new Tuple<string, int>("add_new", 1) };

        /* End of configuration section
        ***********************************************************/


        // PublicVars has all the global public variables
        //PublicVars PublicVars = new PublicVars();


        // Variable for the xml document
        readonly XmlDocument xmlSurvey = new XmlDocument();


        // Variables for the software version and the xml version
        string swVer;
        string xmlVer;


        // Path to data file
        string dataFile = string.Concat("..\\..\\data\\", PublicVars.survey, ".txt");


        // Path to XML file
        string xmlFile = string.Concat("..\\..\\xml\\", PublicVars.survey, ".xml");



        // QuestionInfo class
        // There is a new QuestionInfo object created for each question in the xml file
        // Each QuestionInfo object is added to the QuestionInfoList List.
        // The QuestionInfoList is populated in either the InitializeNewSurvey() or the 
        // GetResponsesFromPreviousSurvey() function depending on whether it is a new
        // survey or an existing survey
        public class QuestionInfo
        {
            public int quesNum;             // Question number from the xml file
            public string fieldName;        // Field name from the xml file
            public string fieldType;        // Field type from the xml file (string, integer, decimal, date, N/A - for Information screens)
            public string quesType;         // Type of question [radio button (single response), checkbox (multiple response), or text (open text))
            public string response;         // Text response. All fields set to "-9" initially
            public string value;            // Used to store the numeric 'value' of responses.  Using a string to store multiple values. e.g. 1,3,5
            public int prevQues;            // The previous question before this one - in case someone hits the 'Previous' button. Default = -9
            public Boolean hasBeenAnswered; // Has question been answered before?  If so display the previous response
        }


        // List of QuestionInfo objects
        public List<QuestionInfo> QuestionInfoList = new List<QuestionInfo>();


        // Number of questions in the xml file
        int numQuestions;


        // Keeps track of the current and previous question numbers
        // The previousQuestion is not necessarily the question right before the
        // current questions - it will depend on skip patterns
        int currentQuestion;
        int previousQuestion;


        // Values for N/A, Refuse and Don't know buttons
        string naValue = "-6";
        string dontKnowValue = "-7";
        string refuseValue = "-8";


        // Used as the background colour if a 'special button' is selected
        Color selectedColour = Color.PaleVioletRed;


        // Used for the text box control
        int charPerLine = 81;
        int lineHeight = 19;







        // When the form loads...
        private void Survey_Load(object sender, EventArgs e)
        {
            // Load the xml document for the current survey
            xmlSurvey.Load(@xmlFile);

            // XML version
            XmlElement root = xmlSurvey.DocumentElement;
            xmlVer = root.GetAttribute("version");

            // Software version
            swVer = string.Concat(ConfigurationManager.AppSettings["swver"], "_", xmlVer);


            // Get the total number of questions
            // Total number of nodes named 'question' 
            numQuestions = xmlSurvey.DocumentElement.ChildNodes.Count;
            

            // If it is an existing survey...
            if (PublicVars.modifyingSurvey == true)
            {
                // ... populate the 'QuestionInfoList' with the previous responses from the database
                GetResponsesFromPreviousSurvey();
            }
            else
            {
                // ... otherwise, it is a new survey
                InitializeNewSurvey();
            }
        }





        // Function to get the responses from a previously conducted survey
        // And save them to QuestionInfoList
        private void GetResponsesFromPreviousSurvey()
        {
            string[] lines = File.ReadAllLines(dataFile);

            List<string> uniqueIDs = new List<string>();

            foreach (string ln in lines)
            {
                //string[] data = ln.Split(',');
                string[] data = ln.Split(new string[] { ";;;;" }, StringSplitOptions.None);
                if (data.Length > 1)
                {
                    string subjid = data[PublicVars.subjidPos].Replace("\"", "");

                    if (subjid == PublicVars.subjid)
                    {
                        XmlNodeList question = xmlSurvey.GetElementsByTagName("question");
                        for (int i = 0; i < question.Count - 1; i++)
                        {
                            // Update the information for the particular question
                            var curQuestion = new QuestionInfo
                            {
                                quesNum = i,
                                fieldName = question[i].Attributes["fieldname"].Value.Replace("\"", ""),
                                fieldType = question[i].Attributes["fieldtype"].Value.Replace("\"", ""),
                                quesType = question[i].Attributes["type"].Value.Replace("\"", ""),
                                response = data[i].Replace("\"", ""),
                                value = data[i].Replace("\"", ""),
                                prevQues = -9,
                                hasBeenAnswered = data[i] != "-9"
                            };

                            // Add the question to the list
                            QuestionInfoList.Add(curQuestion);
                        }
                    }
                }
            }
            // Need to add this for the last 'information' question since it is not 
            // included in the data
            var infoQuestion = new QuestionInfo
            {
                quesNum = numQuestions,
                fieldName = "end_of_questions",
                fieldType = "n/a",
                quesType = "information",
                response = "-9",
                value = "-9",
                prevQues = -9,
                hasBeenAnswered = false
            };

            // Add the question to the list
            QuestionInfoList.Add(infoQuestion);


            // Set Current and Previous question to 0 and create the first question
            previousQuestion = -1;
            currentQuestion = 0;
            QuestionInfoList[currentQuestion].prevQues = previousQuestion;
            CreateQuestion(currentQuestion, true);

        }




        // Function to initialize the QuestionInfoList with default values
        private void InitializeNewSurvey()
        {
            // For each question in the xml file, add the question information
            // to the QuestionInfoList - field name, field type and type of question
            XmlNodeList question = xmlSurvey.GetElementsByTagName("question");
            for (int i = 0; i < question.Count; i++)
            {
                // Update the information for the particular question
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


            // Check to see if this version of the questionnaire and xml file 
            // is already stored in the data file
            Boolean verFound = false;

            // If it exists, read it
            if (File.Exists(dataFile))
            {
                // Store each line in array of strings 
                string[] lines = File.ReadAllLines(dataFile);

                foreach (string ln in lines)
                {
                    if (ln.CompareTo(swVer) == 0)
                        verFound = true;
                }
                // Version not found in data file
                if (verFound == false)
                    WriteVerToDataFile();
            }
            // It does not exist, so write variable names for the current version
            else
            {
                WriteVerToDataFile();
            }


            // Set Current and Previous questions and create the first question
            previousQuestion = -1;
            currentQuestion = 0;
            QuestionInfoList[currentQuestion].prevQues = previousQuestion;
            CreateQuestion(currentQuestion, false);
        }


        // Write the version and 
        private void WriteVerToDataFile()
        {
            // Variable for string of data to write to file
            string dataToSave = "";

            // Use numQuestions - 1 because we do not want to include the last 
            // 'information' question.
            for (var i = 0; i < numQuestions - 1; i++)
            {
                if (i != numQuestions - 2)
                {
                    dataToSave = string.Concat(dataToSave, "\"", QuestionInfoList[i].fieldName, "\"", ",");
                }
                else
                {
                    dataToSave = string.Concat(dataToSave, "\"", QuestionInfoList[i].fieldName, "\"");
                }
            }
            File.AppendAllText(dataFile, swVer + Environment.NewLine);
            File.AppendAllText(dataFile, dataToSave + Environment.NewLine);
        }



        // This function responds to the Next Button Click event
        private void NextButton_Click(object sender, EventArgs e)
        {
            // IsValidResponse checks for numeric range checks, logic checks and any manual checks
            // If response is valid, save the data, set the question numbers and show the next question.
            // If we are at the end of the survey, save the data
            if (IsValidResponse() == true)
            {
                // Save data to the QuestionInfoList if it not an automatic question
                // Responses for automatic questions are saved to the list in
                // the AddAutomatic() function
                if (QuestionInfoList[currentQuestion].quesType != "automatic")
                {
                    SaveDataToList();
                }

                ShowNextQuestion();

            }
        }





        // This function responds to the Previous Button Click event
        private void PrevButton_Click(object sender, EventArgs e)
        {
            // Set the previous question number stored in the 'prevQues' varible of the QuestionInfoList
            //previousQuestion = QuestionInfoList[currentQuestion].prevQues;
            
            
            currentQuestion = QuestionInfoList[currentQuestion].prevQues;

            /* This was the 'old' code - need to test if we actually need this or not
             
            // If it is an 'automatic' type question...
            if (QuestionInfoList[currentQuestion].quesType == "automatic")
            {
                // ...get the previous question from the QuestionInfoList
                currentQuestion = QuestionInfoList[currentQuestion].prevQues;
            }
            else
            {
                // ...otherwise set the current question to be the previous question
                currentQuestion = previousQuestion;
            }

             */


            // The first question's 'prevQues' was initialized to -1, so in this case,
            // set the current question to 0.
            currentQuestion = currentQuestion == -1 ? currentQuestion = 0 : currentQuestion;

            // Show the question
            CreateQuestion(currentQuestion, QuestionInfoList[currentQuestion].hasBeenAnswered);
        }



        // Click event for the Don't know button
        private void dontKnowButton_Click(object sender, EventArgs e)
        {
            SpecialButtonClick(int.Parse(dontKnowValue));
        }



        // Click event for the Not Applicable button
        private void notApplicableButton_Click(object sender, EventArgs e)
        {
            SpecialButtonClick(int.Parse(naValue));
        }



        // Click event for the Refuse to Answer button
        private void refuseToAnswerButton_Click(object sender, EventArgs e)
        {
            SpecialButtonClick(int.Parse(refuseValue));
        }


        // Update info special button and show the next question
        private void SpecialButtonClick(int buttonValue)
        {
            // pause half a second before moving to next question
            System.Threading.Thread.Sleep(500);

            QuestionInfoList[currentQuestion].response = buttonValue.ToString();
            QuestionInfoList[currentQuestion].value = buttonValue.ToString();
            QuestionInfoList[currentQuestion].hasBeenAnswered = true;

            ShowNextQuestion();
        }




        // This function checks to see if the response from the user is valid
        // There are Numeric checks, Logic checks and in some cases, Manual checks
        private Boolean IsValidResponse()
        {
            
            // Get the current question from the XML file
            XmlNode question = xmlSurvey.GetElementsByTagName("question").Item(currentQuestion);


            // Range Check
            // Check if there is a Range Check for this question
            XmlNode rangeCheck = question.SelectSingleNode("rangeCheck");

            // If there is a numeric check, call the RangeCheck() function
            if (rangeCheck != null)
            {
                if (NumericRangeCheck(int.Parse(rangeCheck.Attributes["minvalue"].Value),
                                     int.Parse(rangeCheck.Attributes["maxvalue"].Value),
                                     rangeCheck.Attributes["message"].Value) == false)
                {
                    return false;
                }
            }



            // Logic check
            // Check if there is a Range Check for this question
            XmlNode logicCheck = question.SelectSingleNode("logic_check");

            // If there is a numeric check, call the RangeCheck() function
            if (logicCheck != null)
            {
                foreach (XmlNode logic in logicCheck)
                {
                    string fieldname = logic.Attributes["fieldname"].Value;

                    //TesLogicCheckOK(fieldname, condition, response, response_type, currentresponse, message)

                    if (TesLogicCheckOK(logic.Attributes["fieldname"].Value,
                                        logic.Attributes["condition"].Value,
                                        logic.Attributes["response"].Value,
                                        logic.Attributes["response_type"].Value,
                                        logic.Attributes["currentresponse"].Value,
                                        logic.Attributes["message"].Value) == false)
                    {
                        return false;
                    }
                }
            }


            // Manaul checks


            return true;
        }


        private Boolean NumericRangeCheck(int minvalue, int maxvalue, string message)
        {
            foreach (Control control in responsePanel.Controls)
            {
                switch (control.GetType().Name)
                {
                    case "TextBox":
                        if (long.Parse(control.Text) >= minvalue && long.Parse(control.Text) <= maxvalue)
                        {
                            return true;
                        }
                        break;
                }
            }
            MessageBox.Show(message);
            return false;
        }


        private Boolean TesLogicCheckOK(string fieldname, string condition, string response, string response_type, string currentresponse, string message)
        {
            return true;
        }



        private void ShowNextQuestion()
        {
            // If we are not at the end of the survey....
            if (currentQuestion < numQuestions - 1)
            {
                // ...set the next and previous question numbers
                SetQuestionNumbers();

                // ...show the next question 
                CreateQuestion(currentQuestion, QuestionInfoList[currentQuestion].hasBeenAnswered);
            }
            else
            {
                // ...otherwise, save the data
                SaveData();
                this.Close();
                this.Dispose();
            }
            subjIDLabel.Text = PublicVars.subjid;
        }




        // Set the current question number and the previous 
        // question number based on skips
        private void SetQuestionNumbers()
        {

            // To Do....
            // Check for skips........

            // Check if there is a post skip for this question,
            // if not....
            if (CheckForSkip("postskip") == false)
            {
                // We can't use the current question to set the previous question if it
                // is an automatic question since we need to go back to the last question
                // displayed.  If we jsut go back to the previous, it is an aotomatic and
                // will jsut move forward again!
                if (QuestionInfoList[currentQuestion].quesType != "automatic")
                {
                    previousQuestion = currentQuestion;
                }

                // ... increment the question number
                currentQuestion += 1;
                // and set the prevQues for the current in the QuestionInfoList
                QuestionInfoList[currentQuestion].prevQues = previousQuestion;
            }
            else // ...otherwise, set all skipped questions to have values of -9
            {
                for (int i = previousQuestion + 1; i < currentQuestion; i++)
                {
                    if (previousQuestion == -1)
                    {
                        previousQuestion = 0;
                    }
                    QuestionInfoList[i].hasBeenAnswered = false;
                    QuestionInfoList[i].value = "-9";
                    QuestionInfoList[i].response = "-9";
                    QuestionInfoList[i].prevQues = -9;

                }
            }


            // Preskip
            while (CheckForSkip("preskip") != false)
            {
                for (int j = previousQuestion + 1; j < currentQuestion; j++)
                {
                    if (QuestionInfoList[j].quesType != "automatic")
                    {
                        QuestionInfoList[j].hasBeenAnswered = false;
                        QuestionInfoList[j].value = "-9";
                        QuestionInfoList[j].response = "-9";
                        QuestionInfoList[j].prevQues = -9;
                    }
                }
            } 
        }



        private Boolean CheckForSkip(string skiptype)
        {
            //Boolean returnValue = false;

            // Create an XML node containing all the information about the question
            XmlNode question = xmlSurvey.GetElementsByTagName("question").Item(currentQuestion);

            foreach (XmlNode skipnode in question.SelectNodes(skiptype))
            {
                foreach (XmlNode skip in skipnode)
                {
                    if (TestSkipCondition(skip.Attributes["fieldname"].Value,
                                          skip.Attributes["condition"].Value,
                                          skip.Attributes["response"].Value,
                                          skip.Attributes["response_type"].Value) == true)
                    {
                        // No need to set previous question if we are skipping this question (for a preskip)
                        if (skiptype == "postskip")
                        {
                            previousQuestion = currentQuestion;
                        }
                        currentQuestion = GetQuestionNumber(skip.Attributes["skiptofieldname"].Value);
                        QuestionInfoList[currentQuestion].prevQues = previousQuestion;
                        return true;
                    }
                }
            }
            return false;
        }


        private Boolean TestSkipCondition(string fieldname, string condition, string response, string response_type)
        {
            var question = QuestionInfoList.FirstOrDefault(o => o.fieldName == fieldname);
            string currentValue = question.value;
            int questionNumber = question.quesNum;
            string questionType = question.quesType;
            string fieldType = question.fieldType;



            if (questionType == "checkbox" && currentValue.Length > 1)
            {
                return CheckMultipleResponses(currentValue, response, condition);
            }
            else if (currentValue.All(char.IsNumber) == true)
            {
                if (currentValue.All(char.IsNumber) == true)
                {
                    int compareValue = response_type == "fixed" ? int.Parse(response) : int.Parse(GetValue(response));
                    switch (condition)
                    {
                        case "=":
                            if (int.Parse(currentValue) == compareValue)
                                return true;
                            break;
                        case "<":
                            if (int.Parse(currentValue) < compareValue)
                                return true;
                            break;
                        case ">":
                            if (int.Parse(currentValue) > compareValue)
                                return true;
                            break;
                        case "<>":
                        case "does not contain":
                            if (int.Parse(currentValue) != compareValue)
                                return true;
                            break;
                    }
                }
            }

            else
            {
                string compareValue = response_type == "fixed" ? response : GetValue(response);
                switch (condition)
                {
                    case "=":
                        if (currentValue.CompareTo(compareValue) == 0)
                                return true;
                        break;
                    case "<>":
                        if (currentValue.CompareTo(compareValue) != 0)
                            return true;
                        break;
                }
            }
            return false;
        }



        private bool CheckMultipleResponses(string currentValue, string response, string condition)
        {
            Boolean returnValue = false;

            string[] responseArray = currentValue.Split(',');
            for (var i = 0; i < responseArray.Length; i++)
            {
                switch (condition)
                {
                    case "does not contain":
                        returnValue = true;
                        if (responseArray[i] == response)
                            return false;
                        break;

                    case "contains":
                        returnValue = false;
                        if (responseArray[i] == response)
                            return true;
                        break;

                }
            }
            return returnValue;
        }



        // Function to show the question on the main survey screen
        private void CreateQuestion(int questionNum, Boolean ShowPreviousResponse)
        {
            // Create an XML node containing all the information about the question
            XmlNode question = xmlSurvey.GetElementsByTagName("question").Item(currentQuestion);


            // Call the appropriate function based on te question type
            switch (question.Attributes["type"].Value)
            {
                case "radio":
                    AddRadioButtons(question);
                    break;
                case "checkbox":
                    AddCheckBoxes(question);
                    break;
                case "text":
                    AddTextBox(question);
                    break;
                case "automatic":
                    AddAutomatic();
                    break;
                case "information":
                    AddInformation(question);
                    break;
            }






            if (question.Attributes["type"].Value != "automatic")
            {
                // Default all special buttons to not visible,
                // in case they were visible from the previous question
                notApplicableButton.Visible = false;
                refuseToAnswerButton.Visible = false;
                dontKnowButton.Visible = false;

                notApplicableButton.BackColor = Color.LightGray;
                refuseToAnswerButton.BackColor = Color.LightGray;
                dontKnowButton.BackColor = Color.LightGray;


                // Check if Not Applicable Button needs to be shown 
                // and if it needs to be selected 
                if (question.SelectSingleNode("na") != null)
                {
                    notApplicableButton.Visible = true;
                    if (ShowPreviousResponse == true && QuestionInfoList[currentQuestion].value == naValue)
                    {
                        notApplicableButton.BackColor = selectedColour;
                        EnableNextButton();
                        NextButton.Focus();
                    }
                }

                // Check if Refuse Button needs to be shown 
                // and if it needs to be selected 
                if (question.SelectSingleNode("refuse") != null)
                {
                    refuseToAnswerButton.Visible = true;
                    if (ShowPreviousResponse == true && QuestionInfoList[currentQuestion].value == refuseValue)
                    {
                        refuseToAnswerButton.BackColor = selectedColour;
                        EnableNextButton();
                        NextButton.Focus();
                    }
                }


                // Check if Not Applicable Button needs to be shown 
                // and if it needs to be selected 
                if (question.SelectSingleNode("dont_know") != null)
                {
                    dontKnowButton.Visible = true;
                    if (ShowPreviousResponse == true && QuestionInfoList[currentQuestion].value == dontKnowValue)
                    {
                        dontKnowButton.BackColor = selectedColour;
                        EnableNextButton();
                        NextButton.Focus();
                    }
                }


            }

            // If we are supposed to show the previous response or if it the last question
            if (ShowPreviousResponse == true || questionNum == numQuestions - 1)
            {
                EnableNextButton();
                NextButton.Focus();
            }
            else
            {
                DisableNextButton();
            }

            // Disable the "Previous" button if we are at the beginning of the survey
            if (QuestionInfoList[currentQuestion].prevQues == -1)
            {
                DisablePrevButton();
            }
            else
            {
                EnablePrevButton();
            }
        }





        // Add the radio buttons to the panel
        private void AddRadioButtons(XmlNode question)
        {
            // Clear previous controls from response area
            responsePanel.Controls.Clear();

            // Display the question text
            questionLabel.Text = SubstituteFieldNames(question.SelectSingleNode("text").InnerText);

            // Get a list of the responses
            XmlNodeList responses = question.SelectNodes("responses/response");

            // Populate a 'ResponseArray' with all of the responses for the current question
            string[,] ResponseArray = GetAllResponses(responses);


            // show the radio buttons on the panel
            for (int i = 0; i < responses.Count; i++)
            {
                RadioButton rdo = new RadioButton
                {
                    Text = ResponseArray[i, 0],
                    Tag = ResponseArray[i, 1],
                    Location = new Point(5, 20 * i)
                };
                responsePanel.Controls.Add(rdo);

                // Add handler for Click event
                rdo.Click += new System.EventHandler(this.RadioButtonHandler_Click);


                // if showing the question again, show previous response
                if (QuestionInfoList[currentQuestion].hasBeenAnswered == true && ResponseArray[i, 1] == QuestionInfoList[currentQuestion].value)
                {
                    rdo.Checked = true;
                }


            }
        }


        // This handles the click event for a radio button
        private void RadioButtonHandler_Click(object sender, EventArgs e)
        {
            try
            {
                if ((sender.GetType().Name == "RadioButton"))
                {
                    System.Threading.Thread.Sleep(600);
                    NextButton_Click(null, null);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }





        // Add the check boxes to the panel
        private void AddCheckBoxes(XmlNode question)
        {
            // Clear previous controls from response area
            responsePanel.Controls.Clear();

            // Display the question text
            questionLabel.Text = question.SelectSingleNode("text").InnerText;

            // Get a list of the responses
            XmlNodeList responses = question.SelectNodes("responses/response");

            // Populate a 'ResponseArray' with all of the responses for the current question
            string[,] ResponseArray = GetAllResponses(responses);

            // Show the checkboxes on the panel
            for (int i = 0; i < responses.Count; i++)
            {
                CheckBox chkbox = new CheckBox
                {
                    Text = ResponseArray[i, 0],
                    Tag = ResponseArray[i, 1],
                    Location = new Point(5, 20 * i)
                };
                responsePanel.Controls.Add(chkbox);

                // Add handler for CheckedChanged event
                chkbox.CheckedChanged += new System.EventHandler(this.CheckBoxHandler_CheckedChanged);

            }


            // Show previous responses, if appropriate
            if (QuestionInfoList[currentQuestion].hasBeenAnswered == true)
            {
                ShowCheckBoxResponses();
            }
        }


        // This handles the Click event for the Checkboxes.
        // It is used to determine wether of not we should 
        // enable or disable the 'Next' button (if none are checked - it is disabled)
        private void CheckBoxHandler_CheckedChanged(object sender, EventArgs e)
        {
            int numCheckBoxesChecked = 0;

            foreach (Control control in responsePanel.Controls)
            {
                switch (control.GetType().Name)
                {
                    case "CheckBox":
                        if (control is CheckBox checkbox && checkbox.Checked)
                        {
                            numCheckBoxesChecked += 1;
                        }
                        break;
                }
            }

            // If none are checked, disable the next button
            if (numCheckBoxesChecked > 0)
            {
                EnableNextButton();
            }
            else
            {
                DisableNextButton();
            }

        }


        // This fucntion is used to show the checkbox as checked if it was previously checked
        private void ShowCheckBoxResponses()
        {

            string[] valueArray = QuestionInfoList[currentQuestion].value.Split(',');

            foreach (Control control in responsePanel.Controls)
            {
                switch (control.GetType().Name)
                {
                    case "CheckBox":
                        if (control is CheckBox checkbox && valueArray.Contains(checkbox.Tag))
                        {
                            checkbox.Checked = true;
                        }
                        break;
                }
            }

        }


        // This function is used by the AddRadioButtons and AddCheckBoxes functions.
        // It returns an array with the text to display and the numeric value
        private string[,] GetAllResponses(XmlNodeList responses)
        {
            // Number of responses
            int numResponses = responses.Count;

            // Array to store the text and values foe each option
            string[,] ResponseArray = new string[numResponses, 2];

            // Populate the array
            for (int i = 0; i < responses.Count; i++)
            {
                ResponseArray[i, 0] = responses[i].InnerText;
                ResponseArray[i, 1] = responses[i].Attributes["value"].Value;
            }
            return ResponseArray;
        }





        // Adds the text box to the panel
        private void AddTextBox(XmlNode question)
        {
            // Show the question
            questionLabel.Text = question.SelectSingleNode("text").InnerText;

            // Clear controls from previous question
            responsePanel.Controls.Clear();

            // Create a new Text Box
            TextBox newTextBox = new TextBox
            {
                Text = "",
                Location = new Point(20, 20),
                Width = 500
            };

            // Set the MaxLength property based on 'maxCharacters' in the XML file
            XmlNode hasMaxCharacters = question.SelectSingleNode("maxCharacters");
            if (hasMaxCharacters != null)
            {
                newTextBox.MaxLength = int.Parse(hasMaxCharacters.InnerText);
            }

            

            // Calculate the number of lines that should be allowed for.
            if (newTextBox.MaxLength > 0)
            {
                //int numLines = (newTextBox.MaxLength \ charPerLine) + 1;
                int numLines = (newTextBox.MaxLength / charPerLine) + 1;

                // Calculate how large the textbox should be, and whether scrollbars are necessary.
                if (numLines == 1)
                {
                    newTextBox.Multiline = false;
                }
                else
                {
                    newTextBox.Multiline = true;
                    newTextBox.Height = 8 * lineHeight;
                    newTextBox.ScrollBars = ScrollBars.Vertical;
                }

            }


            // Add the Text box to the panel
            responsePanel.Controls.Add(newTextBox);

            // Set the focus on the Text box
            newTextBox.Focus();

            // Make all text uppercase
            newTextBox.CharacterCasing = CharacterCasing.Upper;

            // Add event handlers for KeyUp and KeyPress
            newTextBox.KeyUp += new KeyEventHandler(TextBoxHandlerKeyUp);
            newTextBox.KeyPress += new KeyPressEventHandler(TextBoxHandlerKeyPress);


            if (QuestionInfoList[currentQuestion].hasBeenAnswered == true)
            {
                newTextBox.Text = QuestionInfoList[currentQuestion].response;
            }
        }


        // Event handler for keyup event on textbox
        // This is used to determine if the 'Next' button should be enabled or disabled
        private void TextBoxHandlerKeyUp(object sender, EventArgs e)
        {
            try
            {
                // Sets the minimun length of text to enter in the Text box
                int MinLength = 1;

                //  Verify that the type of control triggering this event is a text box
                if ((sender.GetType().Name == "TextBox"))
                {
                    // If the field is in the 'minLengths' Tuple array
                    // Then we can set the minimum length of text to be entered
                    foreach (var minLength in minLengths)
                    {
                        if (minLength.Item1 == QuestionInfoList[currentQuestion].fieldName)
                        {
                            MinLength = minLength.Item2;
                        }
                    }
                }

                // Check the length of the text - if it > 0, then enable the "Next button"
                // Unless a length is specified above for a particular field
                if (((TextBox)(sender)).Text.Length >= MinLength)
                {
                    EnableNextButton();
                }
                else
                {
                    DisableNextButton();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        // Event handler for keypress event on the textbox
        // This is used to determine if the key pressed was a number or a decimal point
        private void TextBoxHandlerKeyPress(object sender, KeyPressEventArgs e)
        {
            //  Verify that the type of control triggering this event is indeed a Radio Button.
            if ((sender.GetType().Name == "TextBox"))
            {

                // Allow numbers only for 'integer' type question
                if (QuestionInfoList[currentQuestion].fieldType == "integer")
                {
                    // If key pressed was not a number or a special character,
                    // display error message
                    if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                        MessageBox.Show("Only numbers are allowed!");
                        return;
                    }
                }

                // Allow numbers and decimal place for 'decimal' type questions
                // Ascii code for '.' is 46
                if (QuestionInfoList[currentQuestion].fieldType == "decimal")
                {
                    // If key pressed was not a number, special character,
                    // or a decimal, display error message
                    if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != 46)
                    {
                        e.Handled = true;
                        MessageBox.Show("Only numbers and decimal point are allowed!");
                        return;
                    }
                }
            }
        }



        // Add the automatic question type
        private void AddAutomatic()
        {

            // Stores the current 'Auto' value for automatic question type
            string currentAutoValue = "";


            switch (QuestionInfoList[currentQuestion].fieldName)
            {
                // Start time of interview
                case "starttime":
                    currentAutoValue = GetValue("starttime");
                    if (currentAutoValue == "" || 
                        currentAutoValue == "01/01/1899 00:00:00" || 
                        currentAutoValue == "01/01/1899 12:00:00 AM" || 
                        currentAutoValue == "-9")
                    {
                        DateTime now = DateTime.Now;
                        currentAutoValue = now.ToString("dd/MM/yyyy HH:mm:ss");
     
                    }
                    break;



                // Sunbect ID
                case "subjid":
                    currentAutoValue = PublicVars.subjid;
                    break;



                // Software Version
                case "swver":
                    currentAutoValue = swVer;
                    break;


                // Stop time of interview
                case "stoptime":
                    currentAutoValue = GetValue("stoptime");
                    if (currentAutoValue == "" ||
                        currentAutoValue == "01/01/1899 00:00:00" ||
                        currentAutoValue == "01/01/1899 12:00:00 AM" ||
                        currentAutoValue == "-9")
                    {
                        DateTime now = DateTime.Now;
                        currentAutoValue = now.ToString("dd/MM/yyyy HH:mm:ss");

                    }
                    break;
            }

            // Update the information in the QuestionInfoList
            QuestionInfoList[currentQuestion].response = currentAutoValue;
            QuestionInfoList[currentQuestion].value = currentAutoValue;
            QuestionInfoList[currentQuestion].hasBeenAnswered = true;


            // Automatically click the 'Next' button
            NextButton_Click(null, null);
        }


 
        // Add the 'information' type question
        private void AddInformation(XmlNode question)
        {
            // Clear the controls from previous question
            responsePanel.Controls.Clear();

            // Display the text
            questionLabel.Text = question.SelectSingleNode("text").InnerText;

            // Set the focus on the 'Next' button
            NextButton.Focus();
        }





        // This function returns the questions text with any field names substutued
        // as per the XML file.  Substituted field names are in the form [[xxxx]],
        // where xxxx is the field name whose value is to be substituted
        private string SubstituteFieldNames(string originalQuestion)
        {
            // Get index of '[[' in the original text
            int startPos = originalQuestion.IndexOf("[[");

            // Don't need to do anything if there is no substitution to do
            if (startPos != -1)
            {
                // And get index of ']]' in the original text
                int endPos = originalQuestion.IndexOf("]]");

                // Get the field name between [[ xxx ]] in the original question
                string fieldName = originalQuestion.Substring(startPos + 2, endPos - startPos - 2);

                // Get the question object from the  QuestionInfoList for this fieldname
                var question = QuestionInfoList.Find(item => item.fieldName == fieldName);

                // return the new question
                return originalQuestion.Replace(string.Concat("[[", fieldName, "]]"), question.response);

            }

            // No substitution performed, so return orignal string
            return originalQuestion;
        }








        // This function stores the current question's response
        // into the QuestionInfoList.
        private void SaveDataToList()
        {
            // Used for checkboxes and radio buttons to determine if 
            // 'none' were selected, which means a 'Special' button was selected
            Boolean anyValueSelected = false;

            // Variable used to keep track of which type of control is on the panel
            string controlType = "";

            // Values to be saved to the QuestionInfoList Text and Value variables
            string surveyResponseText = "";
            string surveyResponseValue = "-9";


            // Loop through the controls on the panel
            // There can only be one type of control at a time on the panel
            // If it was an automatic question, the data was already saved
            // into the QuestionInfoList
            foreach (Control control in responsePanel.Controls)
            {
                switch (control.GetType().Name)
                {
                    // Radio buttons
                    case "RadioButton":
                        RadioButton radio = control as RadioButton;
                        if (radio != null && radio.Checked)
                        {
                            // Save the info for the checked one
                            surveyResponseText = radio.Text;
                            surveyResponseValue = (string)radio.Tag;
                            anyValueSelected = true;
                        }
   
                        // If none are seleceted, then the Text and Value variables 
                        // were updated in the code for the 'Special' button_click.
                        if (anyValueSelected == false)
                        {
                            surveyResponseText = QuestionInfoList[currentQuestion].response;
                            surveyResponseValue = QuestionInfoList[currentQuestion].value;
                        }
                        break;




                    // Check boxes
                    case "CheckBox":
                        CheckBox checkbox = control as CheckBox;
                        if (checkbox != null && checkbox.Checked)
                        {
                            if (surveyResponseText == "") 
                            {
                                // This is the first checkBox selected
                                surveyResponseText = checkbox.Text;
                                surveyResponseValue = (string)checkbox.Tag;
                            }
                            else
                            {
                                // There is already a previously selected checkbox
                                surveyResponseText = String.Concat(surveyResponseText, ",", checkbox.Text);
                                surveyResponseValue = String.Concat(surveyResponseValue, ",", checkbox.Tag);
                            }
                            anyValueSelected = true;
                        }

                        break;




                    // Text box
                    case "TextBox":
                        // Special button was clicked
                        if (control.Text == "")
                        {
                            surveyResponseText = QuestionInfoList[currentQuestion].response;
                            surveyResponseValue = QuestionInfoList[currentQuestion].value;
                        }
                        else
                        {
                            // Otherwise, get the value from the text box
                            surveyResponseText = control.Text;
                            surveyResponseValue = control.Text;
                        }
                        break;




                    // Date/time picker
                    case "DateTimePicker":
                        MessageBox.Show("checkbox");
                        break;



                    // Combobox
                    case "ComboBox":
                        MessageBox.Show("checkbox");
                        break;
                }
            }

            // If none are seleceted, then the Text and Value variables 
            // were updated in the code for the 'Special' button_click.
            if (anyValueSelected == false && (controlType == "RadioButton" || controlType == "CheckBox"))
            {
                surveyResponseText = QuestionInfoList[currentQuestion].response;
                surveyResponseValue = QuestionInfoList[currentQuestion].value;
            }


            // Update the QuestionInfoList
            QuestionInfoList[currentQuestion].response = surveyResponseText;
            QuestionInfoList[currentQuestion].value = surveyResponseValue;
            QuestionInfoList[currentQuestion].hasBeenAnswered = true;
        }




        // Save the data
        private void SaveData()
        {
            // Variable for string of data to write to file
            string dataToSave = "";

            // Use numQuestions - 1 because we do not want to include the last 
            // 'information' question.
            for (var i = 0; i < numQuestions - 1; i++)
            {
                dataToSave = i != numQuestions - 2 ? string.Concat(dataToSave, "\"", QuestionInfoList[i].value, "\"", ";;;;") : 
                                                     dataToSave = string.Concat(dataToSave, "\"", QuestionInfoList[i].value, "\"");
            }
            File.AppendAllText(dataFile, dataToSave + Environment.NewLine);
            MessageBox.Show("Data has been saved");
        }


        // Interview was cancelled
        private void cancelButton_Click(object sender, EventArgs e)
        {

        }



        /************************************************************
         Helper Functions
         ************************************************************/

        // Disable Previous Button
        private void DisablePrevButton()
        {
            PrevButton.BackColor = Color.LightGray;
            PrevButton.Enabled = false;
        }

        // Enable Previous Button
        private void EnablePrevButton()
        {
            PrevButton.BackColor = ColorTranslator.FromHtml("#4C75B4");
            PrevButton.Enabled = true;
        }

        // Disable Next Button
        private void DisableNextButton()
        {
            NextButton.BackColor = Color.LightGray;
            NextButton.Enabled = false;
        }

        // Disable/Enable Previous/Next Buttons
        private void EnableNextButton()
        {
            NextButton.BackColor = ColorTranslator.FromHtml("#4C75B4");
            NextButton.Enabled = true;
        }



        // Function to return the value based on the fieldname passed to it
        private string GetValue(string fieldname)
        {
            var question = QuestionInfoList.FirstOrDefault(o => o.fieldName == fieldname);
            return question != null ? question.value : "-9";
        }


        // Function to return the question number of the fieldname passed to it
        private int GetQuestionNumber(string fieldname)
        {
            var question = QuestionInfoList.FirstOrDefault(o => o.fieldName == fieldname);
            return question != null ? question.quesNum : -9;
        }






    }
}
