package com.example.gym.models

import com.google.gson.annotations.SerializedName

class UserModel (fullName: String, gender: String, email: String, phone: String, birthday: String, login: String, password: String) {
    @SerializedName("login")
    private var login: String? = login

    @SerializedName("password")
    private var password: String? = password

    @SerializedName("fullName")
    private var fullName: String? = fullName

    @SerializedName("gender")
    private var gender: String? = gender

    @SerializedName("email")
    private var email: String? = email

    @SerializedName("phone")
    private var phone: String? = phone

    @SerializedName("birthday")
    private var birthday: String? = birthday

    @SerializedName("role")
    private var role: List<String>? = arrayListOf("User")

}