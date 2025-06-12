using System.Collections;
public class ExceptionMonster_A : BasicMonster
{
    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(TestA());
    }
    private IEnumerator TestA()
    {
        yield return null;
    }
}