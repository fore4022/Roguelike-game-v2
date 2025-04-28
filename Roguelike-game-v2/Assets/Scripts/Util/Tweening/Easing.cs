using UnityEngine;
public class Easing
{
    private const float start = 0;
    private const float end = 1;

    public float Linear(float value)
    {
        return Mathf.Lerp(start, end, value);
    }
    public float InQuad(float value)
    {
        return end * value * value + start;
    }
    public float OutQuad(float value)
    {
        return -end * value * (value - 2) + start;
    }
    public float InOutQuad(float value)
    {
        value /= .5f;

        if(value < 1)
        {
            return end * 0.5f * value * value + start;
        }

        value--;

        return -end * 0.5f * (value * (value - 2) - 1) + start;
    }
    public float InCubic(float value)
    {
        return end * value * value * value + start;
    }
    public float OutCubic(float value)
    {
        value--;

        return end * (value * value * value + 1) + start;
    }
    public float InOutCubic(float value)
    {
        value /= 0.5f;

        if(value < 1)
        {
            return end * 0.5f * value * value * value + start;
        }

        value -= 2;

        return end * 0.5f * (value * value * value + 2) + start;
    }
    public float InQuart(float value)
    {
        return end * value * value * value * value + start;
    }
    public float OutQuart(float value)
    {
        value--;

        return -end * (value * value * value * value - 1) + start;
    }
    public float InOutQuart(float value)
    {
        value /= .5f;

        if (value < 1)
        {
            return end * 0.5f * value * value * value * value + start;
        }

        value -= 2;

        return -end * 0.5f * (value * value * value * value - 2) + start;
    }
    public float InQuint(float value)
    {
        return end * value * value * value * value * value + start;
    }
    public float OutQuint(float value)
    {
        value--;

        return end * (value * value * value * value * value + 1) + start;
    }
    public float InOutQuint(float value)
    {
        value /= 0.5f;

        if(value < 1)
        {
            return end * 0.5f * value * value * value * value * value + start;
        }
        
        value -= 2;

        return end * 0.5f * (value * value * value * value * value + 2) + start;
    }
    public float InSine(float value)
    {
        return -end * Mathf.Cos(value * (Mathf.PI * 0.5f)) + end + start;
    }
    public float OutSine(float value)
    {
        return end * Mathf.Sin(value * (Mathf.PI * 0.5f)) + start;
    }
    public float InOutSine(float value)
    {
        return -end * 0.5f * (Mathf.Cos(Mathf.PI * value) - 1) + start;
    }
    public float InExpo(float value)
    {
        return end * Mathf.Pow(2, 10 * (value - 1)) + start;
    }
    public float OutExpo(float value)
    {
        return end * (-Mathf.Pow(2, -10 * value) + 1) + start;
    }
    public float InOutExpo(float value)
    {
        value /= 0.5f;

        if(value < 1)
        {
            return end * 0.5f * Mathf.Pow(2, 10 * (value - 1)) + start;
        }

        value--;

        return end * 0.5f * (-Mathf.Pow(2, -10 * value) + 2) + start;
    }
    public float InCirc(float value)
    {
        return -end * (Mathf.Sqrt(1 - value * value) - 1) + start;
    }
    public float OutCirc(float value)
    {
        value--;

        return end * Mathf.Sqrt(1 - value * value) + start;
    }
    public float InOutCirc(float value)
    {
        value /= .5f;

        if(value < 1)
        {
            return -end * 0.5f * (Mathf.Sqrt(1 - value * value) - 1) + start;
        }

        value -= 2;

        return end * 0.5f * (Mathf.Sqrt(1 - value * value) + 1) + start;
    }
    public float InBounce(float value)
    {
        float d = 1f;

        return end - OutBounce(d - value) + start;
    }
    public float OutBounce(float value)
    {
        value /= 1f;

        if (value < (1 / 2.75f))
        {
            return end * (7.5625f * value * value) + start;
        }
        else if (value < (2 / 2.75f))
        {
            value -= (1.5f / 2.75f);

            return end * (7.5625f * (value) * value + .75f) + start;
        }
        else if (value < (2.5 / 2.75))
        {
            value -= (2.25f / 2.75f);

            return end * (7.5625f * (value) * value + .9375f) + start;
        }
        else
        {
            value -= (2.625f / 2.75f);

            return end * (7.5625f * (value) * value + .984375f) + start;
        }
    }
    public float InOutBounce(float value)
    {
        float d = 1f;

        if(value < d * 0.5f)
        {
            return InBounce(value * 2) * 0.5f + start;
        }
        else
        {
            return OutBounce(value * 2 - d) * 0.5f + end * 0.5f + start;
        }
    }
    public float InBack(float value)
    {
        float s = 1.70158f;

        value /= 1;

        return end * (value) * value * ((s + 1) * value - s) + start;
    }
    public float OutBack(float value)
    {
        float s = 1.70158f;

        value = (value) - 1;

        return end * ((value) * value * ((s + 1) * value + s) + 1) + start;
    }
    public float InOutBack(float value)
    {
        float s = 1.70158f;

        value /= 0.5f;

        if((value) < 1)
        {
            s *= 1.525f;

            return end * 0.5f * (value * value * (((s) + 1) * value - s)) + start;
        }

        value -= 2;
        s *= 1.525f;

        return end * 0.5f * ((value) * value * (((s) + 1) * value + s) + 2) + start;
    }
    public float InElastic(float value)
    {
        float d = 1f;
        float p = d * .3f;
        float s;
        float a = 0;

        if(value == 0)
        {
            return start;
        }

        if((value /= d) == 1)
        {
            return start + end;
        }

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        return -(a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
    }
    public float OutElastic(float value)
    {
        float d = 1f;
        float p = d * .3f;
        float s;
        float a = 0;

        if(value == 0)
        {
            return start;
        }

        if((value /= d) == 1)
        {
            return start + end;
        }

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p * 0.25f;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + end + start);
    }
    public float InOutElastic(float value)
    {
        float d = 1f;
        float p = d * .3f;
        float s;
        float a = 0;

        if(value == 0)
        {
            return start;
        }

        if((value /= d * 0.5f) == 2)
        {
            return start + end;
        }

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        if(value < 1)
        {
            return -0.5f * (a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + start;
        }

        return a * Mathf.Pow(2, -10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) * 0.5f + end + start;
    }
}