package com.example.gym.models

import com.google.gson.annotations.SerializedName
import java.math.BigInteger
import java.util.*

data class ScheduleTraining (

    @SerializedName("idTraining") val idTraining : Int,
    @SerializedName("idTrainer") val idTrainer : Int,
    @SerializedName("trainerUserFullName") val trainerUserFullName : String,
    @SerializedName("trainerUserImage") val trainerUserImage : String,
    @SerializedName("idRoom") val idRoom : Int,
    @SerializedName("roomName") val roomName : String,
    @SerializedName("trainingDateFrom") val trainingDateFrom : String,
    @SerializedName("trainingDateTo") val trainingDateTo : String,
    @SerializedName("idSpecialization") val idSpecialization : Int,
    @SerializedName("specializationName") val specializationName : String,
    @SerializedName("type") val type : String,
    @SerializedName("roominess") val roominess : Int,
    @SerializedName("engaged") val engaged : Int,
    @SerializedName("recordIsClosed") val recordIsClosed : Boolean
)