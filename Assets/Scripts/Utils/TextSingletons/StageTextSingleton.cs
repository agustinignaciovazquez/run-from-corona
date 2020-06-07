using TMPro;
using UnityEngine;

public class StageTextSingleton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageText;

    private int stage = 0;
    public static StageTextSingleton SharedInstance { get; private set; }
    private void Awake(){

        if (SharedInstance == null){

            SharedInstance = this;
            //DontDestroyOnLoad(this.gameObject);
            
        } else {
            Destroy(this);
        }
    }
    
    //TODO llamar cuando empieza el juego
    public void AddStage()
    {
        stageText.text = "Stage " + (++stage) + " !";
    }
}
