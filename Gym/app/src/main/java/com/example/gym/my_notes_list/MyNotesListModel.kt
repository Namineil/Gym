package com.example.gym.my_notes_list

import com.example.gym.models.ClassRecords
import com.example.gym.network.ApiClient
import com.example.gym.network.ApiInterface
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class MyNotesListModel : MyNotesListContract.Model {
    override fun getMyNotesList(
        onFinishedListener: MyNotesListContract.Model.OnFinishedListener,
        idClient: Int,
    ) {
        val apiInterface : ApiInterface = ApiClient.authApi
        val call : Call<List<ClassRecords>> = apiInterface.getMyNotes(idClient)

        call.enqueue(object: Callback<List<ClassRecords>> {
            override fun onResponse(
                call: Call<List<ClassRecords>>,
                response: Response<List<ClassRecords>>
            ) {
                val myNotes = response.body()
                onFinishedListener.onMyNotesArrived(myNotes)
            }

            override fun onFailure(call: Call<List<ClassRecords>>, t: Throwable) {
                onFinishedListener.onError(t)
            }
        })
    }
}