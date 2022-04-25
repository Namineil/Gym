package com.example.gym.fragment

import User
import android.os.Build
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.Spinner
import android.widget.Toast
import androidx.annotation.RequiresApi
import com.example.gym.R
import com.example.gym.models.save.SaveUser
import com.example.gym.network.ApiClient
import com.example.gym.utils.Constants
import com.google.android.material.snackbar.Snackbar
import com.google.gson.GsonBuilder
import com.rengwuxian.materialedittext.MaterialEditText
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.time.LocalDate
import java.time.format.DateTimeFormatter
import android.annotation.SuppressLint
import androidx.fragment.app.FragmentActivity
import info.hoang8f.widget.FButton

class DataUserFragment : Fragment() {
    private lateinit var user: User
    private lateinit var userSave: SaveUser
    private val apiClient = ApiClient.authApi
    private lateinit var act: FragmentActivity

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

    }

    @SuppressLint("SimpleDateFormat")
    @RequiresApi(Build.VERSION_CODES.O)
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        val view = inflater.inflate(R.layout.fragment_data_user, container, false)
        act = requireActivity()
        val userJson = activity?.intent?.getStringExtra("User")
        val gson = GsonBuilder().create()
        user = gson.fromJson(userJson, User::class.java)

        val met_fullNameField = view.findViewById<MaterialEditText>(R.id.met_fullNameField)
        met_fullNameField.setText(user.fullName)
        val met_emailField = view.findViewById<MaterialEditText>(R.id.met_emailField)
        met_emailField.setText(user.email)
        val met_phoneField = view.findViewById<MaterialEditText>(R.id.met_phoneField)
        met_phoneField.setText(user.phone)
        val string = resources.getStringArray(R.array.gender)
        var select = 0
        for (x in string) {
            if (x != user.gender) {
                ++select
            } else {
                break
            }
        }
        val sp_genderField = view.findViewById<Spinner>(R.id.sp_genderField)
        sp_genderField.setSelection(select)
        var date = LocalDate.parse(user.birthday, DateTimeFormatter.ISO_LOCAL_DATE_TIME)
        val met_birthdayField = view.findViewById<MaterialEditText>(R.id.met_birthdayField)
        met_birthdayField.setText(date.format(
            DateTimeFormatter.ofPattern("dd.MM.yyyy")))

        view.findViewById<FButton>(R.id.btn_save).setOnClickListener {
            val dtStart = met_birthdayField.text.toString()
//            val format = SimpleDateFormat("dd.MM.yyyy")
            val date = LocalDate.parse(dtStart, DateTimeFormatter.ofPattern("dd.MM.yyyy"))
            val saveDate = date.format(
                DateTimeFormatter.ofPattern("yyyy-MM-dd"))
            userSave = SaveUser(met_fullNameField.text.toString(),
                                sp_genderField.selectedItem.toString(),
                                met_emailField.text.toString(),
                                met_phoneField.text.toString(),
                                saveDate.toString(),
                                user.image,
                                user.login,
                                user.password)
            updateUser(user.idUser.toInt(), userSave)
            this.activity?.onBackPressed()
        }

        view.findViewById<FButton>(R.id.btn_cancel).setOnClickListener {
            this.activity?.onBackPressed()
        }

        return view
    }

    private fun updateUser(idUser: Int, userSave : SaveUser) {
        val call = apiClient.updateUser(Constants.CONTENT_TYPE, idUser, userSave)
        call.enqueue(
            object : Callback<User> {
                override fun onResponse(
                    call: Call<User>,
                    response: Response<User>
                ) {
                    if (response.body() != null) {
                        val gson = GsonBuilder().create()
                        act.intent?.putExtra("User", gson.toJson(response.body()))
                        user = response.body()!!;
                    } else
                        Toast.makeText(requireContext(), response.body()?.toString(), Toast.LENGTH_LONG).show()
                }

                override fun onFailure(
                    call: Call<User>,
                    t: Throwable) {

                    Toast.makeText(requireContext(), t.message, Toast.LENGTH_LONG).show()
                }
            })
    }

}