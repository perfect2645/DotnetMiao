using System.Collections.Generic;
using Zhuzher.Common;
using Zhuzher.search;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;

namespace Zhuzher.Score
{
    internal class SurveyAnswerContent : OnewoContent
    {
        public const string Url = "https://z.onewo.com/survey/api/answer/save";
        public SurveyAnswerContent(UserProject user) : base(Url, user)
        {
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddHeader("Zhuzher-Project-Code", User.ProjectCode);
            AddHeader("Zhuzher-Project-Role", "1");
        }

        private void BuildContent()
        {
            AddContent("surveyId", 1);
            AddContent("answerDetailList", BuildAnswerDetails());
        }

        private List<SurveyAnswer> BuildAnswerDetails()
        {
            var answerDetails = new List<SurveyAnswer>();
            answerDetails.Add(new SurveyAnswer("0", "1"));
            answerDetails.Add(new SurveyAnswer("1", "1"));
            answerDetails.Add(new SurveyAnswer("2", "1"));
            answerDetails.Add(new SurveyAnswer("3", "1"));
            answerDetails.Add(new SurveyAnswer("4", "1"));
            answerDetails.Add(new SurveyAnswer("5", "1"));
            answerDetails.Add(new SurveyAnswer("6", "1"));

            return answerDetails;
        }
    }

    internal class SurveyAnswer
    {
        [JsonPropertyName("questionId")]
        public string QuestionId { get; set; }
        [JsonPropertyName("questionAnswer")]
        public string QuestionAnswer { get; set; }


        public SurveyAnswer(string id, string answer) 
        {
            QuestionId = id;
            QuestionAnswer = answer;
        }
    }
}