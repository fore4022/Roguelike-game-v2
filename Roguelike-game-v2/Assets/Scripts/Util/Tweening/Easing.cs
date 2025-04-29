using UnityEngine;
using static Tween;
public class Easing
{
    private const float start = 0;
    private const float end = 1;

    public EaseDelegate Get(Ease ease)
    {
        EaseDelegate del = null;

        switch (ease)
        {
            case Ease.InLinear:
                del += InLinear;
                break;
            case Ease.OutLinear:
                del += OutLinear;
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
        }

        return del;
    }
    private float InLinear(float value)
    {
        return Mathf.Lerp(start, end, value);
    }
    private float OutLinear(float value)
    {
        return Mathf.Lerp(end, start, value);
    }
    private float InQuad(float value)
    {
        return end * value * value + start;
    }
    private float OutQuad(float value)
    {
        return -end * value * (value - 2) + start;
    }
    private float InOutQuad(float value)
    {
        value /= .5f;

        if(value < 1)
        {
            return end * 0.5f * value * value + start;
        }

        value--;

        return -end * 0.5f * (value * (value - 2) - 1) + start;
    }
    private float InCubic(float value)
    {
        return end * value * value * value + start;
    }
    private float OutCubic(float value)
    {
        value--;

        return end * (value * value * value + 1) + start;
    }
    private float InOutCubic(float value)
    {
        value /= 0.5f;

        if(value < 1)
        {
            return end * 0.5f * value * value * value + start;
        }

        value -= 2;

        return end * 0.5f * (value * value * value + 2) + start;
    }
    private float InQuart(float value)
    {
        return end * value * value * value * value + start;
    }
    private float OutQuart(float value)
    {
        value--;

        return -end * (value * value * value * value - 1) + start;
    }
    private float InOutQuart(float value)
    {
        value /= .5f;

        if (value < 1)
        {
            return end * 0.5f * value * value * value * value + start;
        }

        value -= 2;

        return -end * 0.5f * (value * value * value * value - 2) + start;
    }
    private float InQuint(float value)
    {
        return end * value * value * value * value * value + start;
    }
    private float OutQuint(float value)
    {
        value--;

        return end * (value * value * value * value * value + 1) + start;
    }
    private float InOutQuint(float value)
    {
        value /= 0.5f;

        if(value < 1)
        {
            return end * 0.5f * value * value * value * value * value + start;
        }
        
        value -= 2;

        return end * 0.5f * (value * value * value * value * value + 2) + start;
    }
    private float InSine(float value)
    {
        return -end * Mathf.Cos(value * (Mathf.PI * 0.5f)) + end + start;
    }
    private float OutSine(float value)
    {
        return end * Mathf.Sin(value * (Mathf.PI * 0.5f)) + start;
    }
    private float InOutSine(float value)
    {
        return -end * 0.5f * (Mathf.Cos(Mathf.PI * value) - 1) + start;
    }
    private float InExpo(float value)
    {
        return end * Mathf.Pow(2, 10 * (value - 1)) + start;
    }
    private float OutExpo(float value)
    {
        return end * (-Mathf.Pow(2, -10 * value) + 1) + start;
    }
    private float InOutExpo(float value)
    {
        value /= 0.5f;

        if(value < 1)
        {
            return end * 0.5f * Mathf.Pow(2, 10 * (value - 1)) + start;
        }

        value--;

        return end * 0.5f * (-Mathf.Pow(2, -10 * value) + 2) + start;
    }
    private float InCirc(float value)
    {
        return -end * (Mathf.Sqrt(1 - value * value) - 1) + start;
    }
    private float OutCirc(float value)
    {
        value--;

        return end * Mathf.Sqrt(1 - value * value) + start;
    }
    private float InOutCirc(float value)
    {
        value /= .5f;

        if(value < 1)
        {
            return -end * 0.5f * (Mathf.Sqrt(1 - value * value) - 1) + start;
        }

        value -= 2;

        return end * 0.5f * (Mathf.Sqrt(1 - value * value) + 1) + start;
    }
    private float InBounce(float value)
    {
        float d = 1f;

        return end - OutBounce(d - value) + start;
    }
    private float OutBounce(float value)
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
    private float InOutBounce(float value)
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
    private float InBack(float value)
    {
        float s = 1.70158f;

        value /= 1;

        return end * (value) * value * ((s + 1) * value - s) + start;
    }
    private float OutBack(float value)
    {
        float s = 1.70158f;

        value = (value) - 1;

        return end * ((value) * value * ((s + 1) * value + s) + 1) + start;
    }
    private float InOutBack(float value)
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
    private float InElastic(float value)
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
    private float OutElastic(float value)
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
    private float InOutElastic(float value)
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