package com.example.amofirstlab.ui.branched;

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

public class BranchedFragment extends Fragment {

    private EditText objA;
    private EditText objB;
    private Button btnCalc;
    private TextView textResultValue;
    private Presenter presenter;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_branched, container, false);
        objA = root.findViewById(R.id.editText_a);
        objB = root.findViewById(R.id.editText_b);
        btnCalc = root.findViewById(R.id.btn_calc);
        textResultValue = root.findViewById(R.id.text_result_value_y);
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
                double sumSquaresAB = Math.pow(a, 2) + Math.pow(b, 2);
                double diffSquaresAB = Math.pow(a, 2) - Math.pow(b, 2);

                double expressionResult;
                if (Math.sqrt(sumSquaresAB) > 10) {
                    if(diffSquaresAB <= 0) {
                        presenter.showMessage("У даному випадку (>10), числа a та b повинні задовольняти умові:\n a^2 - b^2 > 0 ", getContext());
                        return;
                    }
                    expressionResult = (Math.sqrt(sumSquaresAB / diffSquaresAB));

                } else {
                    if(diffSquaresAB < 0) {
                        presenter.showMessage("У даному випадку (<=10), числа a та b повинні задовольняти умові:\n a^2 - b^2 >= 0", getContext());
                        return;
                    }
                    expressionResult = (Math.sqrt(diffSquaresAB / sumSquaresAB));
                }
                textResultValue.setText(String.format(Locale.getDefault(), "y = %.4f", expressionResult));
            }
        });


    }
}