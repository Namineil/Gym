package com.example.gym.fragment

import android.annotation.SuppressLint
import android.icu.text.SimpleDateFormat
import android.os.Build
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ProgressBar
import android.widget.TextView
import androidx.annotation.RequiresApi
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.applandeo.materialcalendarview.CalendarDay
import com.applandeo.materialcalendarview.CalendarView
import com.applandeo.materialcalendarview.EventDay
import com.applandeo.materialcalendarview.listeners.OnDayClickListener
import com.example.gym.R
import com.example.gym.adapter.MyNotesAdapter
import com.example.gym.models.ClassRecords
import com.example.gym.my_notes_list.MyNotesListContract
import com.example.gym.my_notes_list.MyNotesListPresenter
import com.example.gym.utils.Constants.KEY_CLIENT_ID
import com.google.android.material.snackbar.Snackbar
import java.time.LocalDate
import java.time.format.DateTimeFormatter
import java.util.*


class MyNotesFragment : Fragment(), MyNotesListContract.View {
    // TODO: Rename and change types of parameters
    private val myNotesListPresenter = MyNotesListPresenter(this)
    private val myNotesListAll = mutableListOf<ClassRecords?>()
    private val myNotesList = mutableListOf<ClassRecords?>()
    private var myNotesAdapter : MyNotesAdapter? = null
    private lateinit var pb_loading: ProgressBar
    private lateinit var rv_my_notes_list: RecyclerView
    private var eventsPresence = arrayListOf<CalendarDay>()
    private lateinit var calendarView: CalendarView
    private lateinit var tv_not_notes: TextView

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    @SuppressLint("ResourceAsColor")
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        val view = inflater.inflate(R.layout.fragment_my_notes, container, false)


        pb_loading = view.findViewById(R.id.pb_loading)
        rv_my_notes_list = view.findViewById(R.id.rv_my_notes_list)
        tv_not_notes = view.findViewById(R.id.tv_not_notes)
        myNotesAdapter = MyNotesAdapter(this, myNotesList)
        getMyNotes(KEY_CLIENT_ID)

        //календарь
        calendarView = view.findViewById(R.id.calendarView)

        //клик дня
        calendarView.setOnDayClickListener(object : OnDayClickListener {
            @SuppressLint("NewApi", "SimpleDateFormat")
            @RequiresApi(Build.VERSION_CODES.N)
            override fun onDayClick(eventDay: EventDay) {

                val clickedDayCalendar : Calendar = eventDay.calendar
                var calenDay = SimpleDateFormat("yyyy-MM-dd").format(clickedDayCalendar.time)
                val calendarDay = LocalDate.parse(calenDay, DateTimeFormatter.ISO_LOCAL_DATE)
                getMyNotesDay(calendarDay)
            }
        })

        rv_my_notes_list.run {
            setHasFixedSize(true)
            layoutManager = LinearLayoutManager(requireActivity())
            adapter = myNotesAdapter
        }

        return view
    }

    @RequiresApi(Build.VERSION_CODES.O)
    @SuppressLint("NewApi", "NotifyDataSetChanged")
    fun getMyNotesDay(calendarDay: LocalDate) {
        myNotesList.clear()
        val nowDay = Calendar.getInstance()
        eventsPresence.add(CalendarDay(nowDay).apply {
            backgroundResource = R.drawable.ic_dot_now_day
        })
        for (x in myNotesListAll) {
            val trainingDateFrom = LocalDate.parse(x!!.scheduleTrainingTrainingDateFrom, DateTimeFormatter.ISO_ZONED_DATE_TIME)
            val trainingDateCalendar = Calendar.getInstance()
            trainingDateCalendar.clear()
            trainingDateCalendar.set(trainingDateFrom.year, trainingDateFrom.monthValue-1, trainingDateFrom.dayOfMonth)
            if (trainingDateFrom < LocalDate.now()){
                when (x.presence) {
                    true -> {
                        eventsPresence.add(CalendarDay(trainingDateCalendar).apply {
                            backgroundResource = R.drawable.ic_dot_presence
                        })
                    }
                    false -> {
                        eventsPresence.add(CalendarDay(trainingDateCalendar).apply {
                            backgroundResource = R.drawable.ic_dot_not_presence
                        })
                    }
                    else -> {
                        eventsPresence.add(CalendarDay(trainingDateCalendar).apply {
                            backgroundResource = R.drawable.ic_dot_day
                        })
                    }
                }
            } else {
                eventsPresence.add(CalendarDay(trainingDateCalendar).apply {
                    backgroundResource = R.drawable.ic_dot_day
                })
            }

            if(trainingDateFrom == calendarDay) {
                myNotesList.add(x)
            }

        }
        if (myNotesList.size == 0) {
            tv_not_notes.visibility = View.VISIBLE
        } else {
            tv_not_notes.visibility = View.GONE
        }
        calendarView.setCalendarDays(eventsPresence)
        myNotesAdapter?.notifyDataSetChanged()
    }

    private fun getMyNotes(idClient: Int) {
        myNotesListPresenter?.requestMyNotesFromServer(idClient)
    }

    override fun showProgress() {
        pb_loading.visibility = View.VISIBLE
    }

    @RequiresApi(Build.VERSION_CODES.O)
    override fun hideProgress() {
        pb_loading.visibility = View.GONE
        getMyNotesDay(LocalDate.now())
    }

    @SuppressLint("NotifyDataSetChanged")
    override fun setMyNotesInRecycler(myNotes: List<ClassRecords>?) {
        myNotesListAll.clear()
        myNotesListAll.addAll(myNotes!!)
    }

    override fun setError(error: Throwable) {
        Snackbar.make(requireActivity().findViewById(R.id.rv_my_notes_list), error.message.toString(), Snackbar.LENGTH_SHORT).show()
    }

}