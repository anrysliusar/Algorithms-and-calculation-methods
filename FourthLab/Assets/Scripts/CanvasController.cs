using System;
using System.Collections;
using System.Globalization;
using AwesomeCharts;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {
    public static CanvasController instance { get; private set; }
    private const float ERROR_TIME = 2f;
    private const string INPUT_ERROR_MESSAGE = "Неправильне введення даних!";

    public GameObject info;
    public GameObject combinedMethod;
    
    public GameObject error;
    public Image errorImage;
    public Text errorText;
    private Coroutine errorCoroutine;

    public LineChart plot;

    public InputField inputLeftLim;
    public InputField inputRightLim;
    public InputField inputEpsilon;
    public Text resultText;


    private float MyFx(float x) => x * x * x - 2 * x + 7;


    private void ShowErrorMessage(string message) {
        if (errorCoroutine != null) {
            StopCoroutine(errorCoroutine);
        }

        errorCoroutine = StartCoroutine(_ShowErrorMessage(message));
    }

    private IEnumerator _ShowErrorMessage(string message) {
        error.SetActive(true);
        errorText.text = message;
        var time = 0f;
        while (time < ERROR_TIME) {
            errorImage.color = Color.Lerp(new Color(180f, 180f, 180f), Color.clear, time / ERROR_TIME);
            time += Time.deltaTime;
            yield return null;
        }

        error.SetActive(false);
        errorCoroutine = null;
    }

    public void RaiseAndShowError(string message) {
        ShowErrorMessage(message);
    }

    private void Awake() {
        instance = this;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
    }

    private void Start() {
        SetDefault();
    }

    public void SetDefault() {
        info.SetActive(true);
        combinedMethod.SetActive(false);
    }

    private void OnClick(GameObject other) {
        info.SetActive(false);
        other.SetActive(true);
    }

    public void OnCombinedMethod() {
        OnClick(combinedMethod);
    }

    private bool CheckInputs(out float leftLim, out float rightLim, out float epsilon) {
        if (!float.TryParse(inputLeftLim.text, NumberStyles.Float, CultureInfo.InvariantCulture, out leftLim) ||
            !float.TryParse(inputRightLim.text, NumberStyles.Float, CultureInfo.InvariantCulture, out rightLim) ||
            !float.TryParse(inputEpsilon.text, NumberStyles.Float, CultureInfo.InvariantCulture, out epsilon)) {
            (leftLim, rightLim, epsilon) = (0, 0, 0);
            leftLim = rightLim = epsilon = 0;
            return false;
        }

        return true;
    }


    public void OnFindRoot() {
        ClearPlotChart(plot);
        if (!CheckInputs(out var leftLim, out var rightLim, out var epsilon)) {
            RaiseAndShowError(INPUT_ERROR_MESSAGE);
            return;
        }

        DrawFunction(leftLim, rightLim);
        float root;
        try {
            root = FindRoot(leftLim, rightLim, epsilon);
        }
        catch (Exception e) {
            RaiseAndShowError(e.Message);
            return;
        }
        
        AddEntryToPlot(1, root - root / 1000, MyFx(root + root / 1000), plot);
        AddEntryToPlot(1, root + root / 1000, MyFx(root - root / 1000), plot);
        RefreshPlotChart();
        resultText.text = $"x = {Math.Round(root, 5)}";
    }

    private float FindRoot(float leftLim, float rightLim, float epsilon) {
        CombinedMethod.instance.SetInputs(leftLim, rightLim, epsilon);
        return CombinedMethod.instance.FindRoot();
    }

    private void DrawFunction(float leftLim, float rightLim, int numOfPoints = 250) {
        ConfigurePlotChartAxisBorders(plot.XAxis, new Vector2(leftLim, rightLim));
        for (var x = leftLim; x < rightLim; x += (rightLim - leftLim) / numOfPoints) {
            AddEntryToPlot(0, x, MyFx(x), plot);
        }

        RefreshPlotChart();
    }

    private void AddEntryToPlot(int dataSet, float x, float y, LineChart plot) {
        plot.GetChartData().DataSets[dataSet].AddEntry(new LineEntry(x, y));
    }

    private void ConfigurePlotChartAxisBorders(AxisBase axObj, Vector2 axLimits) {
        axObj.MinAxisValue = axLimits.x;
        axObj.MaxAxisValue = axLimits.y;
    }

    private void ClearPlotChart(LineChart lineChart) {
        foreach (var dataSet in lineChart.GetChartData().DataSets) {
            dataSet.Clear();
        }
    }

    private void RefreshPlotChart() {
        plot.SetDirty();
    }
}