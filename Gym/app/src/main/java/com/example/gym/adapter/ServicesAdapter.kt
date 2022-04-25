package com.example.gym.adapter

import android.annotation.SuppressLint
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.gym.R
import com.example.gym.models.ServicesCard

class ServicesAdapter (private var servicesDataset: List<ServicesCard?>
) : RecyclerView.Adapter<ServicesAdapter.ViewHolder>() {

    class ViewHolder(val item: View) : RecyclerView.ViewHolder(item)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val itemView = LayoutInflater
            .from(parent.context)
            .inflate(R.layout.services_list, parent, false)
        return ViewHolder(itemView)
    }

    @SuppressLint("SetTextI18n")
    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        holder.item.findViewById<TextView>(R.id.tv_services).text = "* " + servicesDataset[position]?.name
    }

    override fun getItemCount() = servicesDataset.size

}

