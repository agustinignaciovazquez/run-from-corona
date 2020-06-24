using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    public GameObject pauseMenuUI;
    private AudioManager audioManager;

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject pauseButton;
    
    [SerializeField] private AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseButton.SetActive(true);
        playButton.SetActive(false);
        audioMixer.SetFloat("Volume", 0f);
        pauseMenuUI.SetActive(false);
        audioManager.Play("ButtonClick");
        Time.timeScale = 1f ;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseButton.SetActive(false);
        playButton.SetActive(true);
        audioMixer.SetFloat("Volume", -20f);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f ;
        GameIsPaused = true;
    }
    
    //TODO pitch sound lower when paused
    
    
    public void Restart()
    {
        audioMixer.SetFloat("Volume", 0f);
        audioManager.Play("ButtonClick");
        //TODO
        //StartCoroutine(audioManager.FadeOut("x",0.01f,0.15f));
        //StartCoroutine(audioManager.FadeIn("Airport",0.01f,0.15f));
        Time.timeScale = 1f ;
        SceneManager.LoadScene("GameScene");
    }

    public void QuitToMenu()
    {
        audioMixer.SetFloat("Volume", 0f);
        //TODO
        //StartCoroutine(audioManager.FadeOut("x",0.01f,0.15f));
        audioManager.Play("ButtonClick");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    
    
}
