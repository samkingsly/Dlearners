using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HolderBehaviour : MonoBehaviour, IDropHandler, IPointerDownHandler
{

    public int Value;

    [SerializeField] private GameObject holder;
    [SerializeField] private SetValuesInit setValuesInit;
    [SerializeField] private string valueTag;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void DisplayValues()
    {
        if(Value != 0)
        {
            holder.transform.GetChild(Value - 1).gameObject.SetActive(true);
        }
        

        switch (Value)
        {
            case 1:
                holder.transform.localPosition = new Vector3(0, -1, 0);
                holder.transform.localScale = new Vector3(0.2f, 0.2f, 0);
                break;
            case 2:
                holder.transform.localPosition = new Vector3(-0.75f, -1, 0);
                holder.transform.localScale = new Vector3(0.15f, 0.15f, 0);
                break;
            case 3:
                holder.transform.localPosition = new Vector3(-1, -1, 0);
                holder.transform.localScale = new Vector3(0.1f, 0.1f, 0);
                break;
            case 4:
                holder.transform.localPosition = new Vector3(-1, -0.5f, 0);
                break;
            case 7:
                holder.transform.localPosition = new Vector3(-1, -0, 0);
                break;
        }
    }

    public void ResetHolder()
    {
        Value = 0;
        holder.transform.localPosition = new Vector3(0, -1, 0);
        holder.transform.localScale = new Vector3(0.2f, 0.2f, 0);
        foreach (Transform t in holder.transform)
        {
            t.gameObject.SetActive(false);
        }

    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Droped");
        if(Value < 10)
        {
            if (setValuesInit.currentValueTag == valueTag)
            {
                Value += 1;
                DisplayValues();
            }  
        }
        setValuesInit.currentValueTag = "";

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Value > 0)
        {
            Value -= 1;
            holder.transform.GetChild(Value).gameObject.SetActive(false);
            DisplayValues();
        } 
    }
}
