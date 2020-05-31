using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceTextSingleton : MonoBehaviour
{
    [SerializeField] private Text distanceText;
    [SerializeField] private GameObject distanceReference;
    public static DistanceTextSingleton SharedInstance { get; private set; }
    private void Awake(){

        if (SharedInstance == null){

            SharedInstance = this;
            DontDestroyOnLoad(this.gameObject);
            
        } else {
            Destroy(this);
        }
    }

    public void Update()
    {
        float distance = distanceReference.transform.position.x * -1f;
        SetDistance((int)distance);
    }
    public void SetDistance(int amount)
    {
        distanceText.text = amount + "m";
    }

}
