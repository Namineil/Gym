package com.example.gym.scheduleTraining_list

import com.example.gym.models.ScheduleTraining
import com.example.gym.network.ApiClient
import com.example.gym.network.ApiInterface
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class ScheduleTrainingListModel : ScheduleTrainingListContract.Model {
    override fun getScheduleTrainingList(
        onFinishedListener: ScheduleTrainingListContract.Model.OnFinishedListener,
        specializationId: Int,
    ) {
        val apiInterface : ApiInterface = ApiClient.authApi
        val call : Call<List<ScheduleTraining>> = apiInterface.getScheduleTraining(specializationId)

        call.enqueue(object: Callback<List<ScheduleTraining>> {
            override fun onResponse(
                call: Call<List<ScheduleTraining>>,
                response: Response<List<ScheduleTraining>>
            ) {
                val scheduleTraining = response.body()
                onFinishedListener.onScheduleTrainingArrived(scheduleTraining)
            }

            override fun onFailure(call: Call<List<ScheduleTraining>>, t: Throwable) {
                onFinishedListener.onError(t)
            }
        })
    }
}