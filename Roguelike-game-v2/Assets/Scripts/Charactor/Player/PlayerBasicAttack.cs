using System.Collections;
using UnityEngine;
public class PlayerBasicAttack
{
    public Coroutine basicAttackCoroutine;

    private BasicAttack_SO basicAttackSO;
    private GameObject prefab;

    private string basicAttackType = "basicAttackSO";
    private float attackSpeed = 1;
    private int createCount = 40;

    public IEnumerator basicAttacking()
    {
        LoadBasicAttackSO();

        yield return new WaitUntil(() => basicAttackSO != null);

        ObjectPool.CreateToPath(basicAttackSO.attackTypePath, createCount);

        yield return new WaitUntil(() => ObjectPool.GetObject(basicAttackSO.attackType.name) != null);

        while (true)
        {
            prefab = ObjectPool.GetOrActiveObject(basicAttackSO.attackType.name);

            Set();

            yield return new WaitForSeconds(attackSpeed);
        }
    }
    public void ChangeBasicAttack(string attackType)
    {
        MonoBehaviour mono = Util.GetMonoBehaviour();

        basicAttackType = attackType;

        if(basicAttackCoroutine != null)
        {
            mono.StopCoroutine(basicAttackCoroutine);
        }

        mono.StartCoroutine(basicAttacking());
    }
    private void Set()
    {
        prefab.GetComponent<BasicAttack>().basicAttackSO = basicAttackSO;
    }
    private async void LoadBasicAttackSO()
    {
        basicAttackSO = await Util.LoadToPath<BasicAttack_SO>(basicAttackType);
    }
}