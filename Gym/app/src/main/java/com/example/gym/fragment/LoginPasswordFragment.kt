package com.example.gym.fragment

import User
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import com.example.gym.R
import com.example.gym.models.save.SaveUser
import com.example.gym.network.ApiClient
import com.example.gym.utils.Constants
import com.google.android.material.snackbar.Snackbar
import com.google.gson.GsonBuilder
import com.rengwuxian.materialedittext.MaterialEditText
import info.hoang8f.widget.FButton
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class LoginPasswordFragment : Fragment() {
    private lateinit var user: User
    private lateinit var userSave: SaveUser
    private val apiClient = ApiClient.authApi

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment

        val view = inflater.inflate(R.layout.fragment_login_password, container, false)
        val userJson = activity?.intent?.getStringExtra("User")
        val gson = GsonBuilder().create()
        user = gson.fromJson(userJson, User::class.java)

        val met_loginField = view.findViewById<MaterialEditText>(R.id.met_loginField)
        met_loginField.setText(user.login)
        val met_passwordField = view.findViewById<MaterialEditText>(R.id.met_passwordField)
        met_passwordField.setText("")

        view.findViewById<FButton>(R.id.btn_save).setOnClickListener {

            userSave = SaveUser(user.fullName,
                                user.gender,
                                user.email,
                                user.phone,
                                user.birthday,
                                user.image,
                                met_loginField.text.toString(),
                                met_passwordField.text.toString())
            updateLoginPass(user.idUser.toInt(), userSave)
            this.activity?.onBackPressed();
        }

        view.findViewById<FButton>(R.id.btn_cancel).setOnClickListener {
            this.activity?.onBackPressed();
        }

        return view
    }

    private fun updateLoginPass(idUser: Int, userSave : SaveUser) {
        val call = apiClient.updateLoginPass(Constants.CONTENT_TYPE, idUser, userSave)
        call.enqueue(
            object : Callback<User> {
                override fun onResponse(
                    call: Call<User>,
                    response: Response<User>
                ) {
                    if (response.body() != null) {
                        user = response.body()!!
                    } else
                        Toast.makeText(
                            requireContext(),
                            response.body()?.toString(),
                            Snackbar.LENGTH_LONG
                        ).show()
                }

                override fun onFailure(
                    call: Call<User>,
                    t: Throwable
                ) {

                    Toast.makeText(requireContext(), t.message, Toast.LENGTH_LONG).show()
                }
            })
    }

}