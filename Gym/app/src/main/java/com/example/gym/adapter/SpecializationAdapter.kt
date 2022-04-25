package com.example.gym.adapter

import android.graphics.drawable.Drawable
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.core.content.res.ResourcesCompat
import androidx.core.os.bundleOf
import androidx.navigation.findNavController
import androidx.recyclerview.widget.RecyclerView
import com.example.gym.R
import com.example.gym.fragment.SpecializationFragment
import com.example.gym.models.Specialization
import com.example.gym.utils.Constants
import de.hdodenhof.circleimageview.CircleImageView

class SpecializationAdapter(private val specializationFragment: SpecializationFragment, private var specializationDataset: List<Specialization?>
) : RecyclerView.Adapter<SpecializationAdapter.ViewHolder>() {

    class ViewHolder(val item: View) : RecyclerView.ViewHolder(item)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val itemView = LayoutInflater
            .from(parent.context)
            .inflate(R.layout.specialization_card, parent, false)
        return ViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        val imageResource: Int = specializationFragment.requireActivity().resources!!.getIdentifier(
            "@drawable/" + specializationDataset[position]?.image, "drawable",
            specializationFragment.requireActivity().packageName
        )
        val image: Drawable? = specializationFragment.requireActivity().let { ResourcesCompat.getDrawable(it.resources, imageResource, null) }
        holder.item.findViewById<CircleImageView>(R.id.iv_specialization).setImageDrawable(image)
        holder.item.findViewById<TextView>(R.id.tv_specialization_title).text = specializationDataset[position]?.name

        holder.itemView.setOnClickListener {

            val bundle = bundleOf(Constants.KEY_SPECIALIZATION_ID to specializationDataset[position]?.idSpecialization.toString())

            holder.item.findNavController().navigate(
                R.id.action_specializationFragment_to_calendarFragment,
                bundle)
        }
    }

    override fun getItemCount() = specializationDataset.size

}