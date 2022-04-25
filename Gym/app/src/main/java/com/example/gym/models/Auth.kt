package com.example.gym.models

import User
import com.google.gson.annotations.SerializedName

data class Auth (
        @SerializedName("success") val success : Boolean,
        @SerializedName("message") val message : String,
        @SerializedName("data") val data : User
)