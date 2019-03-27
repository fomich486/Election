using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(ConfigPanelHeads))]
public class SnapScrolling : MonoBehaviour
{
    [SerializeField]
    private ConfigPanelHeads configPanelHeads;
    private int panCount = 0;
    public int panOffset;
    public GameObject panPrefab;
    public float snapSpeed;
    public float scaleOffset;
    public float scaleSpeed;
    private GameObject[] instPans;
    private Vector2[] pansPos;
    private Vector2[] pansScale;
    private RectTransform contentRect;
    private Vector2 contentVector;
    public int selectedPanID;
    private bool isScrolling;
    private bool startSetup = true;
    void Start()
    {
        panCount = configPanelHeads.configList.Count;
        contentRect = GetComponent<RectTransform>();
        instPans = new GameObject[panCount];
        pansPos = new Vector2[panCount];
        pansScale = new Vector2[panCount];
        for (int i = 0; i < panCount; i++)
        {
            instPans[i] = Instantiate(panPrefab, transform, false);
            instPans[i].GetComponent<PanelFillerHead>().FillNewPanel(configPanelHeads.configList[i].headImage, configPanelHeads.configList[i].Name, configPanelHeads.configList[i].headSong, configPanelHeads.configList[i].HitSound);
            instPans[i].GetComponent<Button>().onClick.AddListener(TransferCandidateHitSong);
            instPans[i].GetComponent<Button>().onClick.AddListener(TransferCandidateImage);
            instPans[i].GetComponent<Button>().onClick.AddListener(TransferCandidateSong);
            instPans[i].GetComponent<Button>().onClick.AddListener(FindObjectOfType<UIMenu>().LoadGame);
            if (i == 0)
                continue;
            float x = instPans[i - 1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset;
            instPans[i].transform.localPosition = new Vector2(x, instPans[i].transform.localPosition.y);
            pansPos[i] = - instPans[i].transform.localPosition;
        }
        selectedPanID = Random.Range(0, panCount);
        StartCoroutine(EnableControll());
        print(selectedPanID);
    }
    private void FixedUpdate()
    {
        if (!startSetup)
        {
            ValueChange();
        }
            if (isScrolling)
            return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }
    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
    }

    public void ValueChange()
    {
        if (!startSetup)
        {
            print("Inside valuechange");
            float nearestPos = float.MaxValue;
            for (int i = 0; i < panCount; i++)
            {
                float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
                if (distance < nearestPos)
                {
                    nearestPos = distance;
                    selectedPanID = i;
                }
                float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
                pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
                pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
                instPans[i].transform.localScale = pansScale[i];
            }
        }
    }

    IEnumerator EnableControll()
    {
        while (Mathf.Abs(contentRect.anchoredPosition.x - pansPos[selectedPanID].x) > 10)
            yield return new WaitForFixedUpdate();
        print("Coroutine finished");
        for (int i = 0; i < 1000; i++)
        {
            contentRect.anchoredPosition = pansPos[selectedPanID] + Vector2.left * Random.Range(-0.1f, 0.1f);
        }
        startSetup = false;
    }

    public void TransferCandidateImage()
    {
        Settings.Instance.headImage = configPanelHeads.configList[selectedPanID].headImage;
       
    }
    public void TransferCandidateSong()
    {
        Settings.Instance.headSong = configPanelHeads.configList[selectedPanID].headSong;
    }

    public void TransferCandidateHitSong()
    {
        Settings.Instance.HitSong = configPanelHeads.configList[selectedPanID].HitSound;
    }

    public void ChangeCandidate(int direction) {
        startSetup = true;
        if (selectedPanID + direction > panCount - 1)
            selectedPanID = 0;
        else if (selectedPanID + direction < 0)
            selectedPanID = panCount - 1;
        else
            selectedPanID += direction;
        StartCoroutine(EnableControll());
    }
}
