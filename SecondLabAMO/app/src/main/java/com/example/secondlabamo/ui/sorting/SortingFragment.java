package com.example.secondlabamo.ui.sorting;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import com.example.secondlabamo.R;
import com.example.secondlabamo.ui.graph.GraphicFragment;
import com.google.android.material.textfield.TextInputEditText;
import java.util.Locale;

import static com.example.secondlabamo.RadixSort.radixSort;


public class SortingFragment extends Fragment {
    private Button btn_graph;
    private Button btn_fill_rand;
    private Button btn_sort;
    private EditText min_num;
    private EditText max_num;
    private EditText amount_num;
    private TextInputEditText edit_array;
    private TextView text_time;
    private TextView text_result;


    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_sorting, container, false);
        btn_graph = root.findViewById(R.id.btn_graph);
        btn_fill_rand = root.findViewById(R.id.btn_fill_random);
        btn_sort = root.findViewById(R.id.btn_sort);
        min_num = root.findViewById(R.id.editText_min_number);
        max_num = root.findViewById(R.id.editText_max_number);
        amount_num = root.findViewById(R.id.editText_amount_number);
        edit_array = root.findViewById(R.id.edit_array);
        text_time = root.findViewById(R.id.text_sorted_time);
        text_result = root.findViewById(R.id.text_result_sort);
        addListenerOnButton();
        return root;

    }

    public void addListenerOnButton() {
        btn_fill_rand.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if (min_num.getText().length() == 0 || max_num.getText().length() == 0 || amount_num.getText().length() == 0) {
                    showMessage("Спочатку введіть нижню та верхню межу випадкових значень (Від, до) та їх кількість!", getContext());
                    return;
                }
                int min = Integer.parseInt(min_num.getText().toString());
                int max = Integer.parseInt(max_num.getText().toString());
                int amount = Integer.parseInt(amount_num.getText().toString());
                int[] arr = fillArrayWithRandomNumbers(min, max, amount);
                edit_array.setText(convertIntArrayInStr(arr));
            }
        });

        btn_sort.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String[] receivedArrayStr;
                String strArr = edit_array.getText().toString();
                if(strArr.length() == 0){
                    showMessage("Спочатку введіть числа", getContext());
                    return;
                }
                receivedArrayStr = strArr.split(" ");
                int size = receivedArrayStr.length;
                int[] receivedArray = new int[size];

                for (int i = 0; i < receivedArrayStr.length; i++) {

                    if(receivedArrayStr[i].equals("")){
                        continue;
                    }
                    try {
                        receivedArray[i] = Integer.parseInt(receivedArrayStr[i]);
                    }catch (NumberFormatException e){
                        showMessage("Hеправильно введені дані", getContext());
                        return;
                    }
                }

                long startTime = System.currentTimeMillis();
                radixSort(receivedArray);
                long endTime = System.currentTimeMillis();
                long executionTime = endTime - startTime;

                text_time.setText(String.format(Locale.getDefault(), "Масив відсортовано за %d ms", executionTime));
                text_result.setText(convertIntArrayInStr(receivedArray));
            }
        });

        btn_graph.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Fragment graphicFragment = new GraphicFragment();
                FragmentTransaction ft = getParentFragmentManager().beginTransaction();
                ft.replace(R.id.nav_host_fragment, graphicFragment);
                ft.commit();

            }
        });
    }

    private int[] fillArrayWithRandomNumbers(int min, int max, int amount) {
        int[] array = new int[amount];
        for (int i = 0; i < array.length; i++) {
            array[i] = (int) (Math.random() * (max - min + 1) + min);
        }
        return array;
    }

    public void showMessage(String message, Context context) {
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

    private StringBuilder convertIntArrayInStr(int[] array) {
        StringBuilder str = new StringBuilder();
        for (int value : array) {
            if (value == 0) {
                continue;
            }
            str.append(Integer.toString(value)).append(" ");
        }
        return str;
    }

}