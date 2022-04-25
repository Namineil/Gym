package com.example.gym.news_list

import com.example.gym.models.News

interface NewsListContract {
    interface Model {
        interface OnFinishedListener {
            fun onNewsArrived(news : List<News>?)
            fun onError(t : Throwable)
        }

        fun getNewsList(onFinishedListener: OnFinishedListener, last : Int = 10)


    }

    interface Presenter {
        fun requestNewsFromServer()
        fun getMoreData(page: Int)
        fun onDestroy()
    }

    interface View {
        fun setNewsInRecycler(news: List<News>?)
        fun setError(error: Throwable)
    }
}