package com.example.gym.specialization_list

import com.example.gym.models.Specialization

class SpecializationListPresenter (private var view : SpecializationListContract.View?) : SpecializationListContract.Presenter,
    SpecializationListContract.Model.OnFinishedListener {
    private val specializationListModel : SpecializationListContract.Model

    override fun requestSpecializationFromServer() {
        specializationListModel.getSpecializationList(this, )
    }

    override fun onDestroy() {
        view = null;
    }

    init {
        specializationListModel = SpecializationListModel()
    }

    override fun onSpecializationArrived(specialization: List<Specialization>?) {
        view?.let {
            it.setSpecializationInRecycler(specialization)
        }
    }

    override fun onError(t: Throwable) {
        view?.let {
            it.setError(t)
        }
    }
}