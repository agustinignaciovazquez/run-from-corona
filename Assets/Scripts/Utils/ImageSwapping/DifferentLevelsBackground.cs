using System;
using System.Collections.Generic;
using UnityEngine;

public class DifferentLevelsBackground : ScrollingBackground
{
    
    private ObjectPoolSpawner objectPoolSpawner;
    private int indexCurrentBackground;
    private float distanceToNextBackground;
    private SpriteRenderer spriteRenderer;
    private bool shouldTransition;
    private bool fadeIn;
    private BackgroundSettings backgroundSettings;
    private AudioManager audioManager;

    protected override void Awake()
    {
        base.Awake();
        //totalDistance = GetTotalDistance();
        
    }

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        indexCurrentBackground = 0;
        shouldTransition = false;
        fadeIn = false;
        
        objectPoolSpawner = ObjectPoolSpawner.GetSharedInstance;
        backgroundSettings = BackgroundSettings.Instance;
        distanceToNextBackground = backgroundSettings.Backgrounds[indexCurrentBackground].DistanceToShow;
        audioManager = FindObjectOfType<AudioManager>();
    }

    protected override void RepositionBackground(Transform backgroundTransform)
    {
        base.RepositionBackground(backgroundTransform);
       
        if (PlayerController.DistanceTraveled > distanceToNextBackground)
        {
            ChangeBackground();
        }
    }

    private void ChangeBackground()
    {
        int nextIndex = (indexCurrentBackground + 1) % backgroundSettings.Backgrounds.Count;
        if (nextIndex != PlayerController.BackgroundIndex)
        {
            PlayerController.BackgroundIndex = nextIndex;
            shouldTransition = true;
            fadeIn = true;
        }
      
        BackgroundSettings.ScenarioBackground nextBackground = backgroundSettings.Backgrounds[nextIndex];
        spriteRenderer.sprite = nextBackground.BackgroundImage;
        indexCurrentBackground = nextIndex;
        
        distanceToNextBackground += nextBackground.DistanceToShow;
    }
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && shouldTransition)
        {
            DoTransition();
            
        }
    }

    private void DoTransition()
    {
        var prevBackground = indexCurrentBackground - 1;
        if (prevBackground < 0)
            prevBackground = backgroundSettings.Backgrounds.Count - 1;
        
        if (fadeIn)
        {
            DoFadeInTransition(prevBackground);
        }
        else
        {
           DoFadeOutTransition();
        }
    }

    private void DoFadeInTransition(int prevBackground)
    {
        PlayerController.Inmunity = true;
        objectPoolSpawner.ResetPool();
        PlayerController.ScrollingBackgroundSpeed *= 3;
        
        backgroundSettings.SceneTransition.FadeOut();
        fadeIn = false;
        
        string songName = backgroundSettings.Backgrounds[prevBackground].BackgroundImage.name;
        Debug.Log(songName);
        StartCoroutine(audioManager.FadeOut(songName,0.1f));
        
        backgroundSettings.TeleportEffect.SetActive(true);
        backgroundSettings.StageText.SetActive(true);
        
        StageTextSingleton stageTextSingleton = StageTextSingleton.SharedInstance;
        stageTextSingleton.AddStage();
    }

    private void DoFadeOutTransition()
    {
        objectPoolSpawner.ResetPool();
        string songName = backgroundSettings.Backgrounds[indexCurrentBackground].BackgroundImage.name;
        Debug.Log(songName);
        audioManager.Unmute(songName);
        StartCoroutine(audioManager.FadeIn(songName,0.01f,0.15f));
        
        shouldTransition = false;
        backgroundSettings.SceneTransition.FadeIn();
        
        backgroundSettings.TeleportEffect.SetActive(false);
        PlayerController.ScrollingBackgroundSpeed /= 3;
        
        backgroundSettings.StageText.SetActive(false);
        PlayerController.Inmunity = false;
    }
    
}