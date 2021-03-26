using System;

public class CombinedMethod
{
    public static CombinedMethod instance { get; } = new CombinedMethod();

    private const string ON_INTERVAL_NO_ROOT = "На цьому інтервалі немає кореня";
    private const string WRONG_LIMITS = "Ліва межа не може бути більшою за праву";

    private float leftLim;
    private float rightLim;
    private float epsilon;
    private float Fx(float x) => x * x * x - 2 * x + 7;
    private float dFx(float x) => 3 * x * x - 2;
    private float d2Fx(float x) => 6 * x;
    
    public void SetInputs(float leftLim, float rightLim, float epsilon)
    {
        if (leftLim > rightLim)
        {
            throw new Exception(WRONG_LIMITS);
        }
        this.leftLim = leftLim;
        this.rightLim = rightLim;
        this.epsilon = epsilon;
    }

    public float FindRoot()
    {
        return FindRootWithCombinedMethod(leftLim, rightLim);
    }

    private float FindRootWithCombinedMethod(float a, float b)
    {
        if (Fx(a) * Fx(b) > 0)
        {
            throw new Exception(ON_INTERVAL_NO_ROOT);
        }

        int k = 0;
        while (Math.Abs(b - a) >= epsilon)
        {
            float temp = (b - a) / 2f;

            if (dFx(temp) * d2Fx(temp) < 0)
            {
                b -= Fx(b) * (b - a) / (Fx(b) - Fx(a));
                a -= Fx(a) / dFx(a);
            }
            else
            {
                a -= Fx(a) * (b - a) / (Fx(b) - Fx(a));
                b -= Fx(b) / dFx(b);
            }
            k++;
        }
        return (b + a) / 2f;
    }
}
