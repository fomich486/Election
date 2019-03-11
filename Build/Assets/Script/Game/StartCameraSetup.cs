using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class StartCameraSetup : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public CinemachineFramingTransposer ft;

    private void Start()
    {
        GameController.Instance.AfterShootAction.AddListener(GetCameraBack);
        GameController.Instance.GameOver.AddListener(GetCAmerCentr);
        ft = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        ft.m_ScreenX = 0.5f;
        ft.m_ScreenY= 0.5f;
        vcam.m_Lens.FieldOfView = 30;
        StartCoroutine(CamZoomOut());
    }
    IEnumerator CamZoomOut()
    {
        yield return new WaitForSeconds(0.75f);
        while (vcam.m_Lens.FieldOfView < 65)
        {
            vcam.m_Lens.FieldOfView++;
            yield return new WaitForSeconds(0.01f);
        }
        GameController.Instance.ActivatePlayerControl?.Invoke();
    }
    private void GetCameraBack()
    {
        StartCoroutine(TranslateLeft());
    }
    private void GetCAmerCentr()
    {
        StartCoroutine(TranslateCentr());
    }
    IEnumerator TranslateLeft()
    {
        while (ft.m_ScreenX > 0.250f)
        {
            ft.m_ScreenX -= 0.0015f;
            yield return new WaitForSeconds(0.01f);
        }
        while (ft.m_ScreenY < 0.7f)
        {
            ft.m_ScreenY += 0.0005f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator TranslateCentr()
    {
        while (ft.m_ScreenX < 0.5f)
        {
            ft.m_ScreenX += 0.005f;
            yield return new WaitForSeconds(0.01f);
        }
        while (ft.m_ScreenY > 0.5f)
        {
            ft.m_ScreenY -= 0.005f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
