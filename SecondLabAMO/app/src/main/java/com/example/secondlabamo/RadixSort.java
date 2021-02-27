package com.example.secondlabamo;

import java.util.ArrayList;
import java.util.List;

public class RadixSort {
    public static void radixSort(int[] array) {
        final int RADIX = 10;
        List<Integer>[] bucket = new ArrayList[RADIX];
        for (int i = 0; i < bucket.length; i++) {
            bucket[i] = new ArrayList<Integer>();
        }
        // sort
        boolean maxLength = false;
        int tmp = -1, place = 1;

        while (!maxLength) {
            maxLength = true;
            // split input between lists
            for (Integer i : array) {
                tmp = i / place;
                bucket[tmp % RADIX].add(i);
                if (maxLength && tmp > 0) {
                    maxLength = false;
                }
            }
            // empty lists into input array
            int a = 0;
            for (int b = 0; b < RADIX; b++) {

                for (Integer i : bucket[b]) {

                    array[a++] = i;
                }
                bucket[b].clear();
            }
            // move to next digit
            place *= RADIX;
        }

    }
}
