using System;

namespace gist
{
    public class PublicVars
    {

        // Unique identifier position in text file string
        // Will use 3:
        // 1 - software version
        // 2 - start time
        // 3 - subjid (unique identifier)
        public static int subjidPos = 2;




        // Name of the survey.
        // Must be the same name as the XML file.
        static string _survey;
        public static string survey
        {
            get
            {
                return _survey;
            }
            set
            {
                _survey = value;
            }
        }


        // Subject ID.
        // Unique identifier for the particular survey (Primary Key)
        static string _subjid;
        public static string subjid
        {
            get
            {
                return _subjid;
            }
            set
            {
                _subjid = value;
            }
        }


        // Keeps track of whether or not the interview is cancelled.
        // Set to true when user clicks the 'Cancel Interview' button.
        public static bool cancelledInterview;


        // Keeps track of whether the survey is being modified or not.
        // Set to true when modify an existing survey.
        // Set to false for new surveys.
        public static bool modifyingSurvey;
    }
}
