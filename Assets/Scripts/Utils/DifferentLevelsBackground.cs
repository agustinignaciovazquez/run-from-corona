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
    [SerializeField] private GameObject distanceReference;
    [SerializeField] private List<ScenarioBackground> backgrounds;
    private int indexCurrentBackground;
    private float distanceToNextBackground;
    private float totalDistance;
    private SpriteRenderer spriteRenderer;
    protected override void Awake()
    {
        base.Awake();
        totalDistance = GetTotalDistance();
        spriteRenderer = GetComponent<SpriteRenderer>();
        indexCurrentBackground = 0;
        distanceToNextBackground = backgrounds[indexCurrentBackground].DistanceToShow;
    }

    private float GetTotalDistance()
    {
        float distance = 0;
        
        foreach(ScenarioBackground df in backgrounds)
        {
            distance += df.BackgroundImage.bounds.size.x;
        }
        
        return distance;
    }
    protected override void RepositionBackground(Transform backgroundTransform)
    {
        base.RepositionBackground(backgroundTransform);
        float distance = distanceReference.transform.position.x * -1f;
        print(distance);
        print(distanceToNextBackground);
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
        distanceToNextBackground += nextBackground.DistanceToShow;
    }
}