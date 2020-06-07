using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTextController : MonoBehaviour
{
    [SerializeField] private GameObject stageText;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());
    }

     IEnumerator StartGame()
    {
        stageText.SetActive(true);
        StageTextSingleton stageTextSingleton = StageTextSingleton.SharedInstance;
        stageTextSingleton.AddStage();

        yield return new WaitForSeconds(1.5f);
        stageText.SetActive(false);
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
