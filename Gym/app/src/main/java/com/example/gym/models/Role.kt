package com.example.gym.models

import com.google.gson.annotations.SerializedName

data class Role (
        @SerializedName("idRole") val idRole : Int,
        @SerializedName("name") val name : String
)