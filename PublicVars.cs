using System;

namespace gist
{
    public class PublicVars
    {
        // Name of the survey.
        // Must be the same name as the XML file.
        public string survey;


        // Subject ID.
        // Unique identifier for the particular survey (Primary Key)
        public string subjid;


        // Keeps track of whether or not the interview is cancelled.
        // Set to true when user clicks the 'Cancel Interview' button.
        public Boolean cancelledInterview;


        // Keeps track of whether the survey is being modified or not.
        // Set to true when modify an existing survey.
        // Set to false for new surveys.
        public Boolean modifyingSurvey;
    }
}
