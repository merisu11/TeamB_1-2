using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipPanel;

    void Start()
    {
        tooltipPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipPanel.SetActive(false);
    }
}
