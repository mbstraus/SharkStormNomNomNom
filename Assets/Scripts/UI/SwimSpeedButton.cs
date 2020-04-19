using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwimSpeedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image CursorImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameSettingsManager.instance.ChangeSwimSpeed();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorImage.gameObject.SetActive(true);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        CursorImage.gameObject.SetActive(false);
    }
}
