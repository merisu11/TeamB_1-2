using UnityEngine;
using UnityEngine.EventSystems;

public class ExlainText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject text;
    public GameObject textback;

    void Start()
    {
        text.SetActive(false);
        textback.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.SetActive(true);
        textback.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.SetActive(false);
        textback.SetActive(false);
    }
}
