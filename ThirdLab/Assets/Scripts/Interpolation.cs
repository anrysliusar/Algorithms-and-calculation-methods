using UnityEngine;

public class Interpolation {
    private CanvasController userUI;

    public Interpolation(CanvasController userUI) {
        this.userUI = userUI;
    }

    public void SinFunctionInterpolation(int degree, float limLeft, float limRight) {
        
        float[] arrX = ValuesInInterval(limLeft, limRight, degree);
        float[] arrY = new float[arrX.Length];
        
        for (int i = 0; i < arrX.Length; i++) {
            arrY[i] = Mathf.Sin(arrX[i]);
        }

        float valX = limLeft;
        float incVal = 0.01f;
        
        while (valX < limRight) {
            float analyticVal = Mathf.Sin(valX);
            float interpolVal = Interpolate(arrX, arrY, valX);
            userUI.AddEntryToMainPlot(0, new Vector2(valX, interpolVal), CanvasController.instance.sinPlot);
            userUI.AddEntryToMainPlot(1, new Vector2(valX, analyticVal), CanvasController.instance.sinPlot);
            valX += incVal;
        }
    }

    public void GivenFuncInterpolation(int degree, float limLeft, float limRight) {
        float[] arrX = ValuesInInterval(limLeft, limRight, degree);
        float[] arrY = new float[arrX.Length];
        for (int i = 0; i < arrX.Length; i++) {
            arrY[i] = Mathf.Sin(arrX[i]) - 2 * Mathf.Cos(arrX[i]);
        }

        float valX = limLeft;
        float incVal = 0.01f;
        while (valX < limRight) {
            float analyticVal = Mathf.Sin(valX) - 2 * Mathf.Cos(valX);
            float interpolVal = Interpolate(arrX, arrY, valX);
            userUI.AddEntryToMainPlot(0, new Vector2(valX, interpolVal), CanvasController.instance.myFuncPlot);
            userUI.AddEntryToMainPlot(1, new Vector2(valX, analyticVal), CanvasController.instance.myFuncPlot);
            valX += incVal;
        }
    }

    public void SinFuncTweak(int maxDegree, float limLeft, float limRight) {
        float[][] arrXs = new float[maxDegree + 1][];
        float[][] arrYs = new float[maxDegree + 1][];

        for (int i = 0; i < maxDegree + 1; i++) {
            arrXs[i] = ValuesInInterval(limLeft, limRight, i + 2);
            arrYs[i] = new float[arrXs[i].Length];
            for (int j = 0; j < arrXs[i].Length; j++) {
                arrYs[i][j] = Mathf.Sin(arrXs[i][j]);
            }
        }

        for (int i = 0; i < maxDegree - 1; i++) {
            float valX = limLeft;
            float incVal = 0.01f;
            while (valX < limRight) {
                float yValue = Mathf.Abs(Interpolate(arrXs[i], arrYs[i], valX) -
                                         Interpolate(arrXs[i + 1], arrYs[i + 1], valX));
                userUI.AddEntryToErrorPlot(i, new Vector2(valX, yValue), CanvasController.instance.sinErrorPlot);
                valX += incVal;
            }
        }
    }

    public void GivenFuncTweak(int maxDegree, float limLeft, float limRight) {
        float[][] arrXs = new float[maxDegree + 1][];
        float[][] arrYs = new float[maxDegree + 1][];

        for (int i = 0; i < maxDegree + 1; i++) {
            arrXs[i] = ValuesInInterval(limLeft, limRight, i + 2);
            arrYs[i] = new float[arrXs[i].Length];
            for (int j = 0; j < arrXs[i].Length; j++) {
                arrYs[i][j] = Mathf.Sin(arrXs[i][j]) - 2 * Mathf.Cos(arrXs[i][j]);
            }
        }

        for (int i = 0; i < maxDegree - 1; i++) {
            float valX = limLeft;
            float incVal = 0.01f;
            while (valX < limRight) {
                if (valX > 2 && valX < 2.01) {
                    var deltaN = Interpolate(arrXs[i], arrYs[i], valX) -
                                 Interpolate(arrXs[i + 1], arrYs[i + 1], valX);
                    var deltaExactN = Interpolate(arrXs[i], arrYs[i], valX) -
                                      (Mathf.Sin(valX) - 2 * Mathf.Cos(valX));
                    var k = 1 - (deltaExactN / deltaN);
                    Debug.Log($"deltaN = {deltaN}, deltaExactN = {deltaExactN}, k = {k}");
                }

                float yValue = Mathf.Abs(Interpolate(arrXs[i], arrYs[i], valX) -
                                         Interpolate(arrXs[i + 1], arrYs[i + 1], valX));
                userUI.AddEntryToErrorPlot(i, new Vector2(valX, yValue), CanvasController.instance.myFuncErrorPlot);
                valX += incVal;
            }
        }
    }

    public float Interpolate(float[] xArray, float[] yArray, float xValue) {
        float functionValue = 0;

        for (int i = 0; i < xArray.Length; i++) {
            functionValue += DiffOfX(i, xValue, xArray) * DividedDifference(0, i, xArray, yArray);
        }
        return functionValue;
    }

    private float DividedDifference(int limLeftIndex, int limRightIndex, float[] arrX, float[] arrY) {
        float divDiff = 0;

        for (int j = limLeftIndex; j <= limRightIndex; j++) {
            float denominator = 1;
            for (int i = limLeftIndex; i <= limRightIndex; i++) {
                if (i == j) {
                    continue;
                }

                denominator *= (arrX[j] - arrX[i]);
            }

            divDiff += arrY[j] / denominator;
        }

        return divDiff;
    }

    private float DiffOfX(int countOfX, float valX, float[] arrX) {
        float diffOfX = 1;

        for (int i = 0; i < countOfX; i++) {
            diffOfX *= valX - arrX[i];
        }

        return diffOfX;
    }

    private float[] ValuesInInterval(float limLeft, float limRight, int countOfVal) {
        float incVal = Mathf.Abs(limRight - limLeft) / (countOfVal - 1);
        float[] vals = new float[countOfVal];

        for (int i = 0; i < countOfVal; i++) {
            vals[i] = limLeft + incVal * i;
        }

        return vals;
    }
}