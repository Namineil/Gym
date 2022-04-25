package com.example.gym.card_details

import com.example.gym.models.Card
import com.example.gym.network.ApiClient
import com.example.gym.network.ApiInterface
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.math.BigInteger

class CardDetailsModel : CardDetailsContract.Model {
    override fun getCardDetails(
        onFinishedListener: CardDetailsContract.Model.OnFinishedListener,
        cardId: Int,
    ) {
        val apiInterface : ApiInterface = ApiClient.authApi
        val call : Call<Card> = apiInterface.getCard(cardId)

        call.enqueue(object : Callback<Card> {
            override fun onResponse(call: Call<Card>, response: Response<Card>) {
                val card = response.body()
                if (card != null) {
                    card.period = convertTimestampToMonth(card.period).toBigInteger()
                };
                onFinishedListener.onCardArrived(card!!)
            }

            override fun onFailure(call: Call<Card>, t: Throwable) {
                onFinishedListener.onError(t)
            }

        })
    }

    private fun convertTimestampToMonth(time: BigInteger): Int {
        val t = time.toBigDecimal();
        val a:Long = 1000L * 60 * 60 * 24 * 30;
        val res = (t / a.toBigDecimal()).toDouble();
        return Math.ceil(res).toInt();
    }

}