using CommunityToolkit.Mvvm.Input;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using System;
using System.Windows.Input;
using Zhuzher.Common;
using Zhuzher.Play;
using Zhuzher.Post;
using Zhuzher.vote;

namespace Zhuzher.viewmodel
{
    internal partial class ZhuzherViewModel
    {
        #region Properties
        public ICommand ActivityChanceCommand { get; set; }
        public ICommand GuessResultCommand { get; set; }


        #endregion Properties

        private void InitActivityComments()
        {
            ActivityChanceCommand = new RelayCommand(ExecuteActivityChance);
            GuessResultCommand = new RelayCommand(ExecuteGuessResult);
        }

        private void ExecuteActivityChance()
        {
            try
            {
                var chanceController = HttpServiceController.GetService<PlayChanceController>();
                chanceController.GetChanceAsync();
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

        private void ExecuteGuessResult()
        {
            try
            {
                var myGuessController = HttpServiceController.GetService<MyGuessController>();
                myGuessController.GetGuessResultAsync(214);
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
