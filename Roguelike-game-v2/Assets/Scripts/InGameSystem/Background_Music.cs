using System.Collections;
using UnityEngine;
public class Background_Music : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        StartCoroutine(Setting());
    }
    private IEnumerator Setting()
    {
        yield return new WaitUntil(() => Managers.Game.stageInformation != null);

        audioSource.clip = Managers.Game.stageInformation.bgm;

        yield return new WaitUntil(() => Managers.Game.Playing);

        audioSource.Play();
    }
}