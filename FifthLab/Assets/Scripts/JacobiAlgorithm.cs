using System;


class JacobiAlgorithm {
    public static JacobiAlgorithm instance { get; } = new JacobiAlgorithm();
    private double[,] matrix;
    private double[] complementaryElem;

    private double _precision;

    public double Precision {
        set => _precision = value;
    }

    private JacobiAlgorithm() { }
    

    public void SetData(double[,] Matrix, double[] ComplementaryElem, double Precision) {
        matrix = Matrix;
        complementaryElem = ComplementaryElem;
        this.Precision = Precision;
        
    }

    public double[] JacobiAlgorithmImplementation() {
        double[] arrOfSolution = {0, 0, 0};
        for (int i = 0; i < complementaryElem.Length - 1; i++) {
            for (int j = i + 1; j < complementaryElem.Length; j++) {
                for (int k = i + 1; k < complementaryElem.Length; k++) {
                    matrix[j, k] = matrix[j, k] - matrix[i, k] * (matrix[j, i] / matrix[i, i]);
                }
                complementaryElem[j] = complementaryElem[j] - complementaryElem[i] * matrix[j, i] / matrix[i, i];
            }
        }
        Print(matrix);


        for (int i = complementaryElem.Length - 1; i >= 0; i--) {
            double sigma = 0;
            for (var j = i + 1; j < complementaryElem.Length; j++) {
                sigma += matrix[i, j] * arrOfSolution[j];
            }
            arrOfSolution[i] = (complementaryElem[i] - sigma) / matrix[i, i];
        }

        return arrOfSolution;
    }
    
    void Print(double[,] arr) {
        for (int i = 0; i < arr.GetLength(0); i++, 
            Console.WriteLine())
            for (int j = 0; j < arr.GetLength(1); j++)
                Console.Write("{0,3}", arr[i, j]);
    }
}