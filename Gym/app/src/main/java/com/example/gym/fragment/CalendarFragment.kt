package com.example.gym.fragment

import User
import android.annotation.SuppressLint
import android.graphics.drawable.Drawable
import android.icu.text.SimpleDateFormat
import android.os.Build
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ProgressBar
import androidx.annotation.RequiresApi
import androidx.core.graphics.toColor
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.applandeo.materialcalendarview.CalendarDay
import com.applandeo.materialcalendarview.CalendarView
import com.applandeo.materialcalendarview.EventDay
import com.applandeo.materialcalendarview.listeners.OnDayClickListener
import com.example.gym.R
import com.example.gym.adapter.ScheduleTrainingAdapter
import com.example.gym.models.ScheduleTraining
import com.example.gym.network.ApiClient
import com.example.gym.scheduleTraining_list.ScheduleTrainingListContract
import com.example.gym.scheduleTraining_list.ScheduleTrainingListPresenter
import com.example.gym.utils.Constants.KEY_SPECIALIZATION_ID
import com.google.android.material.snackbar.Snackbar
import com.google.gson.GsonBuilder
import java.time.LocalDate
import java.time.format.DateTimeFormatter
import java.util.*

class CalendarFragment : Fragment(), ScheduleTrainingListContract.View {
    private val scheduleTrainingListPresenter = ScheduleTrainingListPresenter(this)
    private lateinit var user: User
    private val scheduleTrainingList = mutableListOf<ScheduleTraining?>()
    private val scheduleTrainingListAll = mutableListOf<ScheduleTraining>()
    private var scheduleTrainingAdapter : ScheduleTrainingAdapter? = null
    private lateinit var pb_loading: ProgressBar
    private var eventsNotes = arrayListOf<CalendarDay>()
    private lateinit var calendarView: CalendarView

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        val userJson = activity?.intent?.getStringExtra("User")
        val gson = GsonBuilder().create()
        user = gson.fromJson(userJson, User::class.java)

        ApiClient.setUserToken(user.token)
    }

    @RequiresApi(Build.VERSION_CODES.O)
    @SuppressLint("SimpleDateFormat")
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_calendar, container, false)

        //календарь
        pb_loading = view.findViewById(R.id.pb_loading)
        scheduleTrainingAdapter = ScheduleTrainingAdapter(this, scheduleTrainingList)
        val specializationId = arguments?.getString(KEY_SPECIALIZATION_ID) ?: "0"
        getScheduleTraining(specializationId.toInt())
        calendarView = view.findViewById(R.id.calendarView)
        calendarView.setOnDayClickListener(object : OnDayClickListener {
            @SuppressLint("NewApi", "SimpleDateFormat")
            @RequiresApi(Build.VERSION_CODES.N)
            override fun onDayClick(eventDay: EventDay) {

                val clickedDayCalendar : Calendar = eventDay.calendar
                var calenDay = SimpleDateFormat("yyyy-MM-dd").format(clickedDayCalendar.time)
                val calendarDay = LocalDate.parse(calenDay, DateTimeFormatter.ISO_LOCAL_DATE)

                getScheduleTrainingDay(calendarDay)
            }
        })

        view.findViewById<RecyclerView>(R.id.rv_fitness_appointments_list).run {
            setHasFixedSize(true)
            layoutManager = LinearLayoutManager(requireActivity())
            adapter = scheduleTrainingAdapter
        }

        return view
    }

    private fun getScheduleTraining(specializationId: Int) {
        scheduleTrainingListPresenter.requestScheduleTrainingFromServer(specializationId)
    }

    @SuppressLint("NotifyDataSetChanged")
    @RequiresApi(Build.VERSION_CODES.O)
    private fun getScheduleTrainingDay(calendarDay: LocalDate) {
        scheduleTrainingList.clear()
        for (x in scheduleTrainingListAll) {
            val trainingDateFrom = LocalDate.parse(x.trainingDateFrom, DateTimeFormatter.ISO_ZONED_DATE_TIME)

            val trainingDateCalendar = Calendar.getInstance()
            trainingDateCalendar.clear()
            trainingDateCalendar.set(trainingDateFrom.year, trainingDateFrom.monthValue-1, trainingDateFrom.dayOfMonth)
            if (trainingDateFrom >= LocalDate.now()){
                eventsNotes.add(CalendarDay(trainingDateCalendar).apply {
                    backgroundResource = R.drawable.ic_dot_day
                })
            }

            if(trainingDateFrom == calendarDay)
                scheduleTrainingList.add(x)
        }
        val nowDay = Calendar.getInstance()
        eventsNotes.add(CalendarDay(nowDay).apply {
            backgroundResource = R.drawable.ic_dot_now_day
        })
        calendarView.setCalendarDays(eventsNotes)
        scheduleTrainingAdapter?.notifyDataSetChanged()
    }

    override fun setScheduleTrainingInRecycler(scheduleTraining: List<ScheduleTraining>?) {
        scheduleTrainingListAll.clear()
        scheduleTrainingListAll.addAll(scheduleTraining!!)
    }

    override fun setError(error: Throwable) {
        Snackbar.make(requireActivity().findViewById(R.id.rv_fitness_appointments_list), error.message.toString(), Snackbar.LENGTH_SHORT).show()
    }

    override fun showProgress() {
        pb_loading.visibility = View.VISIBLE
    }

    @RequiresApi(Build.VERSION_CODES.O)
    override fun hideProgress() {
        pb_loading.visibility = View.GONE
        getScheduleTrainingDay(LocalDate.now())
    }

}