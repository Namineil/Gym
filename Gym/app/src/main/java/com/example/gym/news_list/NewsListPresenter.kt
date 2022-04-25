package com.example.gym.news_list

import com.example.gym.models.News

class NewsListPresenter (private var view : NewsListContract.View?) : NewsListContract.Presenter,
    NewsListContract.Model.OnFinishedListener {
    private val newsListModel : NewsListContract.Model

    override fun requestNewsFromServer() {
        newsListModel.getNewsList(this, )
    }

    override fun getMoreData(last: Int) {
        newsListModel.getNewsList(this, last)
    }

    override fun onDestroy() {
        view = null;
    }

    init {
        newsListModel = NewsListModel()
    }

    override fun onNewsArrived(news: List<News>?) {
        view?.let {
            it.setNewsInRecycler(news)
        }
    }

    override fun onError(t: Throwable) {
        view?.let {
            it.setError(t)
        }
    }
}