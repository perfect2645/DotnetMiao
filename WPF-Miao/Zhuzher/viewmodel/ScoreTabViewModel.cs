using CommunityToolkit.Mvvm.Input;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Zhuzher.Play;
using Zhuzher.Score;
using Zhuzher.session;

namespace Zhuzher.viewmodel
{
    internal partial class ZhuzherViewModel
    {
        #region Properties
        public ICommand TotalScoreCommand { get; set; }
        public ICommand SurveyCommand { get; set; }


        #endregion Properties

        private void InitScoreTab()
        {
            TotalScoreCommand = new RelayCommand(ExecuteTotalScore);
            SurveyCommand = new RelayCommand(ExecuteSurvey);
        }

        private void ExecuteSurvey()
        {
            var surveyController = HttpServiceController.GetService<SurveyAnswerController>();
            surveyController.SurveyAnswerAsync();
        }

        private void ExecuteTotalScore()
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    var socreController = HttpServiceController.GetService<TotalScoreController>();
                    foreach (var user in MainSession.UserProjectList.UserProjects)
                    {
                        socreController.GetTotalScore(user);
                        Thread.Sleep(200);
                    }
                });
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }
    }
}
