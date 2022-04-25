package com.example.gym.scheduleTraining_list

import com.example.gym.models.ScheduleTraining


class ScheduleTrainingListPresenter (private var view : ScheduleTrainingListContract.View?) : ScheduleTrainingListContract.Presenter,
    ScheduleTrainingListContract.Model.OnFinishedListener {
    private val scheduleTrainingListModel : ScheduleTrainingListContract.Model

    override fun requestScheduleTrainingFromServer(specializationId: Int) {
        view?.showProgress()
        scheduleTrainingListModel.getScheduleTrainingList(this, specializationId)
    }

    override fun onDestroy() {
        view = null;
    }

    init {
        scheduleTrainingListModel = ScheduleTrainingListModel()
    }

    override fun onScheduleTrainingArrived(scheduleTraining: List<ScheduleTraining>?) {
        view?.let {
            it.setScheduleTrainingInRecycler(scheduleTraining)
            it.hideProgress()
        }
    }

    override fun onError(t: Throwable) {
        view?.let {
            it.setError(t)
        }
    }
}