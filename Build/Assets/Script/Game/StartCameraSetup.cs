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
        GameController.Instance.GameOver.AddListener(GetCameraCentr);
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
    private void GetCameraCentr()
    {
        StartCoroutine(TranslateCentr());
    }
    IEnumerator TranslateLeft()
    {
        while (ft.m_ScreenX > 0.2f)
        {
            ft.m_ScreenX -= 0.0015f;
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

    //private void FixedUpdate()
    //{
    //    print(GameController.Instance.myHead.GetYDirection());
    //    if (GameController.Instance.myHead)
    //    {
    //        if ((GameController.Instance.myHead.LastVelocityValue * GameController.Instance.myHead.GetYDirection() < 0))
    //        {
    //            YDirectonCameraFollow(GameController.Instance.myHead.GetYDirection());
    //        }
    //        GameController.Instance.myHead.LastVelocityValue = GameController.Instance.myHead.GetYDirection();
    //    }
    //}
    //public void YDirectonCameraFollow(float direction)
    //{
    //    if (Mathf.Abs(direction) < 5)
    //    {
    //        StopCoroutine("YDirectonFlagFolow");
    //        StartCoroutine(YDirectonFlagFolow(1));
    //    }
    //    else
    //    {
    //        StopCoroutine("YDirectonFlagFolow");
    //        StartCoroutine(YDirectonFlagFolow(direction));
    //    }
    //}

    //public IEnumerator YDirectonFlagFolow(float dir)
    //{
    //    if (dir > 0)
    //        while (ft.m_ScreenY < 0.6f)
    //        {
    //            ft.m_ScreenY += 0.005f;
    //            yield return null;
    //        }
    //    else
    //        while (ft.m_ScreenY > 0.3f)
    //        {
    //            ft.m_ScreenY -= 0.005f;
    //            yield return null;
    //        }
    //}
}
