package com.example.gym.specialization_list

import com.example.gym.models.Specialization

interface SpecializationListContract {
    interface Model {
        interface OnFinishedListener {
            fun onSpecializationArrived(specialization : List<Specialization>?)
            fun onError(t : Throwable)
        }

        fun getSpecializationList(onFinishedListener: OnFinishedListener)


    }

    interface Presenter {
        fun requestSpecializationFromServer()
        fun onDestroy()
    }

    interface View {
        fun setSpecializationInRecycler(specialization: List<Specialization>?)
        fun setError(error: Throwable)
    }
}