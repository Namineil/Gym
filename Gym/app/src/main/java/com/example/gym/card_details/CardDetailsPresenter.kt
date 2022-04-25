package com.example.gym.card_details

import com.example.gym.models.Card

class CardDetailsPresenter(private var cardDetailsView: CardDetailsContract.View?) : CardDetailsContract.Presenter, CardDetailsContract.Model.OnFinishedListener {

    private val cardDetailsModel = CardDetailsModel()

    override fun requestCardDetails(cardId: Int) {
        cardDetailsModel.getCardDetails(this, cardId)
    }

    override fun onDestroy() {
        cardDetailsView = null
    }

    override fun onCardArrived(card: Card) {
        cardDetailsView?.let {
            it.setDataInViews(card)
        }
    }

    override fun onError(t: Throwable) {
        cardDetailsView?.let {
            it.setError(t)
        }
    }
}