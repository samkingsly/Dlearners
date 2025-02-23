using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainHandler : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public int score;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject valueHolder;
    [SerializeField] private GameObject valueDHolder;

    [SerializeField] private List<GameObject> Values;
    [SerializeField] private List<GameObject> ValuesD;

    [SerializeField] private GameObject WinningText;

    private int vHoldCc;
    private int vDHoldCc;

    List<int> RandomHolder;

    public List<AudioClip> AudioClips;
    private AudioSource audSource;

    private void OnEnable()
    {
        vHoldCc = valueHolder.transform.childCount;
        vDHoldCc = valueDHolder.transform.childCount;
        audSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        createLevel();
    }



    public void UpdateScore()
    {
        scoreText.text = score.ToString() + " / 6";
    }

    public void createLevel()
    {
        restartLevel();

        RandomHolder = new List<int>() {0, 1, 2, 3, 4, 5};

        shuffleList();

        for(int i = 0; i < vHoldCc; i++)
        {
            var go = Instantiate(Values[i]);
            go.transform.SetParent(valueHolder.transform.GetChild(RandomHolder[i]));
            go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            go.GetComponent<RectTransform>().localScale = Vector2.one;
        }

        shuffleList();

        for (int i = 0; i < vDHoldCc; i++)
        {
            var go = Instantiate(ValuesD[i]);
            go.transform.SetParent(valueDHolder.transform.GetChild(RandomHolder[i]));
            go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            go.GetComponent<RectTransform>().localScale = Vector2.one;

        }
    }

    void shuffleList()
    {
        int shuffleTimes = 4;
        for(int i = 0; i < shuffleTimes; i++)
        {
            for(int x = 0; x < RandomHolder.Count; x++)
            {
                int r = (int)(Random.value * (RandomHolder.Count - x));
                int temp = RandomHolder[r];
                RandomHolder[r] = RandomHolder[x];
                RandomHolder[x] = temp;
            }
        }
    }

    void restartLevel()
    {
        WinningText.SetActive(false);
        foreach(Transform x in valueHolder.transform)
        {
            if(x.childCount != 0)
            {
                Destroy(x.GetChild(0).gameObject);
            }
        }
        foreach (Transform x in valueDHolder.transform)
        {
            if(x.childCount != 0)
            {
                Destroy(x.GetChild(0).gameObject);
            }
        }

        score = 0;
        UpdateScore();
    }

    public void CheckWin()
    {
        if(score == 6)
        {
            PlayAudioWithID(12);
            StartCoroutine(DisplaySuccess());
            
        }
    }

    IEnumerator DisplaySuccess()
    {
        WinningText.SetActive(true);
        yield return new WaitForSeconds(5f);
        WinningText.SetActive(false);
        createLevel();
    }

    public void PlayAudioWithID(int i)
    {
        audSource.Stop();

        audSource.clip = AudioClips[i];

        audSource.Play();


    }
}
