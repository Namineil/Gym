package com.example.gym.network

import android.annotation.SuppressLint
import android.content.Context
import android.util.Log
import com.example.gym.utils.Constants
import okhttp3.OkHttpClient
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.security.cert.X509Certificate
import javax.net.ssl.SSLContext
import javax.net.ssl.TrustManager
import javax.net.ssl.X509TrustManager
import okhttp3.Interceptor
import okhttp3.Request


object ApiClient {

    private const val BASE_URL = "https://192.168.0.177:5001/"
    private var token = ""

    val partsApi : ApiInterface by lazy {
        Log.d("WebAccess", "Creating retrofit client")
        val retrofit = Retrofit.Builder()
                .baseUrl(BASE_URL)
                .client(getUnsafeOkHttpClient().build())
                .addConverterFactory(GsonConverterFactory.create())
                .build()
        return@lazy retrofit.create(ApiInterface::class.java)
    }

    val authApi : ApiInterface by lazy {
        val client = getUnsafeOkHttpClient().addInterceptor { chain ->
            val newRequest: Request = chain.request().newBuilder()
                .addHeader("Authorization", "Bearer $token")
                .build()
            chain.proceed(newRequest)
        }.build()

        Log.d("WebAccess", "Creating retrofit client")
        val retrofit = Retrofit.Builder()
            .client(client)
            .baseUrl(BASE_URL)
            .addConverterFactory(GsonConverterFactory.create())
            .build()
        return@lazy retrofit.create(ApiInterface::class.java)
    }

    fun setUserToken(data: String) {
        token = data
    }

    private fun getUnsafeOkHttpClient(): OkHttpClient.Builder {
        val x509TrustManager = object: X509TrustManager {
            @SuppressLint("TrustAllX509TrustManager")
            override fun checkClientTrusted(chain: Array<out X509Certificate>?, authType: String?) {
            }

            @SuppressLint("TrustAllX509TrustManager")
            override fun checkServerTrusted(chain: Array<out X509Certificate>?, authType: String?) {
            }

            override fun getAcceptedIssuers(): Array<X509Certificate> {
                return arrayOf()
            }
        }

        val trustManagers = arrayOf<TrustManager>(x509TrustManager)

        val sslContext = SSLContext.getInstance("TLS")
        sslContext.init(null, trustManagers, null)

        val builder = OkHttpClient.Builder()
        builder.sslSocketFactory(sslContext.socketFactory, x509TrustManager)
        builder.hostnameVerifier { _, _ -> true }

        return builder
    }

    }
