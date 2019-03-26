using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum GameState { Play, Pause };
public class HUD : MonoBehaviour
{
    [Header("Lose Window setup")]
    [SerializeField]
    private Image LoseWindow;
    [SerializeField]
    private Image HeadImage;
    [SerializeField]
    private Text finalScoresCount;
    [Space]
    [SerializeField]
    private GameObject MenuWindow;
    [SerializeField]
    private Text highScoreCount;
    [SerializeField]
    private Text dashCountText;
    [SerializeField]
    private Text distanceCountText;
    [SerializeField]
    private Text _plusMinusDash;
    public Text LoseWarning;
    public Text LoseTimer;
    public Text SorryText;

    bool _pause = false;

    private string sceneFolder = "Scenes/";
    public void changeTextDash()
    {
        dashCountText.text = "Вниз :" + PlayerController.Instance.DashAvailable.ToString();
    }

    public void changeTextDistance()
    {
        distanceCountText.text = "Рейтинг " + GameController.Instance.myHead.CalculateScore() + "%";
    }

    public void LoadLevel(string Level)
    {
        SceneManager.LoadScene(sceneFolder + Level);
    }

    public void ChooseCandidate()
    {
        Settings.Instance.headSelection = true;
        LoadLevel("Menu");        
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Pause(bool trigger)
    {
        _pause = trigger;
        if (_pause)
        {
            Time.timeScale = 0.0f;
        }
        else if (!_pause)
        {
            Time.timeScale = 1f;
            
        }
    }
    public void SetHighScore()
    {
        if (PlayerPrefs.GetInt("PlayerBestScore") < int.Parse(GameController.Instance.myHead.CalculateScore()))
        {
            PlayerPrefs.SetInt("PlayerBestScore", int.Parse(GameController.Instance.myHead.CalculateScore()));
            highScoreCount.text = "Best Score:" + PlayerPrefs.GetInt("PlayerBestScore").ToString();
        }
    }

    public void ShowHighScore()
    {
        highScoreCount.text = "Best Score:" + PlayerPrefs.GetInt("PlayerBestScore").ToString();
    }

    public void LoseWindowShow()
    {
        GameController.Instance.soundControll.StopMusic();
        LoseWarning.gameObject.SetActive(false);
        LoseWindow.gameObject.SetActive(true);
        finalScoresCount.text = "Рейтинг " + GameController.Instance.myHead.CalculateScore() + "%";
        HeadImage.sprite = Settings.Instance.headImage;
        StartCoroutine(GameOverPanelFade());
    }
    IEnumerator GameOverPanelFade()
    {
      
        float time = 5f;
        float maxValue = 1f;
        float minValue = 0f;
        float deltaTime = maxValue / 100 * time;
        var color = LoseWindow.color;
        color.a = minValue;
        LoseWindow.color = color;
        while (LoseWindow.color.a < 1f)
        {
            color = LoseWindow.color;
            color.a += deltaTime;
            LoseWindow.color = color;
            yield return null;// new WaitForSeconds(deltaTime); 
        }
    }

    public void ChangeDash(string text, float time)
    {
        StartCoroutine(ChangeDashFade(text,time));
    }


    IEnumerator ChangeDashFade(string text, float time)
    {
        _plusMinusDash.text = text;
        _plusMinusDash.gameObject.SetActive(true);
        float speed = 1 / time;
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            _plusMinusDash.color = Color.Lerp(Color.white, Color.clear, percent);
            yield return null;
        }
        _plusMinusDash.color = Color.white;
        _plusMinusDash.gameObject.SetActive(false);
    }

}

