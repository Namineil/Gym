package com.example.gym.adapter

import User
import android.annotation.SuppressLint
import android.graphics.BitmapFactory
import android.graphics.drawable.BitmapDrawable
import android.graphics.drawable.Drawable
import android.util.Base64
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AlertDialog
import androidx.core.content.res.ResourcesCompat
import androidx.recyclerview.widget.RecyclerView
import com.example.gym.R
import com.example.gym.fragment.MyNotesFragment
import com.example.gym.models.ClassRecords
import com.example.gym.models.ScheduleTraining
import com.example.gym.models.save.SaveClassRecords
import com.example.gym.network.ApiClient
import com.example.gym.utils.Constants
import com.example.gym.utils.Constants.KEY_CLIENT_ID
import com.google.android.material.snackbar.Snackbar
import com.google.gson.GsonBuilder
import de.hdodenhof.circleimageview.CircleImageView
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.time.LocalDate
import java.time.LocalTime
import java.time.format.DateTimeFormatter

class MyNotesAdapter (private val myNotesFragment: MyNotesFragment, private var myNotesDataset: MutableList<ClassRecords?>
) : RecyclerView.Adapter<MyNotesAdapter.ViewHolder>() {
    private val apiClient = ApiClient.authApi
    private var scheduleTraining : ScheduleTraining? = null

    class ViewHolder(val item: View) : RecyclerView.ViewHolder(item)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val itemView = LayoutInflater
            .from(parent.context)
            .inflate(R.layout.notes_card, parent, false)
        return ViewHolder(itemView)
    }

    @SuppressLint("NewApi", "SetTextI18n")
    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val userJson = myNotesFragment.activity?.intent?.getStringExtra("User")
        val gson = GsonBuilder().create()
        var user = gson.fromJson(userJson, User::class.java)

        ApiClient.setUserToken(user.token)
        getScheduleTraining(myNotesDataset[position]!!.idTraining, holder, position)

    }

    private fun getScheduleTraining(id: Int, holder: ViewHolder, position: Int) {
        val call = apiClient.getScheduleTrainingById(id)
        call.enqueue(
            object : Callback<ScheduleTraining> {
                override fun onResponse(
                    call: Call<ScheduleTraining>,
                    response: Response<ScheduleTraining>
                ) {
                    if (response.body() != null) {
                        scheduleTraining = response.body()!!
                        getCardView(holder, position)
                    } else
                        Toast.makeText(myNotesFragment.requireContext(), response.body()?.toString(), Snackbar.LENGTH_LONG).show()
                }

                override fun onFailure(
                    call: Call<ScheduleTraining>,
                    t: Throwable) {

                    Toast.makeText(myNotesFragment.requireContext(), t.message, Toast.LENGTH_LONG).show()
                }
            })
    }

    @SuppressLint("NewApi", "SetTextI18n")
    private fun getCardView(holder : ViewHolder, position: Int) {
        if (scheduleTraining != null) {

            val trainingDateFrom = LocalTime.parse(
                scheduleTraining!!.trainingDateFrom,
                DateTimeFormatter.ISO_ZONED_DATE_TIME
            )
            val trainingDate = LocalDate.parse(
                scheduleTraining!!.trainingDateFrom,
                DateTimeFormatter.ISO_ZONED_DATE_TIME
            )
            val trainingDateTo = LocalTime.parse(
                scheduleTraining!!.trainingDateTo,
                DateTimeFormatter.ISO_ZONED_DATE_TIME
            )

            val imageBytes = Base64.decode(scheduleTraining!!.trainerUserImage, Base64.DEFAULT)
            val decodedImage = BitmapFactory.decodeByteArray(imageBytes, 0, imageBytes.size)

            holder.item.findViewById<CircleImageView>(R.id.iv_trainer).setImageDrawable(
                BitmapDrawable(myNotesFragment.resources, decodedImage)
            )
            holder.item.findViewById<TextView>(R.id.tv_schedule_training_trainer).text =
                scheduleTraining!!.trainerUserFullName
            holder.item.findViewById<TextView>(R.id.tv_schedule_training_date_time).text =
                "c $trainingDateFrom по $trainingDateTo"
            if (trainingDate < LocalDate.now()) {
                holder.item.findViewById<TextView>(R.id.btn_delete).setOnClickListener {
                    val dialog: AlertDialog =
                        AlertDialog.Builder(myNotesFragment.requireContext())
                            .setTitle("Удаление")
                            .setMessage("Запись удалить невозможно!")
                            .setPositiveButton("Ок", null)
                            .show()
                    val positiveButton: Button =
                        dialog.getButton(AlertDialog.BUTTON_POSITIVE);
                    positiveButton.setOnClickListener {
                        dialog.dismiss()
                    }
                }
            } else {
                holder.item.findViewById<TextView>(R.id.btn_delete).setOnClickListener {
                    val call =
                        apiClient.deleteClassRecords(myNotesDataset[position]!!.idClassRecords)
                    call.enqueue(
                        object : Callback<ClassRecords> {
                            @SuppressLint("NotifyDataSetChanged")
                            override fun onResponse(
                                call: Call<ClassRecords>,
                                response: Response<ClassRecords>
                            ) {
                                if (response.body() != null) {
                                    var classRecords = response.body()!!
                                    val dialog: AlertDialog =
                                        AlertDialog.Builder(myNotesFragment.requireContext())
                                            .setTitle("Удаление")
                                            .setMessage("Запись успешно отменена")
                                            .setPositiveButton("Ок", null)
                                            .show()
                                    myNotesDataset.removeAt(position)
                                    notifyDataSetChanged()
                                    val positiveButton: Button =
                                        dialog.getButton(AlertDialog.BUTTON_POSITIVE);
                                    positiveButton.setOnClickListener {
                                        dialog.dismiss()
                                    }
                                } else
                                    Toast.makeText(
                                        myNotesFragment.activity,
                                        response.body()?.toString(),
                                        Snackbar.LENGTH_LONG
                                    ).show()
                            }

                            override fun onFailure(
                                call: Call<ClassRecords>,
                                t: Throwable
                            ) {

                                Toast.makeText(
                                    myNotesFragment.activity,
                                    t.message,
                                    Toast.LENGTH_LONG
                                )
                                    .show()
                            }
                        })
                }
            }
        } else {
            val dialog: AlertDialog =
                AlertDialog.Builder(myNotesFragment.requireContext())
                    .setTitle("Удаление")
                    .setMessage("Запись не удалена! Попробуйте еще раз!")
                    .setPositiveButton("Ок", null)
                    .show()
            val positiveButton: Button =
                dialog.getButton(AlertDialog.BUTTON_POSITIVE);
            positiveButton.setOnClickListener {
                dialog.dismiss()
            }
        }
    }

    override fun getItemCount() = myNotesDataset.size
}