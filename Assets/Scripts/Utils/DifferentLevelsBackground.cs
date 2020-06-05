using System.Collections.Generic;
using UnityEngine;

public class DifferentLevelsBackground : ScrollingBackground
{
 
    
    [System.Serializable]
    public class ScenarioBackground
    {
        [SerializeField] private Sprite backgroundImage;
        [SerializeField] private float distanceToShow;

        public Sprite BackgroundImage => backgroundImage;

        public float DistanceToShow => distanceToShow / 2; //Maths so distance to show really works as expected xd
    }
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private GameObject distanceReference;
    [SerializeField] private List<ScenarioBackground> backgrounds;

    private int indexCurrentBackground;
    private float distanceToNextBackground;
    private SpriteRenderer spriteRenderer;
    private bool shouldTransition;
    private bool fadeIn;

    [SerializeField] private GameObject teleportEffect;
    [SerializeField] private GameObject stageText;

    protected override void Awake()
    {
        base.Awake();
        //totalDistance = GetTotalDistance();
        spriteRenderer = GetComponent<SpriteRenderer>();
        indexCurrentBackground = 0;
        shouldTransition = false;
        fadeIn = false;
        distanceToNextBackground = backgrounds[indexCurrentBackground].DistanceToShow;
        
    }

   /* private float GetTotalDistance()
    {
        float distance = 0;
        
        foreach(ScenarioBackground df in backgrounds)
        {
            distance += df.BackgroundImage.bounds.size.x;
        }
        
        return distance;
    }*/
   
    protected override void RepositionBackground(Transform backgroundTransform)
    {
        base.RepositionBackground(backgroundTransform);
        float distance = distanceReference.transform.position.x * -1f;
        //Update player distance
        PlayerController.DistanceTraveled = distance;
        if (distance > distanceToNextBackground)
        {
            ChangeBackground();
        }
    }

    private void ChangeBackground()
    {
        int nextIndex = (indexCurrentBackground + 1) % backgrounds.Count;
        if (nextIndex != PlayerController.BackgroundIndex)
        {
            PlayerController.BackgroundIndex = nextIndex;
            shouldTransition = true;
            fadeIn = true;
        }
      
        ScenarioBackground nextBackground = backgrounds[nextIndex];
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
        if (fadeIn)
        {
            PlayerController.ScrollingSpeed *= 3;
            sceneTransition.FadeOut();
            fadeIn = false;
            string songName = backgrounds[indexCurrentBackground - 1].BackgroundImage.name;
            Debug.Log(songName);
            StartCoroutine(FindObjectOfType<AudioManager>().FadeOut(songName,0.1f));
            teleportEffect.SetActive(true);
            stageText.SetActive(true);
            StageTextSingleton stageTextSingleton = StageTextSingleton.SharedInstance;
            stageTextSingleton.AddStage();
        }
        else
        {
            string songName = backgrounds[indexCurrentBackground].BackgroundImage.name;
            Debug.Log(songName);
            StartCoroutine(FindObjectOfType<AudioManager>().FadeIn(songName,0.01f,0.15f));
            shouldTransition = false;
            sceneTransition.FadeIn();
            teleportEffect.SetActive(false);
            PlayerController.ScrollingSpeed /= 3;
            stageText.SetActive(false);
        }
        //sceneTransition.FadeOut();
    }
}