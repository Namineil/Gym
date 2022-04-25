package com.example.gym.models

import com.google.gson.annotations.SerializedName
import java.math.BigInteger

data class CardSalesHistory (

    @SerializedName("idSalesHistory") val idSalesHistory : Int,
    @SerializedName("idUser") val idUser : Int,
    @SerializedName("idCard") val idCard : Int,
    @SerializedName("date") var date : String
)