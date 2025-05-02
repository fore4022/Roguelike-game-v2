using UnityEngine;
public class Tween
{
    public Coroutine coroutine;

    public Tween() { }
    public Tween(Coroutine coroutine)
    {
        this.coroutine = coroutine;
    }
}