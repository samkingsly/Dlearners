using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private GameObject DragObject;
    [SerializeField] private GameObject canvas;
    [SerializeField] private SetValuesInit setValuesInit;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
        if (DragObject != null)
        {
            DragObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            setValuesInit.currentValueTag = DragObject.transform.tag;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        if(DragObject != null)
        {
            DragObject.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.GetComponent<Canvas>().scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        if (DragObject != null)
        {
            DragObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        Destroy(DragObject);
        DragObject = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PointerDown");
        DragObject = Instantiate(gameObject);
        DragObject.transform.SetParent(canvas.transform);
        DragObject.transform.position = transform.position;
        DragObject.transform.localScale = transform.localScale;
        Color DragColor = DragObject.GetComponent<Image>().color;
        DragColor.a = 0.7f;
        DragObject.GetComponent<Image>().color = DragColor;
        
    }


}
