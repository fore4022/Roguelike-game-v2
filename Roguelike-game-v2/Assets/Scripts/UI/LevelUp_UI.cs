using System.Collections;
using UnityEngine;
using TMPro;
public class LevelUp_UI : UserInterface
{
    private TextMeshProUGUI level;
    private Animator animator;

    private void Awake()
    {
        level = Util.GetComponentInChildren<TextMeshProUGUI>(transform);
        animator = GetComponent<Animator>();
    }
    protected override void Start()
    {
        base.Start();

        Managers.Game.playerData.levelUpdate += AnimationPlay;

        gameObject.SetActive(false);
    }
    public void AnimationPlay(int level)
    {
        StartCoroutine(AnimationPlaying(level));
    }
    private IEnumerator AnimationPlaying(int level)//
    {
        yield return null;
    }
}
