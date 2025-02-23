using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndButtonBehaviour : MonoBehaviour, IDropHandler
{
    [SerializeField] private MainHandler mainHandler;
    [SerializeField] private int value;
    [SerializeField] private int audioID;

    private void OnEnable()
    {
        mainHandler = FindAnyObjectByType<MainHandler>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        var sBB = mainHandler.lineRenderer.gameObject.GetComponent<StartButtonBehaviour>();
        if(mainHandler.lineRenderer != null && sBB.value == value)
        {
            mainHandler.lineRenderer.SetPosition(1,transform.position);
            sBB.connected = true;
            mainHandler.score += 1;
            mainHandler.UpdateScore();
            mainHandler.PlayAudioWithID(12);
            mainHandler.CheckWin();
        }
    }

    public void onTextButtonClicked()
    {
        mainHandler.PlayAudioWithID(audioID);
    }
}
