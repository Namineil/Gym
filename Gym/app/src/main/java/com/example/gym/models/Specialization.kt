package com.example.gym.models

import com.google.gson.annotations.SerializedName

data class Specialization (
        @SerializedName("idSpecialization") val idSpecialization : Int,
        @SerializedName("name") val name : String,
        @SerializedName("image") val image : String
)