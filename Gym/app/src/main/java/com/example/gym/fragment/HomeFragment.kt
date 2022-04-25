package com.example.gym.fragment

import User
import android.annotation.SuppressLint
import android.app.Activity
import android.graphics.drawable.Drawable
import android.os.Build
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import android.widget.Toast
import androidx.annotation.RequiresApi
import androidx.core.content.res.ResourcesCompat
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.gym.R
import com.example.gym.adapter.NewsAdapter
import com.example.gym.card_details.CardDetailsContract
import com.example.gym.card_details.CardDetailsPresenter
import com.example.gym.models.Card
import com.example.gym.models.Client
import com.example.gym.models.News
import com.example.gym.network.ApiClient
import com.example.gym.news_list.NewsListContract
import com.example.gym.news_list.NewsListPresenter
import com.google.android.material.snackbar.Snackbar
import com.google.gson.GsonBuilder
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import androidx.recyclerview.widget.RecyclerView
import com.example.gym.models.CardSalesHistory
import com.example.gym.utils.Constants.KEY_CARD_ID
import com.example.gym.utils.Constants.KEY_CARD_IMAGE
import com.example.gym.utils.Constants.KEY_CARD_SALES
import com.example.gym.utils.Constants.KEY_CLIENT_ID
import java.time.LocalDate
import java.time.format.DateTimeFormatter
import java.time.temporal.ChronoUnit

class HomeFragment : Fragment(), CardDetailsContract.View, NewsListContract.View {

    private val cardDetailsPresenter = CardDetailsPresenter(this)
    private val newsListPresenter = NewsListPresenter(this)
    private lateinit var client: Client
    private lateinit var cardSalesHistory: CardSalesHistory
//    private var card_id : Int = 0
    private lateinit var user: User
    private lateinit var tv_card: TextView
    private val apiClient = ApiClient.authApi
    private val newsList = mutableListOf<News?>()
    private var newsAdapter : NewsAdapter? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        val userJson = activity?.intent?.getStringExtra("User");
        val gson = GsonBuilder().create()
        user = gson.fromJson(userJson, User::class.java);

        ApiClient.setUserToken(user.token);

        getNews()
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        getClientCard()
        // Inflate the layout for this fragment
        val view = inflater.inflate(R.layout.fragment_home, container, false)
        tv_card = view.findViewById<TextView>(R.id.tv_Card)
        newsAdapter = NewsAdapter(newsList)
        view.findViewById<RecyclerView>(R.id.rv_news_list).run {
            setHasFixedSize(true)
            layoutManager = LinearLayoutManager(requireActivity())
            adapter = newsAdapter
        }

        view.findViewById<TextView>(R.id.tv_Card).setOnClickListener{
            findNavController().navigate(R.id.action_homeFragment_to_cardDetailsFragment)
        }

        return view
    }

    private fun getClientCard() {
        val call = apiClient.getClient(user.idUser)
        call.enqueue(
            object : Callback<Client> {
                override fun onResponse(
                    call: Call<Client>,
                    response: Response<Client>
                ) {
                    if (response.body()?.idClient != null) {
                        client = response.body()!!
                        KEY_CLIENT_ID = client.idClient
                        getSalesCard()
                        cardDetailsPresenter.requestCardDetails(client.idCard)
                    } else {
                        val imageResource: Int = activity?.resources!!.getIdentifier(
                            "@drawable/ic_abonement_guest_access", "drawable",
                            activity?.packageName
                        )
                        val image: Drawable? =
                            activity?.let { ResourcesCompat.getDrawable(it.resources, imageResource, null) }

                        tv_card.background = image
                    }
                }

                override fun onFailure(
                    call: Call<Client>,
                    t: Throwable) {

                    Toast.makeText(requireContext(), t.message, Toast.LENGTH_LONG).show()
                }
            })
    }

    private fun getNews() {
        newsListPresenter.requestNewsFromServer()
    }

    @RequiresApi(Build.VERSION_CODES.O)
    @SuppressLint("SetTextI18n")
    override fun setDataInViews(card: Card) {
        KEY_CARD_ID = card.idCard
        KEY_CARD_IMAGE = card.image
        val daysSales = LocalDate.parse(cardSalesHistory.date, DateTimeFormatter.ISO_LOCAL_DATE_TIME)
        val from = daysSales.plusMonths(1)
        val today = LocalDate.now()
        val period = ChronoUnit.DAYS.between(today, from)
        KEY_CARD_SALES = period
        if (period > 0) {
            val imageResource: Int = activity?.resources!!.getIdentifier(
                "@drawable/" + card.image, "drawable",
                activity?.packageName
            )
            val image: Drawable? =
                activity?.let { ResourcesCompat.getDrawable(it.resources, imageResource, null) }
            tv_card.background = image
            tv_card.text = "Осталось: $period дней"
        } else {
            val imageResource: Int = activity?.resources!!.getIdentifier(
                "@drawable/ic_abonement_guest_access", "drawable",
                activity?.packageName
            )
            val image: Drawable? =
                activity?.let { ResourcesCompat.getDrawable(it.resources, imageResource, null) }

            tv_card.background = image
        }
    }

    private fun getSalesCard() {
        val call = apiClient.getCardSalesHistory(user.idUser)
        call.enqueue(
            object : Callback<CardSalesHistory> {
                override fun onResponse(
                    call: Call<CardSalesHistory>,
                    response: Response<CardSalesHistory>
                ) {
                    if (response.body() != null) {
                        cardSalesHistory = response.body()!!
                    }
                }

                override fun onFailure(
                    call: Call<CardSalesHistory>,
                    t: Throwable) {

                    Toast.makeText(requireContext(), t.message, Toast.LENGTH_LONG).show()
                }
            })
    }

    @SuppressLint("NotifyDataSetChanged")
    override fun setNewsInRecycler(news: List<News>?) {
            newsList.addAll(news!!)
            newsAdapter?.notifyDataSetChanged()
    }

    override fun setError(error: Throwable) {
        Snackbar.make(requireActivity().findViewById(R.id.rv_news_list), error.message.toString(), Snackbar.LENGTH_SHORT).show()
    }

}