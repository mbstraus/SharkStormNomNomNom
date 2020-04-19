using UnityEngine;
using TMPro;

public class SwimSpeedLabel : MonoBehaviour
{
    public TextMeshProUGUI TextObject;

    // Update is called once per frame
    void Update()
    {
        TextObject.text = GameSettingsManager.instance.GetSwimSpeedLabel();
    }
}
