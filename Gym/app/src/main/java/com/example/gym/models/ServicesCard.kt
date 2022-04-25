package com.example.gym.models

import com.google.gson.annotations.SerializedName
import java.math.BigInteger

data class ServicesCard (

    @SerializedName("idServices") val idServices : Int,
    @SerializedName("idCard") val idCard : Int,
    @SerializedName("name") var name : String
)