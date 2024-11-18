using Base.Events;
using CommunityToolkit.Mvvm.Input;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Input;
using Zhuzher.Common;
using Zhuzher.Post;
using Zhuzher.search;
using Zhuzher.session;
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

        public ArticleViewModel ArticleViewModel { get; set; }

        private string _postId = "79215";
        public string PostId
        {
            get { return _postId; }
            set 
            { 
                _postId = value;
                NotifyUI(() => PostId);
            }
        }

        private string _comment = "实用👍🏻";
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
            MainSession.UpdateUiEvent = UpdateUiEvent;

            CommentCommand = new RelayCommand(ExecutePostComment);
            LikeCommand = new RelayCommand(ExecutePostLike);
            CommentV2Command = new RelayCommand(ExecutePostCommentV2);
            LikeV2Command = new RelayCommand(ExecutePostLikeV2);

            ArticleViewModel = new ArticleViewModel();
            ArticleViewModel.LogExceptionAction = new Action<Exception>(Log);
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


        #region Update UI

        protected override void UpdateUI(UiEventArgs e)
        {
            var articleList = e.Value as List<Article>;
            ArticleViewModel.UpdateArticle(articleList);
        }

        #endregion  Update UI
    }
}
