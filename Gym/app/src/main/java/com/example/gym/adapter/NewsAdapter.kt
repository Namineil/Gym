package com.example.gym.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.core.os.bundleOf
import androidx.recyclerview.widget.RecyclerView
import com.example.gym.R
import com.example.gym.fragment.HomeFragment
import com.example.gym.models.News
import com.example.gym.utils.Constants
import androidx.navigation.findNavController
import java.util.*

class NewsAdapter (private var newsDataset: List<News?>
) : RecyclerView.Adapter<NewsAdapter.ViewHolder>() {

    class ViewHolder(val item: View) : RecyclerView.ViewHolder(item)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val itemView = LayoutInflater
            .from(parent.context)
            .inflate(R.layout.news_card, parent, false)
        return ViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        holder.item.findViewById<TextView>(R.id.tv_news_title).text = newsDataset[position]?.title
        holder.item.findViewById<TextView>(R.id.tv_news_text).text = newsDataset[position]?.text

        holder.itemView.setOnClickListener {
            val bundle = bundleOf(Constants.KEY_NEWS_TITLE to newsDataset[position]?.title, Constants.KEY_NEWS_TEXT to newsDataset[position]?.text)

            holder.item.findNavController().navigate(
                R.id.action_homeFragment_to_newsDetailsFragment,
                bundle)
        }
    }

    override fun getItemCount() = newsDataset.size

}

