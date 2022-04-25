package com.example.gym.fragment

import User
import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import android.graphics.BitmapFactory
import android.graphics.drawable.BitmapDrawable
import android.os.Build
import android.os.Bundle
import android.util.Base64
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.annotation.RequiresApi
import androidx.navigation.fragment.findNavController
import com.example.gym.R
import com.example.gym.activity.MainActivity
import com.example.gym.utils.Constants
import com.example.gym.utils.Constants.APP_PREFERENCES_TOKEN
import com.google.gson.GsonBuilder
import de.hdodenhof.circleimageview.CircleImageView
import info.hoang8f.widget.FButton
import java.time.LocalDate
import java.time.format.DateTimeFormatter
import android.content.pm.PackageManager
import androidx.core.app.ActivityCompat
import android.Manifest
import android.widget.Toast
import android.app.Activity
import android.graphics.Bitmap
import android.net.Uri
import android.util.Log
import androidx.fragment.app.FragmentActivity
import com.example.gym.utils.FileUtils
import com.example.gym.models.save.SaveUser
import com.example.gym.network.ApiClient
import com.google.android.material.snackbar.Snackbar
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import java.lang.Exception
import java.io.ByteArrayOutputStream


class ProfileFragment : Fragment() {

    private val MY_REQUEST_CODE_PERMISSION = 1000
    private val MY_RESULT_CODE_FILECHOOSER = 2000
    private val LOG_TAG = "AndroidExample"
    private lateinit var scaledBitmap: Bitmap
    private lateinit var user: User
    private lateinit var userSave: SaveUser
    private val apiClient = ApiClient.authApi
    private lateinit var act: FragmentActivity
    private lateinit var viewProfile: View

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

    }

    @RequiresApi(Build.VERSION_CODES.O)
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        viewProfile = inflater.inflate(R.layout.fragment_profile, container, false)
        val view = viewProfile
        act = requireActivity()
        val userJson = activity?.intent?.getStringExtra("User")
        val gson = GsonBuilder().create()
        user = gson.fromJson(userJson, User::class.java)

        setImage(view, user.image)

        view.findViewById<TextView>(R.id.tv_profile_user_name).text = user.fullName
        view.findViewById<TextView>(R.id.tv_profile_user_email).text = user.email
        view.findViewById<TextView>(R.id.tv_profile_user_phone).text = user.phone
        var date = LocalDate.parse(user.birthday, DateTimeFormatter.ISO_LOCAL_DATE_TIME)
        view.findViewById<TextView>(R.id.tv_profile_user_birthday).text = date.format(DateTimeFormatter.ofPattern("dd.MM.yyyy"))

        view.findViewById<TextView>(R.id.met_me_weight).text = "65"

        view.findViewById<CircleImageView>(R.id.civ_profile_pic).setOnClickListener {
            askPermissionAndBrowseFile()
        }

        view.findViewById<FButton>(R.id.btn_profile_user_card_date).setOnClickListener {
            findNavController().navigate(R.id.action_profileFragment_to_dataUserFragment)
        }

        view.findViewById<FButton>(R.id.btn_profile_user_login_password).setOnClickListener {
            findNavController().navigate(R.id.action_profileFragment_to_loginPasswordFragment)
        }

        view.findViewById<FButton>(R.id.btn_exit).setOnClickListener {
            var settings = this.requireActivity().getSharedPreferences(Constants.APP_PREFERENCES, Context.MODE_PRIVATE)

            val editor: SharedPreferences.Editor = settings.edit()
            editor.remove(APP_PREFERENCES_TOKEN)
            editor.apply()
            val mainIntent = Intent(this.requireContext(), MainActivity::class.java)
            startActivity(mainIntent)
        }

        return view
    }

    private fun setImage(view: View, image: String) {
        if (image != null) {
            val imageBytes = Base64.decode(image, Base64.DEFAULT)
            val decodedImage = BitmapFactory.decodeByteArray(imageBytes, 0, imageBytes.size)
            view.findViewById<CircleImageView>(R.id.civ_profile_pic).setImageDrawable(BitmapDrawable(this.resources, decodedImage))
        } else {
            view.findViewById<CircleImageView>(R.id.civ_profile_pic).setImageResource(R.drawable.ic_profile_24dp)
        }
    }

    private fun askPermissionAndBrowseFile() {
        // With Android Level >= 23, you have to ask the user
        // for permission to access External Storage.
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) { // Level 23

            // Check if we have Call permission
            val permisson = ActivityCompat.checkSelfPermission(
                this.requireContext(),
                Manifest.permission.READ_EXTERNAL_STORAGE
            )
            if (permisson != PackageManager.PERMISSION_GRANTED) {
                // If don't have permission so prompt the user.
                requestPermissions(
                    arrayOf(Manifest.permission.READ_EXTERNAL_STORAGE),
                    MY_REQUEST_CODE_PERMISSION
                )
                return
            }
        }
        doBrowseFile()
    }

    private fun doBrowseFile() {
        var chooseFileIntent = Intent(Intent.ACTION_GET_CONTENT)
        chooseFileIntent.type = "image/*"
        // Only return URIs that can be opened with ContentResolver
        chooseFileIntent.addCategory(Intent.CATEGORY_OPENABLE)
        chooseFileIntent = Intent.createChooser(chooseFileIntent, "Choose a file")
        startActivityForResult(chooseFileIntent, MY_RESULT_CODE_FILECHOOSER)
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        when (requestCode) {
            MY_RESULT_CODE_FILECHOOSER -> if (resultCode == Activity.RESULT_OK) {
                if (data != null) {
                    val fileUri: Uri? = data.data
                    Log.i(LOG_TAG, "Uri: $fileUri")
                    var filePath: String? = null
                    try {
                        filePath = FileUtils.getPath(this.requireContext(), fileUri!!)
                        val options : BitmapFactory.Options = BitmapFactory.Options()
                        options.inScaled = true
                        options.inSampleSize = 5
                        scaledBitmap = BitmapFactory.decodeFile(filePath, options)

                        val byteArrayOutputStream = ByteArrayOutputStream()
                        scaledBitmap.compress(Bitmap.CompressFormat.PNG, 100, byteArrayOutputStream)
                        val byteArray: ByteArray = byteArrayOutputStream.toByteArray()
                        val encodedImage = Base64.encodeToString(byteArray, Base64.DEFAULT)

                        userSave = SaveUser(user.fullName,
                            user.gender,
                            user.email,
                            user.phone,
                            user.birthday,
                            encodedImage,
                            user.login,
                            user.password)
                        updateUser(user.idUser.toInt(), userSave)
                    } catch (e: Exception) {
                        Log.e(LOG_TAG, "Error: $e")
                        Toast.makeText(this.context, "Error: $e", Toast.LENGTH_SHORT).show()
                    }
                }
            }
        }
        super.onActivityResult(requestCode, resultCode, data)
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
                        user = response.body()!!
                        val gson = GsonBuilder().create()
                        act.intent?.putExtra("User", gson.toJson(response.body()))
                        setImage(viewProfile, user.image)
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