package com.example.gym.models

import com.google.gson.annotations.SerializedName
import java.math.BigInteger
import java.util.*

data class News (

    @SerializedName("idNews") val idNews : Int,
    @SerializedName("date") val date : String,
    @SerializedName("title") var title: String,
    @SerializedName("text") var text : String
)