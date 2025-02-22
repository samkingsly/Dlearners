using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetValuesInit : MonoBehaviour
{
    [SerializeField] private List<Sprite> ValueSprites;
    [SerializeField] private List<GameObject> ValueHolders;

    public int WinValue;
    [SerializeField] private int ValueOrderInLayer;

    [SerializeField] private AudioClip VO_AudioClip;
    [SerializeField] private AudioClip CorrectClip;
    [SerializeField] private AudioClip WrongClip;

    [SerializeField] private List<HolderBehaviour> holderBehaviours;

    [SerializeField] private GameObject WinningText;

    public string currentValueTag;

    private void Awake()
    {
        InitializeHolders();
        currentValueTag = "";
        WinningText.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        setValue();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentValueTag);
    }

    public void setValue()
    {
        var valueHolder = new GameObject();
        valueHolder.transform.SetParent(gameObject.transform);
        int Hundred = WinValue / 100;
        int Ten = (WinValue % 100) / 10;
        int One = (WinValue % 10) % 10;

        if(Hundred == 0)
        {
            GameObject hundred = new GameObject();
            CreateValue(ValueSprites.Count - 1, 0, hundred, valueHolder);
        }
        else
        {
            GameObject hundred = new GameObject();
            CreateValue(Hundred, 0, hundred, valueHolder);
        }

        GameObject ten = new GameObject();
        CreateValue(Ten, 1, ten, valueHolder);


        GameObject one = new GameObject();
        CreateValue(One, 2, one, valueHolder);
        


        Debug.Log(Hundred + " " + Ten + " " + one);
    }

    void CreateValue(int valID, int ValHolderID, GameObject obj, GameObject valHolder)
    {
        obj.AddComponent<SpriteRenderer>().sprite = ValueSprites[valID];
        obj.GetComponent<SpriteRenderer>().sortingOrder = ValueOrderInLayer;
        obj.transform.SetParent(valHolder.transform);
        obj.transform.position = ValueHolders[ValHolderID].transform.position;
        obj.transform.localScale = new Vector3(0.25f, 0.25f, 0);
    }

    public void onVOBUttonClicked()
    {
        Debug.Log("Clicked");
        var audComp = gameObject.GetComponent<AudioSource>();
        audComp.Stop();
        audComp.clip = VO_AudioClip;
        audComp.Play();
    }

    void InitializeHolders()
    {
        foreach(var x in holderBehaviours)
        {
            x.ResetHolder();
        }
    }

    public void checkWin()
    {
        int givenValue = 0;
        int InitialMultiPlier = 100;
        foreach (var x in holderBehaviours)
        {
            givenValue += x.Value * InitialMultiPlier;
            InitialMultiPlier /= 10;
        }
        Debug.Log("Given Val" +  givenValue);
        if(givenValue == WinValue)
        {
            StartCoroutine(ShowWin());
        }
        else
        {
            var audComp = gameObject.GetComponent<AudioSource>();
            audComp.Stop();
            audComp.clip = WrongClip;
            audComp.Play();
            foreach (var x in holderBehaviours)
            {
                x.ResetHolder();
            }
        }
    }

    IEnumerator ShowWin()
    {
        WinningText.SetActive(true);
        var audComp = gameObject.GetComponent<AudioSource>();
        audComp.Stop();
        audComp.clip = CorrectClip;
        audComp.Play();
        yield return new WaitForSeconds(5);
        WinningText.SetActive(false);
        foreach(var x in holderBehaviours)
        {
            x.ResetHolder();
        }
    }
}
