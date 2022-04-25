package com.example.gym.news_details

import com.example.gym.activity.HomeActivity
import com.example.gym.models.News

class NewsDetailsPresenter(private var newsDetailsView: NewsDetailsContract.View?) : NewsDetailsContract.Presenter, NewsDetailsContract.Model.OnFinishedListener {

    private val newsDetailsModel = NewsDetailsModel()

    override fun requestNewsDetails(newsId: Int) {
        //newsDetailsView?.showProgress()
        newsDetailsModel.getNewsDetails(this, newsId)
    }

    override fun onDestroy() {
        newsDetailsView = null
    }

    override fun onNewsArrived(news: News) {
        newsDetailsView?.let {
            //it.hideProgress()
            it.setDataInViews(news)
        }
    }

    override fun onError(t: Throwable) {
        newsDetailsView?.let {
            //it.hideProgress()
            it.setError(t)
        }
    }
}