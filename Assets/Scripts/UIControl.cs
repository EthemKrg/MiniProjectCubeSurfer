using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    private Music music;
    public GameObject fingerSlideAnim;
    public GameObject gameOverPanel;

    public Button musicButton;
    public Sprite musicOpen;
    public Sprite musicClosed;

    public Button soundButton;
    public Sprite soundOpen;
    public Sprite soundClosed;

    public Button vibrateButton;
    public Sprite vibrateOpen;
    public Sprite vibrateClosed;

    private bool PauseGame = false;


    public static AudioClip bubble, down, gem;
    static AudioSource audioSrc;

    private void Awake()
    {
        // SETTING FOR THE FIRST INSTALL
        if (PlayerPrefs.GetInt("MutedSound") == 0 && PlayerPrefs.GetInt("MutedSound") == 1)
        { }
        else
        {
            PlayerPrefs.SetInt("MutedSound", 1);
        }

        if (PlayerPrefs.GetInt("vibrate") == 0 && PlayerPrefs.GetInt("vibrate") == 1)
        { }
        else
        {
            PlayerPrefs.SetInt("vibrate", 1);
        }
    }

    private void Start()
    {

        bubble = Resources.Load<AudioClip>("bubbleSound");
        down = Resources.Load<AudioClip>("downSound");
        gem = Resources.Load<AudioClip>("gemSound");
        audioSrc = GetComponent<AudioSource>();

        music = GameObject.FindObjectOfType<Music>();
         UpdateIcon();
    }

    private void Update()
    {
        if(PauseGame == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }


    public void PauseMusic()
    {
        music.ToggleSound();
        UpdateIcon();

    }

    public void updateSounds()
    {
        ToggleObjectSound();
        UpdateIcon();

    }

    public void updateVibration()
    {
        ToggleVibrate();
        UpdateIcon();
    }

    // SOUND CONTROL BUTTON
    public void ToggleObjectSound()
    {
        if (PlayerPrefs.GetInt("MutedSound", 0) == 0)
        {
            PlayerPrefs.SetInt("MutedSound", 1);
        }
        else
        {
            PlayerPrefs.SetInt("MutedSound", 0);
        }
    }

    // VIBRATION CONTROL BUTTON
    public void ToggleVibrate()
    {
        if (PlayerPrefs.GetInt("vibrate", 0) == 0)
        {
            PlayerPrefs.SetInt("vibrate", 1);
        }
        else
        {
            PlayerPrefs.SetInt("vibrate", 0);
        }
    }


    // ICON UPDATE
    void UpdateIcon()
    {

        // FOR MUSİC
       if( PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            AudioListener.volume = 1;
            musicButton.GetComponent<Image>().sprite = musicOpen;
        }
        else
        {
            AudioListener.volume = 0;
            musicButton.GetComponent<Image>().sprite = musicClosed;
        }


       // FOR SOUNDS
        if (PlayerPrefs.GetInt("MutedSound", 0) == 0)
        {
            soundButton.GetComponent<Image>().sprite = soundClosed;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = soundOpen;
        }


        // FOR VIBRATION
        if (PlayerPrefs.GetInt("vibrate", 0) == 0)
        {
            vibrateButton.GetComponent<Image>().sprite = vibrateClosed;
        }
        else
        {
            vibrateButton.GetComponent<Image>().sprite = vibrateOpen;
        }


    }


    public static void PlaySound(string clip)
    {
        if (PlayerPrefs.GetInt("MutedSound", 0) == 1)
        {
            switch (clip)
            {
                case "bubbleSound":
                    audioSrc.PlayOneShot(bubble);
                    break;
                case "downSound":
                    audioSrc.PlayOneShot(down);
                    vibrateUp();
                    break;
                case "gemSound":
                    audioSrc.PlayOneShot(gem);
                    break;

            }
        }



    }

    public static void vibrateUp()
    {
        if (PlayerPrefs.GetInt("vibrate") == 1)
        {
            Vibrate.vibrate();
        }


    }







    //START GAME BUTTON

    public void startGameButton()
    {
        SceneManager.LoadScene(0);
    }

    public void closeFingerAnim()
    {
        fingerSlideAnim.gameObject.SetActive(false);
    }


    // GAME OVER PANEL

    public void GameOverPanel()
    {
        gameOverPanel.gameObject.SetActive(true);
    }
    

    public void pauseGame()
	{
        StartCoroutine(settingsPanelTimer());

    }
    public void continueGame()
    {
        PauseGame = false;

    }


    public IEnumerator settingsPanelTimer()
    {
        yield return new WaitForSeconds(.5f);
        PauseGame = true;
    }



}
