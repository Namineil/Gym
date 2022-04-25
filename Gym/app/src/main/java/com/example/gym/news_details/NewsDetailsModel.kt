package com.example.gym.news_details

import com.example.gym.models.News
import com.example.gym.network.ApiClient
import com.example.gym.network.ApiInterface
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.math.BigInteger

class NewsDetailsModel : NewsDetailsContract.Model {
    override fun getNewsDetails(
            onFinishedListener: NewsDetailsContract.Model.OnFinishedListener,
            newsId: Int,
    ) {
        val apiInterface : ApiInterface = ApiClient.authApi
        val call : Call<News> = apiInterface.getNews(newsId)

        call.enqueue(object : Callback<News> {
            override fun onResponse(call: Call<News>, response: Response<News>) {
                val news = response.body()
                onFinishedListener.onNewsArrived(news!!)
            }

            override fun onFailure(call: Call<News>, t: Throwable) {
                onFinishedListener.onError(t)
            }

        })
    }

}