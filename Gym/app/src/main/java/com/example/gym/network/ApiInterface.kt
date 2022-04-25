package com.example.gym.network

import User
import com.example.gym.models.*
import com.example.gym.models.save.LoginModel
import com.example.gym.models.save.SaveClassRecords
import com.example.gym.models.save.SaveUser
import retrofit2.Call
import retrofit2.http.*

import retrofit2.http.Body

interface ApiInterface {

    @POST("api/auth/authenticate")
    fun getAuthenticate(@Header("Content-Type") content_type: String, @Body loginModel: LoginModel) : Call<Auth>

    @POST("api/user/registration")
    fun getRegistration(@Header("Content-Type") content_type: String, @Body user: UserModel): Call<Auth>

    @GET("api/client/{idUser}")
    fun getClient(@Path("idUser") cardInt: String) : Call<Client>

    @GET("api/card/{card_id}")
    fun getCard(@Path("card_id") cardInt: Int?) : Call<Card>

    @GET("api/news/{news_id}")
    fun getNews(@Path("news_id") newsInt: Int?) : Call<News>

    @POST("api/auth/validate")
    fun validateToken(@Header("Content-Type") content_type: String, @Body token: String) : Call<Auth>

    @GET("api/news/last/{last}")
    fun getLastNews(@Path("last") last : Int) : Call<List<News>>

    @GET("api/cardSalesHistory/{idUser}")
    fun getCardSalesHistory(@Path("idUser") idUser: String) : Call<CardSalesHistory>

    @GET("api/servicesCard/{idCard}")
    fun getServicesCard(@Path("idCard") idCard: Int) : Call<List<ServicesCard>>

    @GET("api/specialization")
    fun getSpecialization() : Call<List<Specialization>>

    @GET("api/scheduleTraining/by_specialization/{idSpecialization}")
    fun getScheduleTraining(@Path("idSpecialization") idSpecialization: Int) : Call<List<ScheduleTraining>>

    @GET("api/scheduleTraining/{id}")
    fun getScheduleTrainingById(@Path("id") id: Int) : Call<ScheduleTraining>

    @GET("api/classRecords/classRecordsClient/{idClient}")
    fun getMyNotes(@Path("idClient") idClient: Int) : Call<List<ClassRecords>>

    @POST("api/classRecords")
    fun addClassRecords(@Header("Content-Type") content_type: String, @Body saveClassRecords: SaveClassRecords) : Call<ClassRecords>

    @DELETE("api/classRecords/{id}")
    fun deleteClassRecords(@Path("id") id: Int) : Call<ClassRecords>

    @PUT("api/user/{IdUser}")
    fun updateUser(@Header("Content-Type") content_type: String, @Path("IdUser") IdUser: Int, @Body saveUser: SaveUser) : Call<User>
    @PUT("api/user/pass/{IdUser}")
    fun updateLoginPass(@Header("Content-Type") content_type: String, @Path("IdUser") IdUser: Int, @Body saveUser: SaveUser) : Call<User>

}