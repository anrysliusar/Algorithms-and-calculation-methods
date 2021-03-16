using System.Collections;
using AwesomeCharts;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {
    public static CanvasController instance;
    public static Interpolation interpolation;
    public GameObject info;
    public GameObject myFunc;
    public GameObject sin;
    public GameObject error;
    public Image errorImage;
    public Text errorText;
    private Coroutine errorCoroutine;

    public LineChart myFuncPlot;
    public LineChart myFuncErrorPlot;
    public LineChart sinPlot;
    public LineChart sinErrorPlot;
    public float limLeft;
    public float limRight;


    private void Awake() {
        instance = this;
        interpolation = new Interpolation(this);
    }

    private void Start() {
        SetDefault();
        ConfigurePlotChartAxisBorders(myFuncPlot.XAxis, new Vector2(limLeft, limRight));
        ConfigurePlotChartAxisBorders(myFuncErrorPlot.XAxis, new Vector2(limLeft, limRight)); 
        ConfigurePlotChartAxisBorders(sinPlot.XAxis, new Vector2(limLeft, limRight));
        ConfigurePlotChartAxisBorders(sinErrorPlot.XAxis, new Vector2(limLeft, limRight)); 
    }

    public void SetDefault() {
        info.SetActive(true);
        myFunc.SetActive(false);
        sin.SetActive(false);
    }

    private void OnClick(GameObject other) {
        info.SetActive(false);
        other.SetActive(true);
    }

    public void OnMyFunc() {
        OnClick(myFunc);
        MyFunction();
    }

    public void OnSin() {
        OnClick(sin);
        SinFunction();
    }

    private void SinFunction() {
        ClearPlotChart(sinPlot);
        ClearPlotChart(sinErrorPlot);
        
        interpolation.SinFunctionInterpolation(11, limLeft, limRight);
        interpolation.SinFuncTweak(11, limLeft, limRight);

        RefreshAllPlotCharts();
    }

    private void MyFunction() {
        ClearPlotChart(myFuncPlot);
        ClearPlotChart(myFuncErrorPlot);
        
        interpolation.GivenFuncInterpolation(11, limLeft, limRight);
        interpolation.GivenFuncTweak(11, limLeft, limRight);

        RefreshAllPlotCharts();
    }

    public void AddEntryToMainPlot(int dataSet, Vector2 entry, LineChart plot) {
        plot.GetChartData().DataSets[dataSet].AddEntry(new LineEntry(entry.x, entry.y)); 
    }

    public void AddEntryToErrorPlot(int dataSet, Vector2 entry, LineChart plot) {
        plot.GetChartData().DataSets[dataSet].AddEntry(new LineEntry(entry.x, entry.y));
    }

    private void ConfigurePlotChartAxisBorders(AxisBase axisObj, Vector2 axis) {
        axisObj.MinAxisValue = axis.x;
        axisObj.MaxAxisValue = axis.y;
    }

    private void ClearPlotChart(LineChart lineChart) {
        foreach (var dataSet in lineChart.GetChartData().DataSets) {
            dataSet.Clear();
        }
    }

    private void RefreshAllPlotCharts() {
        myFuncPlot.SetDirty();
        myFuncErrorPlot.SetDirty();
        sinPlot.SetDirty();
        sinErrorPlot.SetDirty();
    }
}