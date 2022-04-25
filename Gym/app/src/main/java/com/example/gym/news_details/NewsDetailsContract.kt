package com.example.gym.news_details

import com.example.gym.models.News

interface NewsDetailsContract {

    interface Model {

        interface OnFinishedListener {
            fun onNewsArrived(news : News)
            fun onError(t : Throwable)
        }

        fun getNewsDetails(onFinishedListener: OnFinishedListener, newsId : Int)

    }

    interface Presenter {
        fun requestNewsDetails(newsId : Int)
        fun onDestroy()

    }

    interface View {

        fun setDataInViews(news: News)
        fun setError(t: Throwable)
    }

}