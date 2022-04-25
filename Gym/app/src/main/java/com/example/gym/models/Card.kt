package com.example.gym.models

import com.google.gson.annotations.SerializedName
import java.math.BigInteger

data class Card (

    @SerializedName("idCard") val idCard : Int,
    @SerializedName("name") val name : String,
    @SerializedName("period") var period : BigInteger,
    @SerializedName("price") val price : Int,
    @SerializedName("image") val image : String
)