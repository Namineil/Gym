package com.example.gym.card_details

import com.example.gym.models.Card

interface CardDetailsContract {

    interface Model {

        interface OnFinishedListener {
            fun onCardArrived(card : Card)
            fun onError(t : Throwable)
        }

        fun getCardDetails(onFinishedListener: OnFinishedListener, cardId : Int)

    }

    interface Presenter {
        fun requestCardDetails(cardId : Int)
        fun onDestroy()

    }

    interface View {

        fun setDataInViews(card: Card)
        fun setError(t: Throwable)
    }

}