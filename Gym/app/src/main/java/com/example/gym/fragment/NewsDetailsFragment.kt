package com.example.gym.fragment

import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import com.example.gym.R
import com.example.gym.models.News
import com.example.gym.news_details.NewsDetailsContract
import com.example.gym.news_details.NewsDetailsPresenter
import com.example.gym.utils.Constants.KEY_CARD_ID
import com.example.gym.utils.Constants.KEY_NEWS_TEXT
import com.example.gym.utils.Constants.KEY_NEWS_TITLE
import com.google.android.material.snackbar.Snackbar

//import com.example.gym.utils.Constants.KEY_NEWS_TEXT
//import com.example.gym.utils.Constants.KEY_NEWS_TITLE

class NewsDetailsFragment : Fragment() {
//    private val newsDetailsPresenter = NewsDetailsPresenter(this)

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        val view = inflater.inflate(R.layout.fragment_news_details, container, false)

        val text = arguments?.getString(KEY_NEWS_TEXT) ?: "text"
        view.findViewById<TextView>(R.id.tv_news_text).text = text
        val title = arguments?.getString(KEY_NEWS_TITLE) ?: "title"
        view.findViewById<TextView>(R.id.tv_news_text).text = text
        view.findViewById<TextView>(R.id.tv_news_title).text = title
        return view
    }

}