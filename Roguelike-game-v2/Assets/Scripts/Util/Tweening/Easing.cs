using UnityEngine;
public delegate float EaseDelegate(float f);
public class Easing
{
    private const float _start = 0;
    private const float _end = 1;

    public static EaseDelegate Get(Ease ease)
    {
        EaseDelegate del = null;

        switch (ease)
        {
            case Ease.Linear:
                del += Linear;
                break;
            case Ease.InQuad:
                del += InQuad;
                break;
            case Ease.OutQuad:
                del += OutQuad;
                break;
            case Ease.InOutQuad:
                del += InOutQuad;
                break;
            case Ease.InCubic:
                del += InCubic;
                break;
            case Ease.OutCubic:
                del += OutCubic;
                break;
            case Ease.InOutCubic:
                del += InOutCubic;
                break;
            case Ease.InQuart:
                del += InQuart;
                break;
            case Ease.OutQuart:
                del += OutQuart;
                break;
            case Ease.InOutQuart:
                del += InOutQuart;
                break;
            case Ease.InQuint:
                del += InQuint;
                break;
            case Ease.OutQuint:
                del += OutQuint;
                break;
            case Ease.InOutQuint:
                del += InOutQuint;
                break;
            case Ease.InSine:
                del += InSine;
                break;
            case Ease.OutSine:
                del += OutSine;
                break;
            case Ease.InOutSine:
                del += InOutSine;
                break;
            case Ease.InExpo:
                del += InExpo;
                break;
            case Ease.OutExpo:
                del += OutExpo;
                break;
            case Ease.InOutExpo:
                del += InOutExpo;
                break;
            case Ease.InCirc:
                del += InCirc;
                break;
            case Ease.OutCirc:
                del += OutCirc;
                break;
            case Ease.InOutCirc:
                del += InOutCirc;
                break;
            case Ease.InBounce:
                del += InBounce;
                break;
            case Ease.OutBounce:
                del += OutBounce;
                break;
            case Ease.InOutBounce:
                del += InOutBounce;
                break;
            case Ease.InBack:
                del += InBack;
                break;
            case Ease.OutBack:
                del += OutBack;
                break;
            case Ease.InOutBack:
                del += InOutBack;
                break;
            case Ease.InElastic:
                del += InElastic;
                break;
            case Ease.OutElastic:
                del += OutElastic;
                break;
            case Ease.InOutElastic:
                del += InOutElastic;
                break;
            case Ease.AcceleratedFall:
                del += AcceleratedFall;
                break;
            case Ease.Parabola:
                del += Parabola;
                break;
        }

        return del;
    }
    private static float Linear(float value)
    {
        return Mathf.Lerp(_start, _end, value);
    }
    private static float InQuad(float value)
    {
        return _end * value * value + _start;
    }
    private static float OutQuad(float value)
    {
        return -_end * value * (value - 2) + _start;
    }
    private static float InOutQuad(float value)
    {
        value /= .5f;

        if(value < 1)
        {
            return _end * 0.5f * value * value + _start;
        }

        value--;

        return -_end * 0.5f * (value * (value - 2) - 1) + _start;
    }
    private static float InCubic(float value)
    {
        return _end * value * value * value + _start;
    }
    private static float OutCubic(float value)
    {
        value--;

        return _end * (value * value * value + 1) + _start;
    }
    private static float InOutCubic(float value)
    {
        value /= 0.5f;

        if(value < 1)
        {
            return _end * 0.5f * value * value * value + _start;
        }

        value -= 2;

        return _end * 0.5f * (value * value * value + 2) + _start;
    }
    private static float InQuart(float value)
    {
        return _end * value * value * value * value + _start;
    }
    private static float OutQuart(float value)
    {
        value--;

        return -_end * (value * value * value * value - 1) + _start;
    }
    private static float InOutQuart(float value)
    {
        value /= .5f;

        if (value < 1)
        {
            return _end * 0.5f * value * value * value * value + _start;
        }

        value -= 2;

        return -_end * 0.5f * (value * value * value * value - 2) + _start;
    }
    private static float InQuint(float value)
    {
        return _end * value * value * value * value * value + _start;
    }
    private static float OutQuint(float value)
    {
        value--;

        return _end * (value * value * value * value * value + 1) + _start;
    }
    private static float InOutQuint(float value)
    {
        value /= 0.5f;

        if(value < 1)
        {
            return _end * 0.5f * value * value * value * value * value + _start;
        }
        
        value -= 2;

        return _end * 0.5f * (value * value * value * value * value + 2) + _start;
    }
    private static float InSine(float value)
    {
        return -_end * Mathf.Cos(value * (Mathf.PI * 0.5f)) + _end + _start;
    }
    private static float OutSine(float value)
    {
        return _end * Mathf.Sin(value * (Mathf.PI * 0.5f)) + _start;
    }
    private static float InOutSine(float value)
    {
        return -_end * 0.5f * (Mathf.Cos(Mathf.PI * value) - 1) + _start;
    }
    private static float InExpo(float value)
    {
        return _end * Mathf.Pow(2, 10 * (value - 1)) + _start;
    }
    private static float OutExpo(float value)
    {
        return _end * (-Mathf.Pow(2, -10 * value) + 1) + _start;
    }
    private static float InOutExpo(float value)
    {
        value /= 0.5f;

        if(value < 1)
        {
            return _end * 0.5f * Mathf.Pow(2, 10 * (value - 1)) + _start;
        }

        value--;

        return _end * 0.5f * (-Mathf.Pow(2, -10 * value) + 2) + _start;
    }
    private static float InCirc(float value)
    {
        return -_end * (Mathf.Sqrt(1 - value * value) - 1) + _start;
    }
    private static float OutCirc(float value)
    {
        value--;

        return _end * Mathf.Sqrt(1 - value * value) + _start;
    }
    private static float InOutCirc(float value)
    {
        value /= .5f;

        if(value < 1)
        {
            return -_end * 0.5f * (Mathf.Sqrt(1 - value * value) - 1) + _start;
        }

        value -= 2;

        return _end * 0.5f * (Mathf.Sqrt(1 - value * value) + 1) + _start;
    }
    private static float InBounce(float value)
    {
        float d = 1f;

        return _end - OutBounce(d - value) + _start;
    }
    private static float OutBounce(float value)
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
    private static float InOutBounce(float value)
    {
        float d = 1f;

        if(value < d * 0.5f)
        {
            return InBounce(value * 2) * 0.5f + _start;
        }
        else
        {
            return OutBounce(value * 2 - d) * 0.5f + _end * 0.5f + _start;
        }
    }
    private static float InBack(float value)
    {
        float s = 1.70158f;

        value /= 1;

        return _end * (value) * value * ((s + 1) * value - s) + _start;
    }
    private static float OutBack(float value)
    {
        float s = 1.70158f;

        value = (value) - 1;

        return _end * ((value) * value * ((s + 1) * value + s) + 1) + _start;
    }
    private static float InOutBack(float value)
    {
        float s = 1.70158f;

        value /= 0.5f;

        if((value) < 1)
        {
            s *= 1.525f;

            return _end * 0.5f * (value * value * (((s) + 1) * value - s)) + _start;
        }

        value -= 2;
        s *= 1.525f;

        return _end * 0.5f * ((value) * value * (((s) + 1) * value + s) + 2) + _start;
    }
    private static float InElastic(float value)
    {
        float d = 1f;
        float p = d * .3f;
        float s;
        float a = 0;

        if(value == 0)
        {
            return _start;
        }

        if((value /= d) == 1)
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
    private static float OutElastic(float value)
    {
        float d = 1f;
        float p = d * .3f;
        float s;
        float a = 0;

        if(value == 0)
        {
            return _start;
        }

        if((value /= d) == 1)
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
    private static float InOutElastic(float value)
    {
        float d = 1f;
        float p = d * .3f;
        float s;
        float a = 0;

        if(value == 0)
        {
            return _start;
        }

        if((value /= d * 0.5f) == 2)
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

        if(value < 1)
        {
            return -0.5f * (a * Mathf.Pow(2, 10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p)) + _start;
        }

        return a * Mathf.Pow(2, -10 * (value -= 1)) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) * 0.5f + _end + _start;
    }

    //Additional
    private static float AcceleratedFall(float value)
    {
        return Mathf.Lerp(_start, _end, value * value);
    }
    private static float Parabola(float value)
    {
        return _start + 4 * value * (1 - value);
    }
}