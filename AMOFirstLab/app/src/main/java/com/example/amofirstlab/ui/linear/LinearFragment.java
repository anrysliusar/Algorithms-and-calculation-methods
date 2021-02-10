package com.example.amofirstlab.ui.linear;


import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import com.example.amofirstlab.Presenter;
import com.example.amofirstlab.R;

import java.util.Locale;


public class LinearFragment extends Fragment {

    private EditText objA;
    private EditText objB;
    private Button btnCalc;
    private TextView textResultValue;
    private Presenter presenter;


    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_linear, container, false);
        objA = root.findViewById(R.id.editText_a);
        objB = root.findViewById(R.id.editText_b);
        btnCalc = root.findViewById(R.id.btn_calc);
        textResultValue = root.findViewById(R.id.text_result_value_Y1);
        presenter = new Presenter();
        addListenerOnButton();
        return root;
    }

    public void addListenerOnButton() {
        btnCalc.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(objA.getText().length() == 0 || objB.getText().length() == 0){
                    presenter.showMessage("Введіть числа a та b!", getContext());
                    return;
                }

                double a = Double.parseDouble(objA.getText().toString());
                double b = Double.parseDouble(objB.getText().toString());
                if((a <= b) || (a <= -b) || (a == b + 1)) {
                   presenter.showMessage("Числа a та b повинні задовольняти умовам:\n a > b \n a > -b \n a != b + 1 ", getContext());
                    return;
                }

                double expressionResult = ((Math.log10(a + b)) / (Math.log(a - b))) + ((Math.log(a + b)) / (Math.log10(a - b)));
                textResultValue.setText(String.format(Locale.getDefault(), "Y1 = %.4f", expressionResult));
            }
        });
    }
}