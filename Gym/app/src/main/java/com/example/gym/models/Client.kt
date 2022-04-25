package com.example.gym.models

import com.google.gson.annotations.SerializedName

data class Client (

        @SerializedName("idClient") val idClient : Int,
        @SerializedName("status") val status : String,
        @SerializedName("userName") val userName : String,
        @SerializedName("idCard") val idCard : Int
)