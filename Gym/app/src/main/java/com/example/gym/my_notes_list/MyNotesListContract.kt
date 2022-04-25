package com.example.gym.my_notes_list

import com.example.gym.models.ClassRecords

interface MyNotesListContract {
    interface Model {
        interface OnFinishedListener {
            fun onMyNotesArrived(myNotes : List<ClassRecords>?)
            fun onError(t : Throwable)
        }

        fun getMyNotesList(onFinishedListener: OnFinishedListener, specializationId: Int)


    }

    interface Presenter {
        fun requestMyNotesFromServer(specializationId: Int)
        fun onDestroy()
    }

    interface View {
        fun showProgress()
        fun hideProgress()
        fun setMyNotesInRecycler(myNotes: List<ClassRecords>?)
        fun setError(error: Throwable)
    }
}