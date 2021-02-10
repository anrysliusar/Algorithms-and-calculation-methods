package com.example.amofirstlab;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;

public class Presenter {
    public void showMessage(String message, Context context){
        AlertDialog.Builder builder = new AlertDialog.Builder(context);
        builder
                .setMessage(message)
                .setCancelable(false)
                .setPositiveButton("Зрозуміло", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        dialogInterface.cancel();
                    }
                });
        AlertDialog alert = builder.create();
        alert.setTitle("Увага!");
        alert.show();
    }
}
