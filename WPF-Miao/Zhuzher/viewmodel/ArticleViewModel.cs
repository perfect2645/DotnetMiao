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

        public ICommand SearchCommand { get; set; }

        #endregion Properties

        public ArticleViewModel()
        {
            SourceUserList = MainSession.UserProjectList.UserProjects.ToList();
            SelectedSourceUser = SourceUserList.FirstOrDefault();
            CommentUserList = MainSession.UserProjectList.UserProjects.ToList();
            SearchCommand = new RelayCommand(SearchArticle);
        }

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
    }
}
