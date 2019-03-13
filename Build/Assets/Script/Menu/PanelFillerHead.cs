using UnityEngine.UI;
using UnityEngine;

public class PanelFillerHead : MonoBehaviour
{
    [SerializeField]
    private Image headImage;
    [SerializeField]
    private Text nameText;

    public void FillNewPanel(Sprite spr, string str, AudioClip clp)
    {
        headImage.sprite = spr;
        nameText.text = str;
    }
}
