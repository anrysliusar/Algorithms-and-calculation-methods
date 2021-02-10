package com.example.amofirstlab.ui.cyclic;

import android.os.Bundle;
import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.example.amofirstlab.Presenter;
import com.example.amofirstlab.R;

public class CyclicFragment extends Fragment {
    public EditText objN;
    public Button btnCalc;
    public TextView textResultValue;
    private Presenter presenter;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_cyclic, container, false);
        objN = root.findViewById(R.id.editText_n);
        btnCalc = root.findViewById(R.id.btn_calc);
        textResultValue = root.findViewById(R.id.text_result_value_f);
        presenter = new Presenter();
        addListenerOnButton();

        return root;
    }

    public void addListenerOnButton() {
        btnCalc.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(objN.getText().length() == 0){
                    presenter.showMessage("Введіть число n!", getContext());
                    return;
                }

                int n = Integer.parseInt(objN.getText().toString());
                if(n < 0) {
                    presenter.showMessage("Число n повинно задовольняти умові:\n n >= 0", getContext());
                    return;
                }

                if(n > 20) {
                    presenter.showMessage("При n > 20 буде переповнено діапазон значень змінної результату, будь ласка, введіть n <= 20", getContext());
                    return;
                }

                long expressionResult = 0;
                for (int i = 1; i <= n; i++) {
                    expressionResult += (getFactorial(i) - getFactorial(i - 1));
                }
                textResultValue.setText(String.format("f = %s", expressionResult));
            }
        });
    }

    private long getFactorial(int n) {
        long result = 1;
        if (n == 0) {
            return result;
        }
        for (int i = 1; i <= n; i++) {
            result = result * i;
        }
        return result;
    }


}