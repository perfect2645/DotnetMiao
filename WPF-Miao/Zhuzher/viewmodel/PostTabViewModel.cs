using CommunityToolkit.Mvvm.Input;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using System;
using System.Windows.Input;
using Zhuzher.vote;

namespace Zhuzher.viewmodel
{
    internal partial class ZhuzherViewModel
    {
        public ICommand CommentCommand { get; set; }
        public ICommand LikeCommand { get; set; }

        private void InitPostComments()
        {
            CommentCommand = new RelayCommand(ExecutePostComment);
            LikeCommand = new RelayCommand(ExecutePostLike);
        }

        private void ExecutePostComment()
        {
            try
            {
                var voteController = HttpServiceController.GetService<VoteController>();
                voteController.VoteAsync();
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

        private void ExecutePostLike()
        {
            try
            {
                var voteController = HttpServiceController.GetService<VoteController>();
                voteController.VoteAsync();
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
