using CommunityToolkit.Mvvm.Input;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using System;
using System.Windows.Input;
using Zhuzher.Post;
using Zhuzher.vote;

namespace Zhuzher.viewmodel
{
    internal partial class ZhuzherViewModel
    {
        #region Properties
        public ICommand CommentCommand { get; set; }
        public ICommand LikeCommand { get; set; }


        private string _postId= "9709102";
        public string PostId
        {
            get { return _postId; }
            set 
            { 
                _postId = value;
                NotifyUI(() => PostId);
            }
        }

        private string _comment = "恬恬又出动了";
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                NotifyUI(() => Comment);
            }
        }

        #endregion Properties

        private void InitPostComments()
        {
            CommentCommand = new RelayCommand(ExecutePostComment);
            LikeCommand = new RelayCommand(ExecutePostLike);
        }

        private void ExecutePostComment()
        {
            try
            {
                var commentController = HttpServiceController.GetService<CommentController>();
                commentController.CommentAsync(PostId, Comment);
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
                var likeController = HttpServiceController.GetService<LikeController>();
                likeController.LikeAsync(PostId);
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
