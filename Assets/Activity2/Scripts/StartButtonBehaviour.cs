using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButtonBehaviour : MonoBehaviour, IPointerDownHandler, IEndDragHandler, IDragHandler, IPointerUpHandler
{

    [SerializeField] private MainHandler handler;
    [SerializeField] private Canvas canvas;
    [SerializeField] private int audioID;
    private LineRenderer lineRenderer;
    private Vector3 LR_startPos;
    [SerializeField] private Material lineRendererMaterial;
    public bool connected;
    public int value;

    private void OnEnable()
    {
        connected = false;
        handler = FindAnyObjectByType<MainHandler>();
        canvas = FindAnyObjectByType<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition) );
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!connected && lineRenderer != null)
        {
            Destroy(lineRenderer);
            handler.PlayAudioWithID(13);
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(lineRenderer != null)
        {
            Destroy(lineRenderer);
        }
        Debug.Log("OnPointerDown");
        gameObject.AddComponent<LineRenderer>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = lineRendererMaterial;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.sortingOrder = 6;
        lineRenderer.positionCount = 2;
        LR_startPos = transform.position;
        lineRenderer.SetPosition(0, LR_startPos);
        lineRenderer.SetPosition(1, LR_startPos);
        handler.lineRenderer = lineRenderer;


    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!connected && lineRenderer != null && !eventData.dragging)
        {
            Destroy(lineRenderer);
        }
    }

    public void onTextButtonClicked()
    {
        handler.PlayAudioWithID(audioID);
    }
}
