using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(StatSelection))]
public class StatUpgrade_UI : UserInterface
{
    public List<FileReference> files;
    public GameObject background;

    private const float duration = 0.2f;

    private bool toggle = false;

    // StatElement

    // stat points

    public override void SetUserInterface()
    {
        for(int i = 0; i < files.Count; i++)
        {
            // Instantiate();
        }

        background.SetActive(false);
    }
    public void ToggleUI()
    {
        toggle = !toggle;

        background.SetActive(toggle);
        
        if(toggle)
        {
            transform.SetPosition(new(0, 35), duration, Ease.OutSine);
        }
        else
        { 
            transform.SetPosition(new(0, -1500), duration, Ease.OutSine);
        }
    }
}