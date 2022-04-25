package com.example.gym.my_notes_list

import com.example.gym.models.ClassRecords

class MyNotesListPresenter (private var view : MyNotesListContract.View?) : MyNotesListContract.Presenter,
    MyNotesListContract.Model.OnFinishedListener {
    private val myNotesListModel : MyNotesListContract.Model

    override fun requestMyNotesFromServer(idClient: Int) {
        view?.showProgress()
        myNotesListModel.getMyNotesList(this, idClient)
    }

    override fun onDestroy() {
        view = null;
    }

    init {
        myNotesListModel = MyNotesListModel()
    }

    override fun onMyNotesArrived(myNotes: List<ClassRecords>?) {
        view?.let {
            it.setMyNotesInRecycler(myNotes)
            it.hideProgress()
        }
    }

    override fun onError(t: Throwable) {
        view?.let {
            it.setError(t)
        }
    }
}