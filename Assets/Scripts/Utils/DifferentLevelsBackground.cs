using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    //private float totalDistance;
    private SpriteRenderer spriteRenderer;
    private bool shouldTransition;
    private bool fadeIn;
    [SerializeField] private GameObject teleportEffect; 
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
        ScenarioBackground nextBackground = backgrounds[nextIndex];
        spriteRenderer.sprite = nextBackground.BackgroundImage;
        indexCurrentBackground = nextIndex;
        shouldTransition = true;
        fadeIn = true;
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
        Debug.Log("SCENE TRANSITION");
        if (fadeIn)
        {
            PlayerController.ScrollingSpeed *= 3;
            teleportEffect.SetActive(true);
            sceneTransition.FadeOut();
            fadeIn = false;
        }
        else
        {
            shouldTransition = false;
            sceneTransition.FadeIn();
            teleportEffect.SetActive(false);
            PlayerController.ScrollingSpeed /= 3;
        }
        //sceneTransition.FadeOut();
    }
}