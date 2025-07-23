public delegate float EaseDelegate(float f);
public class EaseProvider
{
    public static EaseDelegate Get(EaseType type)
    {
        EaseDelegate del = null;

        switch (type)
        {
            case EaseType.Linear:
                del += Ease.Linear;
                break;
            case EaseType.InQuad:
                del += Ease.InQuad;
                break;
            case EaseType.OutQuad:
                del += Ease.OutQuad;
                break;
            case EaseType.InOutQuad:
                del += Ease.InOutQuad;
                break;
            case EaseType.InCubic:
                del += Ease.InCubic;
                break;
            case EaseType.OutCubic:
                del += Ease.OutCubic;
                break;
            case EaseType.InOutCubic:
                del += Ease.InOutCubic;
                break;
            case EaseType.InQuart:
                del += Ease.InQuart;
                break;
            case EaseType.OutQuart:
                del += Ease.OutQuart;
                break;
            case EaseType.InOutQuart:
                del += Ease.InOutQuart;
                break;
            case EaseType.InQuint:
                del += Ease.InQuint;
                break;
            case EaseType.OutQuint:
                del += Ease.OutQuint;
                break;
            case EaseType.InOutQuint:
                del += Ease.InOutQuint;
                break;
            case EaseType.InSine:
                del += Ease.InSine;
                break;
            case EaseType.OutSine:
                del += Ease.OutSine;
                break;
            case EaseType.InOutSine:
                del += Ease.InOutSine;
                break;
            case EaseType.InExpo:
                del += Ease.InExpo;
                break;
            case EaseType.OutExpo:
                del += Ease.OutExpo;
                break;
            case EaseType.InOutExpo:
                del += Ease.InOutExpo;
                break;
            case EaseType.InCirc:
                del += Ease.InCirc;
                break;
            case EaseType.OutCirc:
                del += Ease.OutCirc;
                break;
            case EaseType.InOutCirc:
                del += Ease.InOutCirc;
                break;
            case EaseType.InBounce:
                del += Ease.InBounce;
                break;
            case EaseType.OutBounce:
                del += Ease.OutBounce;
                break;
            case EaseType.InOutBounce:
                del += Ease.InOutBounce;
                break;
            case EaseType.InBack:
                del += Ease.InBack;
                break;
            case EaseType.OutBack:
                del += Ease.OutBack;
                break;
            case EaseType.InOutBack:
                del += Ease.InOutBack;
                break;
            case EaseType.InElastic:
                del += Ease.InElastic;
                break;
            case EaseType.OutElastic:
                del += Ease.OutElastic;
                break;
            case EaseType.InOutElastic:
                del += Ease.InOutElastic;
                break;
            case EaseType.AcceleratedFall:
                del += Ease.AcceleratedFall;
                break;
        }

        return del;
    }
}