using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {
    public static CanvasController instance { get; private set; }
    private const float DEFAULT_PRECISION = 0.001f;
    private const float ERROR_TIME = 2f;
    private const string INPUT_ERROR_MESSAGE = "Неправильне введення даних!";

    public GameObject info;
    public GameObject JM;

    public GameObject error;
    public Image errorImage;
    public Text errorText;
    private Coroutine errorCoroutine;

    public InputField[] inputMatrix;
    public InputField[] inputComplementary;
    public Text[] textResults;

    private double[,] matrix = new double[3, 3];
    private double[] complementary = new double[3];
    
    private int numOfDimension;


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
        numOfDimension = (int) Mathf.Sqrt(inputMatrix.Length);
    }

    public void SetDefault() {
        info.SetActive(true);
        JM.SetActive(false);
    }

    private void OnClick(GameObject other) {
        info.SetActive(false);
        other.SetActive(true);
    }

    public void OnJacobiMethod() {
        OnClick(JM);
    }

    private bool TryReadInput() {
        for (var i = 0; i < inputMatrix.Length; i++) {
            var _i = i / numOfDimension;
            var _j = i % numOfDimension;
            if (!double.TryParse(inputMatrix[i].text, NumberStyles.Float, CultureInfo.InvariantCulture,
                out matrix[_i, _j])) {
                return false;
            }
        }
        for (var i = 0; i < inputComplementary.Length; i++) {
            if (!double.TryParse(inputComplementary[i].text, NumberStyles.Float, CultureInfo.InvariantCulture,
                out complementary[i])) {
                return false;
            }
        }
        return true;
    }


    public void OnFindResults() {
        if (!TryReadInput()) {
            RaiseAndShowError(INPUT_ERROR_MESSAGE);
            return;
        }

        PrintResults(FindResults());
    }

    private double[] FindResults() {
        JacobiAlgorithm.instance.SetData(matrix, complementary, DEFAULT_PRECISION);
        return JacobiAlgorithm.instance.JacobiAlgorithmImplementation();
    }

    private void PrintResults(double[] results) {
        for (var i = 0; i < textResults.Length; i++) {
            textResults[i].text = $"x{i+1} = {results[i]:f4}";
        }
    }
}