package com.example.gym.fragment

import android.annotation.SuppressLint
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.gym.R
import com.example.gym.adapter.SpecializationAdapter
import com.example.gym.models.Specialization
import com.example.gym.specialization_list.SpecializationListContract
import com.example.gym.specialization_list.SpecializationListPresenter
import com.google.android.material.snackbar.Snackbar

class SpecializationFragment : Fragment(), SpecializationListContract.View {

    private val specializationListPresenter = SpecializationListPresenter(this)
    private val specializationList = mutableListOf<Specialization?>()
    private var specializationAdapter : SpecializationAdapter? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        // Inflate the layout for this fragment
        val view = inflater.inflate(R.layout.fragment_specialization, container, false)
        specializationAdapter = SpecializationAdapter(this, specializationList)
        getSpecialization()
        view.findViewById<RecyclerView>(R.id.rv_specialization_list).run {
            setHasFixedSize(true)
            layoutManager = LinearLayoutManager(requireActivity())
            adapter = specializationAdapter
        }

        return view
    }

    private fun getSpecialization() {
        specializationListPresenter?.requestSpecializationFromServer()
    }

    @SuppressLint("NotifyDataSetChanged")
    override fun setSpecializationInRecycler(specialization: List<Specialization>?) {
        specializationList.addAll(specialization!!)
        specializationAdapter?.notifyDataSetChanged()
    }

    override fun setError(error: Throwable) {
        Snackbar.make(requireActivity().findViewById(R.id.rv_specialization_list), error.message.toString(), Snackbar.LENGTH_SHORT).show()
    }
}