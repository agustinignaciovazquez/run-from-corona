using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceTextSingleton : MonoBehaviour
{
    [SerializeField] private Text distanceText;
    public static DistanceTextSingleton SharedInstance { get; private set; }
    private void Awake(){

        if (SharedInstance == null){

            SharedInstance = this;
            DontDestroyOnLoad(this.gameObject);
            
        } else {
            Destroy(this);
        }
    }
    
    public void SetDistance(int amount)
    {
        distanceText.text = "x" + amount;
    }

}
