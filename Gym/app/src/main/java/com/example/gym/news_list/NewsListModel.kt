package com.example.gym.news_list

import com.example.gym.models.News
import com.example.gym.network.ApiClient
import com.example.gym.network.ApiInterface
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class NewsListModel : NewsListContract.Model {
    override fun getNewsList(
        onFinishedListener: NewsListContract.Model.OnFinishedListener,
        last: Int
    ) {
        val apiInterface : ApiInterface = ApiClient.authApi
        val call : Call<List<News>> = apiInterface.getLastNews(last)

        call.enqueue(object: Callback<List<News>> {
            override fun onResponse(
                call: Call<List<News>>,
                response: Response<List<News>>
            ) {
                val news = response.body()
                onFinishedListener.onNewsArrived(news)
            }

            override fun onFailure(call: Call<List<News>>, t: Throwable) {
                onFinishedListener.onError(t)
            }
        })
    }
}