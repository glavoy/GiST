﻿using gist.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
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


        // PublicVars has all the global public variables
        PublicVars PublicVars = new PublicVars();


        // Variable for the xml document
        readonly XmlDocument xmlSurvey = new XmlDocument();


        // QuestionInfo class
        // There is a new QuestionInfo object created for each question in the xml file
        // Each QuestionInfo object is added to the QuestionInfoList List
        // The QuestionInfoList is populated in either the InitializeSurvey() or the 
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


        // Stores the current 'Auto' value for automatic question type
        string currentAutoValue = "";



        /************************************************************
         This section is used for configuration of a specific survey
         ************************************************************/
        // Define any field names and minimum lenght expected
        // The next button will not become visible until the 
        // minimum lenght is reached
        Tuple<string, int>[] minLengths =
              { new Tuple<string, int>("intid", 2),
                new Tuple<string, int>("some_dec", 4),
                new Tuple<string, int>("add_new", 1) };

        /* End of configuration section
        ***********************************************************/









        // When the form loads...
        private void Survey_Load(object sender, EventArgs e)
        {
            // Load the xml document for the current survey
            xmlSurvey.Load(@"..\..\xml\gist.xml");

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
                InitializeSurvey();
            }
        }





        // Function to get the responses from a previously conducted survey
        // And save them to QuestionInfoList
        private void GetResponsesFromPreviousSurvey()
        {
            // To Do...

        }




        // Function to initialize the QuestionInfoList with default values
        private void InitializeSurvey()
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


            // Set Current and Previous questions and create the first question
            previousQuestion = -1;
            currentQuestion = 0;
            QuestionInfoList[currentQuestion].prevQues = previousQuestion;
            CreateQuestion(currentQuestion, false);
            
        }





        // This function responds to the Next Button Click event
        private void NextButton_Click(object sender, EventArgs e)
        {
            // IsValidResponse checks for numeric range checks, logic checks and any manual checks
            // If response is valid, save the data, set the question numbers and show the next question.
            // If we are at the end of the survey, save the data
            if (IsValidResponse() == true)
            {
                // Save data to the QuestionInfoList
                SaveDataToList();

                // Set the next and previous question numbers
                SetQuestionNumbers();

                // If we are not at the end of the survey....
                if (currentQuestion < numQuestions - 1)
                {
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
            if (currentQuestion == -1)
            {
                currentQuestion = 0;
            }


            // Show the question
            CreateQuestion(currentQuestion, QuestionInfoList[currentQuestion].hasBeenAnswered);
        }





        // This function checks to see if the response from the user is valid
        private Boolean IsValidResponse()
        {
            return true;

            // Numeric range check


            // Logic check


            // Manaul checks

        }



        // Set the current question number and the previous 
        // question number based on skips
        private void SetQuestionNumbers()
        {

            // To Do....
            // Check for skips........

            // We can't use the current question to set the previous question if it
            // is an automatic question since we need to go back to the last question
            // displayed.  If we jsut go back to the previous, it is an aotomatic and
            // will jsut move forward again!
            if (QuestionInfoList[currentQuestion].quesType != "automatic")
            {
                previousQuestion = currentQuestion;
            }



            currentQuestion += 1;
            QuestionInfoList[currentQuestion].prevQues = previousQuestion;
        }





        private void CreateQuestion(int questionNum, Boolean ShowPreviousResponse)
        {
            XmlNode curQuestion = xmlSurvey.GetElementsByTagName("question").Item(questionNum);




            switch (curQuestion.Attributes["type"].Value)
            {
                case "radio":
                    AddRadioButtons(curQuestion, ShowPreviousResponse);
                    break;
                case "checkbox":
                    AddCheckBoxes(curQuestion, ShowPreviousResponse);
                    break;
                case "text":
                    AddTextBox(curQuestion, ShowPreviousResponse);
                    break;
                case "automatic":
                    AddAutomatic();
                    break;
            }


            if (ShowPreviousResponse == true)
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
        private void AddRadioButtons(XmlNode question, Boolean ShowPreviousResponse)
        {
            // Clear previous controls from response area
            responsePanel.Controls.Clear();

            // Display the question text
            questionLabel.Text = question.SelectSingleNode("text").InnerText;

            // Get a list of the responses
            XmlNodeList responses = question.SelectNodes("responses/response");

            // Populate a 'ResponseArray' with all of the responses for the current question
            string[,] ResponseArray = GetAllResponses(responses);



            // used to make sure the previous response is valid
            // mainly in case the user changes the HHID and makes village/sub, etc incalid
            Boolean foundPrevResponse = false;


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
                if (ShowPreviousResponse == true && ResponseArray[i, 1] == QuestionInfoList[currentQuestion].value)
                {
                    rdo.Checked = true;
                    foundPrevResponse = true;
                }
         

            }


            // Uncomment the code below when the radio button handler is working

            // Check to see if we should disable the "Next" button
            //if (ShowPreviousResponse == true && foundPrevResponse == true)
            //{
            //    NextButton.Visible = true;
            //    NextButton.Focus();
            //}
            //else
            //{
            //    NextButton.Visible = false;
            //}
        }



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
        private void AddCheckBoxes(XmlNode question, Boolean ShowPreviousResponse)
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
            if (ShowPreviousResponse == true)
            {
                ShowCheckBoxResponses();
                EnableNextButton();
                NextButton.Focus();
            }
            else
            {
                DisableNextButton();
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








        /*
         

                    Dim aControl As Control
            Dim aGroupControl As Control

            Dim ValueArray(MaxResponses)
            ValueArray = Split(QuestionInfoArray(CurrentQuestion).Value, ",")

            'Determine the number of check bosex checked
            For Each aControl In Me.Controls
                ' Differentiate output based on the type of the control
                Select Case TypeName(aControl)
                    Case "GroupBox"
                        ' Need to go inside of the GroupBox to yank out the RadioButtons
                        For Each aGroupControl In CType(aControl, GroupBox).Controls
                            If TypeOf aGroupControl Is CheckBox Then
                                'For i = 0 To ValueArray.Length
                                If ValueArray.Contains(CType(aGroupControl, CheckBox).Tag) Then
                                    CType(aGroupControl, CheckBox).Checked = True
                                End If
                            End If

                        Next
                End Select
            Next

         */





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






        private void AddTextBox(XmlNode question, Boolean ShowPreviousResponse)
        {
            questionLabel.Text = question.SelectSingleNode("text").InnerText;

            responsePanel.Controls.Clear();
            TextBox newTextBox = new TextBox
            {
                Text = "",
                Location = new Point(48, 64),
                Size = new Size(104, 16)
            };
            responsePanel.Controls.Add(newTextBox);

            newTextBox.Focus();
            newTextBox.CharacterCasing = CharacterCasing.Upper;

            // Add event handlers for KeyUp and KeyPress
            newTextBox.KeyUp += new KeyEventHandler(TextBoxHandlerKeyUp);
            newTextBox.KeyPress += new KeyPressEventHandler(TextBoxHandlerKeyPress);


            // To Do
            // Add 'special' buttons
            // Also, this is displaying first 

            if (ShowPreviousResponse == true)
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
                int MinLength = 1;

                //  Verify that the type of control triggering this event is a text box
                if ((sender.GetType().Name == "TextBox"))
                {


                    foreach (var minLength in minLengths)
                    {
                        if (minLength.Item1 == QuestionInfoList[currentQuestion].fieldName)
                        {
                            MinLength = minLength.Item2;
                        }
                    }



                    //switch (QuestionInfoList[currentQuestion].fieldName)
                    //{
                    //    case "intid":
                    //        MinLength = 2;
                    //        break;
                    //}
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

                // Allow numbers only
                if (QuestionInfoList[currentQuestion].fieldType == "integer")
                {
                    if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                        MessageBox.Show("Only numbers are allowed!");
                        return;
                    }
                }

                // Allow numbers and decimal place
                // Ascii code for '.' is 46
                if (QuestionInfoList[currentQuestion].fieldType == "decimal")
                {
                    if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != 46)
                    {
                        e.Handled = true;
                        MessageBox.Show("Only numbers are allowed!");
                        return;
                    }
                }
            }
        }



        private void AddAutomatic()
        {
            switch (QuestionInfoList[currentQuestion].fieldName)
            {
                // Software Version
                case "swver":
                    currentAutoValue = ConfigurationManager.AppSettings["swver"];
                    break;

                // Start time of interview
                case "starttime":


                    // To Do - create GetValue() and all other helper functions
                    currentAutoValue = "01/01/1899 00:00:00";
                    //currentAutoValue = GetValue("starttime");
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

            // Automatically click the 'Next' button
            NextButton_Click(null, null);
        }


 





        // This function stores the current question's response
        // into the QuestionInfoList.
        private void SaveDataToList()
        {
            // Used for checkboxes and radio buttons to determine if 
            // 'none' were selected, which means a 'Special' button was selected
            Boolean anyValueSelected = false;

            // Values to be saved to the QuestionInfoList Text and Value variables
            string surveyResponseText = "";
            string surveyResponseValue = "-9";


            // If it is an automatic question, the current value is stored in the 'currentAutoValue' variable
            if (QuestionInfoList[currentQuestion].quesType == "automatic")
            {
                QuestionInfoList[currentQuestion].response = currentAutoValue;
                QuestionInfoList[currentQuestion].value = currentAutoValue;
                QuestionInfoList[currentQuestion].hasBeenAnswered = true;
            }
            else
            {
                // Loop through the controls on the panel
                // There can only be one type of control at a time on the panel
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
                if (anyValueSelected == false)
                {
                    surveyResponseText = QuestionInfoList[currentQuestion].response;
                    surveyResponseValue = QuestionInfoList[currentQuestion].value;
                }


                QuestionInfoList[currentQuestion].response = surveyResponseText;
                QuestionInfoList[currentQuestion].value = surveyResponseValue;
                QuestionInfoList[currentQuestion].hasBeenAnswered = true;
            }
        }





        private void SaveData()
        {
            MessageBox.Show("Done");
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







    }
}
