using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public static ButtonScript instance;
    public GameObject instructionsInterface;
    public GameObject menuInterface;
    public GameObject gameInterface;
    public GameObject exitInterface;
    public GameObject levelsGameObject;
    public Button buttonPause;
    public Button exitButton;
    bool isMuteActive = false;
    bool isAudioPausedActive = false;
    private bool isSettingsActive = false;
    //private bool inGame;

    public void OpenSettings()
    {
        isSettingsActive = !isSettingsActive;
        isAudioPausedActive = !isAudioPausedActive;
        Time.timeScale = isSettingsActive ? 0 : 1;
        //GameManager.instance.settingsInterfaces.SetActive(isSettingsActive);
        AudioListener.pause = isAudioPausedActive ? true : false;
    }

    public void BeginGame()
    {
        gameInterface.SetActive(true);
        levelsGameObject.SetActive(true);
        GameManager.instance.isTheLevelStart = true;
        menuInterface.SetActive(false);
        buttonPause.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(false);
        //inGame = true;
    }

    public void HardcoreGame()
    {
        menuInterface.SetActive(false);
        gameInterface.SetActive(true);
        buttonPause.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(false);
        //inGame = true;
    }

    public void Instructions()
    {
        instructionsInterface.SetActive(true);
        menuInterface.SetActive(false);
    }

    public void Back()
    {
        menuInterface.gameObject.SetActive(true);
        instructionsInterface.gameObject.SetActive(false);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //inGame = false;
    }

    //public void LoadGameScene()
    //{
    //    Time.timeScale = 1;
    //    SceneManager.LoadScene(Random.Range(3, 6));
    //    AudioListener.pause = false;
    //}

    public void Mutebutton()
    {
        isMuteActive = !isMuteActive;
        AudioListener.volume = isMuteActive ? 0 : 1;
    }

    //public void RestartCurrentLevel()
    //{
    //    Time.timeScale = 1;
    //    AudioListener.pause = false;
    //    //inGame = false;
    //}

    public void ExitGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        Application.Quit();
        //inGame = false;
    }

    public void CancelExitGame()
    {
        exitInterface.SetActive(false);
        menuInterface.SetActive(true);
    }
    public void ExitQuestion()
    {
        exitInterface.SetActive(true);
        menuInterface.SetActive(false);
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        menuInterface.SetActive(true);
    }
}
