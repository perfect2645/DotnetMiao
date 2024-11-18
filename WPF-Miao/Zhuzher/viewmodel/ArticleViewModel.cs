using Base.Events;
using Base.viewModel;
using CommunityToolkit.Mvvm.Input;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Utils;
using Zhuzher.Common;
using Zhuzher.Post;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.viewmodel
{
    public class ArticleViewModel : NotifyChanged
    {

        #region Properties

        public Action<Exception> LogExceptionAction { get; set; }

        private List<UserProject> _sourceUserList;
        public List<UserProject> SourceUserList
        {
            get { return _sourceUserList; }
            set
            {
                _sourceUserList = value;
                NotifyUI(() => SourceUserList);
            }
        }

        private UserProject _selectedSourceUser;
        public UserProject SelectedSourceUser
        {
            get { return _selectedSourceUser; }
            set
            {
                _selectedSourceUser = value;
                NotifyUI(() => SelectedSourceUser);
            }
        }

        private List<UserProject> _commentUserList;
        public List<UserProject> CommentUserList
        {
            get { return _commentUserList; }
            set
            {
                _commentUserList = value;
                NotifyUI(() => CommentUserList);
            }
        }

        private UserProject _selectedCommentUser;
        public UserProject SelectedCommentUser
        {
            get { return _selectedCommentUser; }
            set
            {
                _selectedCommentUser = value;
                NotifyUI(() => SelectedCommentUser);
            }
        }

        private int _articleId = 24;
        public int ArticleId
        {
            get { return _articleId; }
            set
            {
                _articleId = value;
                NotifyUI(() => ArticleId);
            }
        }

        private ObservableCollection<Article> _articleList;
        public ObservableCollection<Article> ArticleList
        {
            get { return _articleList; }
            set 
            {
                _articleList = value;
                NotifyUI(() => ArticleList);
            }
        }

        private Article _selectedArticle;
        public Article SelectedArticle
        {
            get { return _selectedArticle; }
            set { 
                _selectedArticle = value;
                NotifyUI(() => SelectedArticle);
                if (value != null)
                {
                    OnSelectedArticleChanged();
                }
            }
        }

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

        public ICommand SearchCommand { get; set; }
        public ICommand CommentCommand { get; set; }
        public ICommand LikeCommand { get; set; }
        public ICommand CommentV2Command { get; set; }
        public ICommand LikeV2Command { get; set; }

        #endregion Properties

        public ArticleViewModel()
        {
            SourceUserList = MainSession.UserProjectList.UserProjects.ToList();
            SelectedSourceUser = SourceUserList.FirstOrDefault();
            CommentUserList = MainSession.UserProjectList.UserProjects.ToList();
            SearchCommand = new RelayCommand(SearchArticle);
            CommentCommand = new RelayCommand(ExecutePostComment);
            LikeCommand = new RelayCommand(ExecutePostLike);
            CommentV2Command = new RelayCommand(ExecutePostCommentV2);
            LikeV2Command = new RelayCommand(ExecutePostLikeV2);
        }

        #region Article Action

        private void SearchArticle()
        {
            try
            {
                var searchController = HttpServiceController.GetService<PostArticleController>();
                searchController.GetArticleAsync(SelectedSourceUser, ArticleId);
            }
            catch (HttpException ex)
            {
                LogExceptionAction.Invoke(ex);
            }
            catch (Exception ex)
            {
                LogExceptionAction.Invoke(ex);
            }
        }

        public void UpdateArticle(List<Article> articles)
        {
            ArticleList = new ObservableCollection<Article>(articles);
        }

        private void OnSelectedArticleChanged()
        {
            var selectedId = SelectedArticle?.Id;
            var articleTitle = SelectedArticle?.Title;
            MainSession.PrintLogEvent.Publish(this, $"选中Id:{selectedId}:{articleTitle}");

            PostId = selectedId.ToString();
        }

        #endregion Article Action

        #region Comment Action

        private void ExecutePostComment()
        {
            try
            {
                var commentController = HttpServiceController.GetService<CommentController>();
                commentController.CommentAsync(PostId, Comment, ApiVersion.V1);
            }
            catch (HttpException ex)
            {
                LogExceptionAction(ex);
            }
            catch (Exception ex)
            {
                LogExceptionAction(ex);
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
                LogExceptionAction(ex);
            }
            catch (Exception ex)
            {
                LogExceptionAction(ex);
            }
        }

        private void ExecutePostCommentV2()
        {
            try
            {
                var commentController = HttpServiceController.GetService<CommentController>();
                commentController.CommentAsync(PostId, Comment, SelectedCommentUser, ApiVersion.V2);
            }
            catch (HttpException ex)
            {
                LogExceptionAction(ex);
            }
            catch (Exception ex)
            {
                LogExceptionAction(ex);
            }
        }

        private void ExecutePostLikeV2()
        {
            try
            {
                var likeController = HttpServiceController.GetService<LikeController>();
                likeController.LikeAsync(PostId, SelectedCommentUser, ApiVersion.V2);
            }
            catch (HttpException ex)
            {
                LogExceptionAction(ex);
            }
            catch (Exception ex)
            {
                LogExceptionAction(ex);
            }
        }

        #endregion Comment Action
    }
}
