package com.example.gym.activity

import android.annotation.SuppressLint
import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.text.TextUtils
import android.view.LayoutInflater
import android.widget.*
import androidx.appcompat.app.AlertDialog
import com.example.gym.R
import com.example.gym.models.Auth
import com.example.gym.models.save.LoginModel
import com.example.gym.models.UserModel
import com.example.gym.network.ApiClient
import com.example.gym.utils.Constants
import com.google.android.material.snackbar.Snackbar
import com.google.gson.GsonBuilder
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class AuthActivity : AppCompatActivity() {

    private lateinit var btnSignIn: Button
    private lateinit var btnRegister: Button
    private lateinit var root: RelativeLayout

    var mSettings: SharedPreferences? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_auth)

        btnSignIn = findViewById(R.id.btnSignIn)
        btnRegister = findViewById(R.id.btnRegister)
        root = findViewById(R.id.root_main)
        mSettings = getSharedPreferences(Constants.APP_PREFERENCES, Context.MODE_PRIVATE);

        btnRegister.setOnClickListener {
            showRegisterWindow()
        }

        btnSignIn.setOnClickListener {
            showSignInWindow()
        }
    }

    @SuppressLint("ResourceType")
    private fun showSignInWindow() {
        val inflate = LayoutInflater.from(this)
        val signInView = inflate.inflate(R.layout.activity_sign_in, null)
        val dialog: AlertDialog = AlertDialog.Builder(this)
            .setTitle("Войти")
            .setMessage("Введите все данные для входа")
            .setView(signInView)
            .setPositiveButton("Войти", null)
            .setNegativeButton("Отменить", null)
            .show();
        val positiveButton: Button = dialog.getButton(AlertDialog.BUTTON_POSITIVE);
        positiveButton.setOnClickListener {
            val login = signInView.findViewById<EditText>(R.id.loginField).text.toString()
            val password = signInView.findViewById<EditText>(R.id.passwordField).text.toString()
            val rememberMe = signInView.findViewById<CheckBox>(R.id.rememberMeField).isChecked;

            if (TextUtils.isEmpty(login)) {
                Toast.makeText(applicationContext, "Введите логин", Toast.LENGTH_LONG).show()
                return@setOnClickListener
            }
            if (password.length < 5) {
                Toast.makeText(applicationContext, "Введите пароль более 5 символов", Toast.LENGTH_LONG).show()
                return@setOnClickListener
            }

            //авторизация
            val apiClient = ApiClient.partsApi
            val auth = LoginModel(login, password, rememberMe)

            val call = apiClient.getAuthenticate(Constants.CONTENT_TYPE, auth)
            call.enqueue(object : Callback<Auth> {
                override fun onResponse(
                    call: Call<Auth>,
                    response: Response<Auth>) {
                    if (response.body()?.data != null) {
                        dialog.dismiss()
                        val user = response.body()?.data
                        val editor: SharedPreferences.Editor = mSettings!!.edit()
                        editor.putString(Constants.APP_PREFERENCES_TOKEN, user?.token)
                        editor.apply()

                        val homeIntent = Intent(this@AuthActivity, HomeActivity::class.java)
                        val gson = GsonBuilder().create()
                        homeIntent.putExtra("User", gson.toJson(response.body()?.data))
                        startActivity(homeIntent)
                        finish()

                    } else {
                        Toast.makeText(applicationContext, response.body()?.message, Snackbar.LENGTH_LONG).show()
                    }

                }

                override fun onFailure(
                    call: Call<Auth>,
                    t: Throwable) {

                    Toast.makeText(applicationContext, t.message, Toast.LENGTH_LONG).show()
                }
            })

            val negativeButton: Button = dialog.getButton(AlertDialog.BUTTON_NEGATIVE);
            negativeButton.setOnClickListener {
                dialog.dismiss()
            }
        }
    }

    private fun showRegisterWindow() {
        val inflate = LayoutInflater.from(this)
        val registerView = inflate.inflate(R.layout.activity_register, null)

        val dialog: AlertDialog = AlertDialog.Builder(this)
            .setTitle("Зарегистрироваться")
            .setMessage("Введите все данные для регистрации")
            .setView(registerView)
            .setPositiveButton("Зарегистрировать", null)
            .setNegativeButton("Отменить", null)
            .show();
        val positiveButton: Button = dialog.getButton(AlertDialog.BUTTON_POSITIVE);
        positiveButton.setOnClickListener {

            val login = registerView.findViewById<EditText>(R.id.loginField).text.toString()
            val password = registerView.findViewById<EditText>(R.id.passwordField).text.toString()
            val email = registerView.findViewById<EditText>(R.id.emailField).text.toString()
            val fullName = registerView.findViewById<EditText>(R.id.fullNameField).text.toString()
            val birthday = registerView.findViewById<EditText>(R.id.birthdayField).text.toString()
            val phone = registerView.findViewById<EditText>(R.id.phoneField).text.toString()
            val gender = registerView.findViewById<Spinner>(R.id.sp_genderField).selectedItem.toString()

            if (TextUtils.isEmpty(login)) {
                Toast.makeText(applicationContext, "Введите логин", Toast.LENGTH_LONG).show()
                return@setOnClickListener
            }
            if (password.length < 5) {
                Toast.makeText(applicationContext, "Введите пароль более 5 символов", Toast.LENGTH_LONG).show()
                return@setOnClickListener
            }
            if (TextUtils.isEmpty(email)) {
                Toast.makeText(applicationContext, "Введите email", Toast.LENGTH_LONG).show()
                return@setOnClickListener
            }
            if (TextUtils.isEmpty(fullName)) {
                Toast.makeText(applicationContext, "Введите ФИО", Toast.LENGTH_LONG).show()
                return@setOnClickListener
            }

            //регистрация
            val apiClient = ApiClient.partsApi
            val user = UserModel(fullName, gender, email, phone, birthday, login, password)
            val call = apiClient.getRegistration(Constants.CONTENT_TYPE, user)
            call.enqueue(object : Callback<Auth> {
                override fun onResponse(
                    call: Call<Auth>,
                    response: Response<Auth>) {
                    if(response.body()?.data == null) {
                        Toast.makeText(applicationContext, response.body()?.message, Snackbar.LENGTH_LONG).show()
                    } else {
                        val user = response.body()?.data
                        val editor: SharedPreferences.Editor = mSettings!!.edit()
                        editor.putString(Constants.APP_PREFERENCES_TOKEN, user?.token)
                        editor.apply()

                        val homeIntent = Intent(this@AuthActivity, HomeActivity::class.java)
                        val gson = GsonBuilder().create()
                        homeIntent.putExtra("User", gson.toJson(response.body()?.data))
                        startActivity(homeIntent)
                        finish()
                    }
                }

                override fun onFailure(
                    call: Call<Auth>,
                    t: Throwable) {
                    Toast.makeText(applicationContext, t.message, Toast.LENGTH_LONG).show()
                }
            })
        }
    }
}
