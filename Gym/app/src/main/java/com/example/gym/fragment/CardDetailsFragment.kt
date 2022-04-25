package com.example.gym.fragment

import android.annotation.SuppressLint
import android.app.Activity
import android.graphics.drawable.Drawable
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import android.widget.Toast
import androidx.core.content.res.ResourcesCompat
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.gym.R
import com.example.gym.adapter.ServicesAdapter
import com.example.gym.models.ServicesCard
import com.example.gym.network.ApiClient
import com.example.gym.utils.Constants
import com.google.android.material.snackbar.Snackbar
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class CardDetailsFragment : Fragment() {

    private val apiClient = ApiClient.authApi
    val mActivity: Activity? = this.activity
    private lateinit var tv_Card: TextView
    private val servicesCard = mutableListOf<ServicesCard?>()
    private var servicesAdapter : ServicesAdapter? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        val view =  inflater.inflate(R.layout.fragment_card_details, container, false)
        val cardId = Constants.KEY_CARD_ID
        tv_Card = view.findViewById<TextView>(R.id.tv_Card)
        servicesAdapter = ServicesAdapter(servicesCard)
        getCard()
        getServicesCard(cardId)

        view.findViewById<RecyclerView>(R.id.rv_services_list).run {
            setHasFixedSize(true)
            layoutManager = LinearLayoutManager(requireActivity())
            adapter = servicesAdapter
        }
        return view
    }

    private fun getServicesCard(cardId: Int) {
        val call = apiClient.getServicesCard(cardId)
        call.enqueue(
            object : Callback<List<ServicesCard>> {
                @SuppressLint("NotifyDataSetChanged")
                override fun onResponse(
                    call: Call<List<ServicesCard>>,
                    response: Response<List<ServicesCard>>
                ) {
                    if (response.body() != null) {
                        servicesCard.addAll(response.body()!!)
                        servicesAdapter?.notifyDataSetChanged()
                    } else
                        Toast.makeText(mActivity, response.body()?.toString(), Snackbar.LENGTH_LONG).show()
                }

                override fun onFailure(
                    call: Call<List<ServicesCard>>,
                    t: Throwable) {

                    Toast.makeText(mActivity, t.message, Toast.LENGTH_LONG).show()
                }
            })
    }
    @SuppressLint("SetTextI18n")
    private fun getCard() {
        if (Constants.KEY_CARD_SALES > 0) {
            val imageResource: Int = activity?.resources!!.getIdentifier(
                "@drawable/" + Constants.KEY_CARD_IMAGE, "drawable",
                activity?.packageName
            )
            val image: Drawable? =
                activity?.let { ResourcesCompat.getDrawable(it.resources, imageResource, null) }
            tv_Card.background = image
            tv_Card.text = "Осталось: ${Constants.KEY_CARD_SALES} дней"
        } else {
            val imageResource: Int = activity?.resources!!.getIdentifier(
                "@drawable/ic_abonement_guest_access", "drawable",
                activity?.packageName
            )
            val image: Drawable? =
                activity?.let { ResourcesCompat.getDrawable(it.resources, imageResource, null) }

            tv_Card.background = image
        }
    }

}