using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSettings : MonoBehaviour
{
    [System.Serializable]
    public class ScenarioBackground
    {
        [SerializeField] private Sprite backgroundImage;
        [SerializeField] private float distanceToShow;

        public Sprite BackgroundImage => backgroundImage;

        public float DistanceToShow => distanceToShow / 2; //Maths so distance to show really works as expected xd
    }
    [SerializeField] private List<ScenarioBackground> backgrounds;
    
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private GameObject teleportEffect;
    [SerializeField] private GameObject stageText;
    public static BackgroundSettings Instance { get; private set; }
    // Start is called before the first frame update
    private void Awake(){
        if (Instance != null && Instance != this){
            Destroy(Instance);
        }
        Instance = this;
    }

    public List<ScenarioBackground> Backgrounds => backgrounds;

    public SceneTransition SceneTransition => sceneTransition;

    public GameObject TeleportEffect => teleportEffect;

    public GameObject StageText => stageText;
}
