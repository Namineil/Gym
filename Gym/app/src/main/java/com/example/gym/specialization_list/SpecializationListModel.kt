package com.example.gym.specialization_list

import com.example.gym.models.Specialization
import com.example.gym.network.ApiClient
import com.example.gym.network.ApiInterface
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class SpecializationListModel : SpecializationListContract.Model {
    override fun getSpecializationList(
        onFinishedListener: SpecializationListContract.Model.OnFinishedListener
    ) {
        val apiInterface : ApiInterface = ApiClient.authApi
        val call : Call<List<Specialization>> = apiInterface.getSpecialization()

        call.enqueue(object: Callback<List<Specialization>> {
            override fun onResponse(
                call: Call<List<Specialization>>,
                response: Response<List<Specialization>>
            ) {
                val specialization = response.body()
                onFinishedListener.onSpecializationArrived(specialization)
            }

            override fun onFailure(call: Call<List<Specialization>>, t: Throwable) {
                onFinishedListener.onError(t)
            }
        })
    }
}