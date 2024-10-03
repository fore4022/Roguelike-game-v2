using UnityEngine;
public class AttackOption_UI : MonoBehaviour
{
    private AttackInformation_SO info;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void SetOption(AttackInformation_SO info)
    {
        this.info = info;

        gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        info = null;
    }
}//IPointerEnterHandler