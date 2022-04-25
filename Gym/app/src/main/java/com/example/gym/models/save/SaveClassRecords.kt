package com.example.gym.models.save

import com.google.gson.annotations.SerializedName

class SaveClassRecords (private @SerializedName("idTraining") var idTraining: Int,
                        private @SerializedName("idClient") var idClient: Int,
                        private @SerializedName("presence") var presence: Boolean?
)