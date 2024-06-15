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
       

        #endregion Properties

        private void InitActivityComments()
        {
            ActivityChanceCommand = new RelayCommand(ExecuteActivityChance);
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
    }
}
