using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image CursorImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("Game");
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
