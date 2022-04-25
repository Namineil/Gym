package com.example.gym.adapter

import User
import android.annotation.SuppressLint
import android.graphics.BitmapFactory
import android.graphics.drawable.BitmapDrawable
import android.os.Build
import android.util.Base64
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.annotation.RequiresApi
import androidx.appcompat.app.AlertDialog
import androidx.recyclerview.widget.RecyclerView
import com.example.gym.R
import com.example.gym.fragment.CalendarFragment
import com.example.gym.models.CardSalesHistory
import com.example.gym.models.ClassRecords
import com.example.gym.models.ScheduleTraining
import com.example.gym.models.save.SaveClassRecords
import com.example.gym.network.ApiClient
import com.example.gym.utils.Constants.CONTENT_TYPE
import com.example.gym.utils.Constants.KEY_CLIENT_ID
import com.google.android.material.snackbar.Snackbar
import com.google.gson.GsonBuilder
import de.hdodenhof.circleimageview.CircleImageView
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.time.LocalDate
import java.time.LocalDateTime
import java.time.LocalTime
import java.time.format.DateTimeFormatter
import java.util.*

class ScheduleTrainingAdapter(private val scheduleTrainingFragment: CalendarFragment, private var scheduleTrainingDataset: List<ScheduleTraining?>
) : RecyclerView.Adapter<ScheduleTrainingAdapter.ViewHolder>() {
    private val apiClient = ApiClient.authApi

    class ViewHolder(val item: View) : RecyclerView.ViewHolder(item)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val itemView = LayoutInflater
            .from(parent.context)
            .inflate(R.layout.schedule_training_card, parent, false)
        return ViewHolder(itemView)
    }

    @RequiresApi(Build.VERSION_CODES.O)
    @SuppressLint("SetTextI18n")
    override fun onBindViewHolder(holder: ViewHolder, @SuppressLint("RecyclerView") position: Int) {
        val userJson = scheduleTrainingFragment.activity?.intent?.getStringExtra("User");
        val gson = GsonBuilder().create()
        var user = gson.fromJson(userJson, User::class.java);

        ApiClient.setUserToken(user.token);

        val imageBytes = Base64.decode(scheduleTrainingDataset[position]!!.trainerUserImage, Base64.DEFAULT)
        val decodedImage = BitmapFactory.decodeByteArray(imageBytes, 0, imageBytes.size)

        val trainingDateFrom = LocalTime.parse(scheduleTrainingDataset[position]?.trainingDateFrom, DateTimeFormatter.ISO_ZONED_DATE_TIME)
        val trainingDateTo = LocalTime.parse(scheduleTrainingDataset[position]?.trainingDateTo, DateTimeFormatter.ISO_ZONED_DATE_TIME)

        holder.item.findViewById<CircleImageView>(R.id.iv_trainer).setImageDrawable(BitmapDrawable(scheduleTrainingFragment.resources, decodedImage))
        holder.item.findViewById<TextView>(R.id.tv_schedule_training_trainer).text = scheduleTrainingDataset[position]?.trainerUserFullName
        holder.item.findViewById<TextView>(R.id.tv_schedule_training_date_time).text = "c $trainingDateFrom по $trainingDateTo"

        holder.item.findViewById<TextView>(R.id.btn_registration).setOnClickListener{
            var saveClassRecords = SaveClassRecords(scheduleTrainingDataset[position]!!.idTraining, KEY_CLIENT_ID, null)
            val call = apiClient.addClassRecords(CONTENT_TYPE, saveClassRecords)
            call.enqueue(
                object : Callback<ClassRecords> {
                    override fun onResponse(
                        call: Call<ClassRecords>,
                        response: Response<ClassRecords>
                    ) {
                        if (response.body() != null) {
                            var classRecords = response.body()!!
                            val dialog: AlertDialog = AlertDialog.Builder(scheduleTrainingFragment.requireContext())
                                .setTitle("Вы успешно записаны")
                                .setMessage("Вы записаны на " + scheduleTrainingDataset[position]!!.specializationName
                                                                + ", к тренеру "
                                                                + scheduleTrainingDataset[position]!!.trainerUserFullName
                                                                + " на "
                                                                + LocalTime.parse(scheduleTrainingDataset[position]!!.trainingDateFrom, DateTimeFormatter.ISO_ZONED_DATE_TIME)
                                                                + "\n\nУдачной тренировки!")
                                .setPositiveButton("Ок", null)
                                .show();
                            val positiveButton: Button = dialog.getButton(AlertDialog.BUTTON_POSITIVE);
                            positiveButton.setOnClickListener {
                                dialog.dismiss()
                            }
                        } else
                            Toast.makeText(scheduleTrainingFragment.activity, response.body()?.toString(), Snackbar.LENGTH_LONG).show()
                    }

                    override fun onFailure(
                        call: Call<ClassRecords>,
                        t: Throwable) {

                        Toast.makeText(scheduleTrainingFragment.activity, t.message, Toast.LENGTH_LONG).show()
                    }
                })
        }

    }

    override fun getItemCount() = scheduleTrainingDataset.size

}