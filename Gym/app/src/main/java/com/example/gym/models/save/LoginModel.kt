package com.example.gym.models.save

import com.google.gson.annotations.SerializedName

class LoginModel(login: String, password: String, rememberMe : Boolean) {
    @SerializedName("login")
    private var login: String? = login

    @SerializedName("password")
    private var password: String? = password

    @SerializedName("rememberMe")
    private var rememberMe: Boolean? = rememberMe

    fun getLogin(): String? {
        return login
    }

    fun setLogin(login: String?) {
        this.login = login
    }

    fun getPassword(): String? {
        return password
    }

    fun setPassword(password: String?) {
        this.password = password
    }
}