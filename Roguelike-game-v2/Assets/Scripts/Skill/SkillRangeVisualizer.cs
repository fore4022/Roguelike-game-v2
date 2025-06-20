using System.Collections;
using UnityEngine;
public class SkillRangeVisualizer : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        Init();
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        StartCoroutine(WarnBeforeCast());
    }
    private void Init()
    {
        _animator = GetComponent<Animator>();
    }
    private IEnumerator WarnBeforeCast()
    {
        yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

        Managers.Game.objectPool.DisableObject(gameObject);
    }
}