using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            instPans[i].GetComponent<PanelFillerHead>().FillNewPanel(configPanelHeads.configList[i].headImage, configPanelHeads.configList[i].Name);
            if (i == 0)
                continue;
            float x = instPans[i - 1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset;
            instPans[i].transform.localPosition = new Vector2(x, instPans[i].transform.localPosition.y);
            //Если положения скрола будет не в 0 по y, єто работать не будет
            pansPos[i] = - instPans[i].transform.localPosition;
        }
    }
    private void FixedUpdate()
    {
        float nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {
            float distance =Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
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
        if (isScrolling)
            return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }
    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
    }
    public void TransferCandidateImage()
    {
        Settings.Instance.headImage = configPanelHeads.configList[selectedPanID].headImage;
    }
}
