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


        public ArticleViewModel ArticleViewModel { get; set; }

        #endregion Properties

        private void InitPostComments()
        {
            MainSession.UpdateUiEvent = UpdateUiEvent;



            ArticleViewModel = new ArticleViewModel();
            ArticleViewModel.LogExceptionAction = new Action<Exception>(Log);
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
