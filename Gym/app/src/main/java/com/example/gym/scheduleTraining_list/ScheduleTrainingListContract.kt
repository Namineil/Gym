package com.example.gym.scheduleTraining_list

import com.example.gym.models.ScheduleTraining

interface ScheduleTrainingListContract {
    interface Model {
        interface OnFinishedListener {
            fun onScheduleTrainingArrived(scheduleTraining : List<ScheduleTraining>?)
            fun onError(t : Throwable)
        }

        fun getScheduleTrainingList(onFinishedListener: OnFinishedListener, specializationId: Int)


    }

    interface Presenter {
        fun requestScheduleTrainingFromServer(specializationId: Int)
        fun onDestroy()
    }

    interface View {
        fun showProgress()
        fun hideProgress()
        fun setScheduleTrainingInRecycler(scheduleTraining: List<ScheduleTraining>?)
        fun setError(error: Throwable)
    }
}