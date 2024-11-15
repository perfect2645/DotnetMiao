using Base.viewModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Utils;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.viewmodel
{
    public class ArticleViewModel : NotifyChanged
    {

        #region Properties

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

        public ICommand SearchCommand { get; set; }

        #endregion Properties

        public ArticleViewModel()
        {
            SourceUserList = MainSession.UserProjectList.UserProjects.ToList();
            CommentUserList = MainSession.UserProjectList.UserProjects.ToList();
            SearchCommand = new RelayCommand(SearchArticle);
        }

        private void SearchArticle()
        {

        }
    }
}
