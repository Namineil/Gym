package com.example.gym.models

import com.google.gson.annotations.SerializedName
import java.math.BigInteger
import java.util.*

data class ClassRecords (

    @SerializedName("idClassRecords") val idClassRecords : Int,
    @SerializedName("idClient") val idClient : Int,
    @SerializedName("clientUserFullName") val clientUserFullName : String,
    @SerializedName("idTraining") val idTraining : Int,
    @SerializedName("scheduleTrainingTrainingDateFrom") val scheduleTrainingTrainingDateFrom: String,
    @SerializedName("presence") var presence: Boolean?
)