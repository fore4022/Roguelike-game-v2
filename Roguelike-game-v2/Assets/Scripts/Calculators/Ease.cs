using UnityEngine;
public static class Ease
{
    private const float _start = 0;
    private const float _end = 1;

    public static float Linear(float value)
    {
        return Mathf.Lerp(_start, _end, value);
    }
    public static float InQuad(float value)
    {
        return _end * value * value + _start;
    }
    public static float OutQuad(float value)
    {
        return -_end * value * (value - 2) + _start;
    }
    public static float InOutQuad(float value)
    {
        value /= .5f;

        if (value < 1)
        {
            return _end * 0.5f * value * value + _start;
        }

        value--;

        return -_end * 0.5f * (value * (value - 2) - 1) + _start;
    }
    public static float InCubic(float value)
    {
        return _end * value * value * value + _start;
    }
    public static float OutCubic(float value)
    {
        value--;

        return _end * (value * value * value + 1) + _start;
    }
    public static float InOutCubic(float value)
    {
        value /= 0.5f;

        if (value < 1)
        {
            return _end * 0.5f * value * value * value + _start;
        }

        value -= 2;

        return _end * 0.5f * (value * value * value + 2) + _start;
    }
    public static float InQuart(float value)
    {
        return _end * value * value * value * value + _start;
    }
    public static float OutQuart(float value)
    {
        value--;

        return -_end * (value * value * value * value - 1) + _start;
    }
    public static float InOutQuart(float value)
    {
        value /= .5f;

        if (value < 1)
        {
            return _end * 0.5f * value * value * value * value + _start;
        }

        value -= 2;

        return -_end * 0.5f * (value * value * value * value - 2) + _start;
    }
    public static float InQuint(float value)
    {
        return _end * value * value * value * value * value + _start;
    }
    public static float OutQuint(float value)
    {
        value--;

        return _end * (value * value * value * value * value + 1) + _start;
    }
    public static float InOutQuint(float value)
    {
        value /= 0.5f;

        if (value < 1)
        {
            return _end * 0.5f * value * value * value * value * value + _start;
        }

        value -= 2;

        return _end * 0.5f * (value * value * value * value * value + 2) + _start;
    }
    public static float InSine(float value)
    {
        return -_end * Mathf.Cos(value * (Mathf.PI * 0.5f)) + _end + _start;
    }
    public static float OutSine(float value)
    {
        return _end * Mathf.Sin(value * (Mathf.PI * 0.5f)) + _start;
    }
    public static float InOutSine(float value)
    {
        return -_end * 0.5f * (Mathf.Cos(Mathf.PI * value) - 1) + _start;
    }
    public static float InExpo(float value)
    {
        return _end * Mathf.Pow(2, 10 * (value - 1)) + _start;
    }
    public static float OutExpo(float value)
    {
        return _end * (-Mathf.Pow(2, -10 * value) + 1) + _start;
    }
    public static float InOutExpo(float value)
    {
        value /= 0.5f;

        if (value < 1)
        {
            return _end * 0.5f * Mathf.Pow(2, 10 * (value - 1)) + _start;
        }

        value--;

        return _end * 0.5f * (-Mathf.Pow(2, -10 * value) + 2) + _start;
    }
    public static float InCirc(float value)
    {
        return -_end * (Mathf.Sqrt(1 - value * value) - 1) + _start;
    }
    public static float OutCirc(float value)
    {
        value--;

        return _end * Mathf.Sqrt(1 - value * value) + _start;
    }
    public static float InOutCirc(float value)
    {
        value /= .5f;

        if (value < 1)
        {
            return -_end * 0.5f * (Mathf.Sqrt(1 - value * value) - 1) + _start;
        }

        value -= 2;

        return _end * 0.5f * (Mathf.Sqrt(1 - value * value) + 1) + _start;
    }
    public static float InBounce(float value)
    {
        float d = 1f;

        return _end - OutBounce(d - value) + _start;
    }
    public static float OutBounce(float value)
    {
        value /= 1f;

        if (value < (1 / 2.75f))
        {
            return _end * (7.5625f * value * value) + _start;
        }
        else if (value < (2 / 2.75f))
        {
            value -= (1.5f / 2.75f);

            return _end * (7.5625f * (value) * value + .75f) + _start;
        }
        else if (value < (2.5 / 2.75))
        {
            value -= (2.25f / 2.75f);

            return _end * (7.5625f * (value) * value + .9375f) + _start;
        }
        else
        {
            value -= (2.625f / 2.75f);

            return _end * (7.5625f * (value) * value + .984375f) + _start;
        }
    }
    public static float InOutBounce(float value)
    {
        float d = 1f;

        if (value < d * 0.5f)
        {
            return InBounce(value * 2) * 0.5f + _start;
        }
        else
        {
            return OutBounce(value * 2 - d) * 0.5f + _end * 0.5f + _start;
        }
    }
    public static float InBack(float value)
    {
        float s = 1.70158f;

        value /= 1;

        return _end * (value) * value * ((s + 1) * value - s) + _start;
    }
    public static float OutBack(float value)
    {
        float s = 1.70158f;

        value = (value) - 1;

        return _end * ((value) * value * ((s + 1) * value + s) + 1) + _start;
    }
    public static float InOutBack(float value)
    {
        float s = 1.70158f;

        value /= 0.5f;

        if ((value) < 1)
        {
            s *= 1.525f;

            return _end * 0.5f * (value * value * (((s) + 1) * value - s)) + _start;
        }

        value -= 2;
        s *= 1.525f;

        return _end * 0.5f * ((value) * value * (((s) + 1) * value + s) + 2) + _start;
    }
    public static float InElastic(float value)
    {
        float d = 1f;
        float p = d * .3f;
        float s;
        float a = 0;

        if (value == 0)
        {
            return _start;
        }

        if ((value /= d) == 1)
        {
            return _start + _end;
        }

        if (a == 0f || a < Mathf.Abs(_end))
        {
            a = _end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(_end / a);
        }

        return -(a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + _start;
    }
    public static float OutElastic(float value)
    {
        float d = 1f;
        float p = d * .3f;
        float s;
        float a = 0;

        if (value == 0)
        {
            return _start;
        }

        if ((value /= d) == 1)
        {
            return _start + _end;
        }

        if (a == 0f || a < Mathf.Abs(_end))
        {
            a = _end;
            s = p * 0.25f;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(_end / a);
        }

        return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + _end + _start);
    }
    public static float InOutElastic(float value)
    {
        float d = 1f;
        float p = d * .3f;
        float s;
        float a = 0;

        if (value == 0)
        {
            return _start;
        }

        if ((value /= d * 0.5f) == 2)
        {
            return _start + _end;
        }

        if (a == 0f || a < Mathf.Abs(_end))
        {
            a = _end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(_end / a);
        }

        if (value < 1)
        {
            return -0.5f * (a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + _start;
        }

        return a * Mathf.Pow(2, -10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) * 0.5f + _end + _start;
    }

    //Additional
    public static float AcceleratedFall(float value)
    {
        return Mathf.Lerp(_start, _end, value * value);
    }
}