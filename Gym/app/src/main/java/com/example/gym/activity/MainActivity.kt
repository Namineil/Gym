package com.example.gym.activity

import android.content.Context
import android.content.Intent

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Toast
import com.example.gym.models.Auth
import com.example.gym.network.ApiClient
import com.example.gym.utils.Constants
import com.google.android.material.snackbar.Snackbar
import com.google.gson.GsonBuilder
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response


class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        var settings = getSharedPreferences(Constants.APP_PREFERENCES, Context.MODE_PRIVATE)

        val token = settings.getString(Constants.APP_PREFERENCES_TOKEN, "")

        if(token != null) {
            val apiClient = ApiClient.partsApi;

            val call = apiClient.validateToken(Constants.CONTENT_TYPE, token)
            call.enqueue(object : Callback<Auth> {
                override fun onResponse(
                    call: Call<Auth>,
                    response: Response<Auth>
                ) {
                    if (response.body() != null && response.body()?.success == true) {
                        val homeIntent = Intent(this@MainActivity, HomeActivity::class.java);
                        val gson = GsonBuilder().create()
                        homeIntent.putExtra("User", gson.toJson(response.body()?.data))
                        startActivity(homeIntent)
                    } else {
                        val authIntent = Intent(this@MainActivity, AuthActivity::class.java);
                        startActivity(authIntent);
                    }

                    finish()
                }

                override fun onFailure(
                    call: Call<Auth>,
                    t: Throwable) {
                    Toast.makeText(applicationContext, "Token expired", Snackbar.LENGTH_LONG).show();

                    val authIntent = Intent(this@MainActivity, AuthActivity::class.java);
                    startActivity(authIntent);

                    finish()
                }
            })
        }

    }
}
