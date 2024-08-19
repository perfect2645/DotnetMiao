using CommunityToolkit.Mvvm.Input;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using System;
using System.Windows.Input;
using Zhuzher.Common;
using Zhuzher.Post;
using Zhuzher.vote;

namespace Zhuzher.viewmodel
{
    internal partial class ZhuzherViewModel
    {
        #region Properties
        public ICommand CommentCommand { get; set; }
        public ICommand LikeCommand { get; set; }
        public ICommand CommentV2Command { get; set; }
        public ICommand LikeV2Command { get; set; }
        
        private string _postId= "66097";
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
            CommentV2Command = new RelayCommand(ExecutePostCommentV2);
            LikeV2Command = new RelayCommand(ExecutePostLikeV2);
        }

        private void ExecutePostComment()
        {
            try
            {
                var commentController = HttpServiceController.GetService<CommentController>();
                commentController.CommentAsync(PostId, Comment, ApiVersion.V1);
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
                likeController.LikeAsync(PostId, ApiVersion.V1);
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

        private void ExecutePostCommentV2()
        {
            try
            {
                var commentController = HttpServiceController.GetService<CommentController>();
                commentController.CommentAsync(PostId, Comment, ApiVersion.V2);
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

        private void ExecutePostLikeV2()
        {
            try
            {
                var likeController = HttpServiceController.GetService<LikeController>();
                likeController.LikeAsync(PostId, ApiVersion.V2);
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
