using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceTextSingleton : MonoBehaviour
{
    [SerializeField] private Text distanceText;
    [SerializeField] private GameObject distanceReference;
    [SerializeField] private Text distanceTextEnd;
    public static DistanceTextSingleton SharedInstance { get; private set; }
    private void Awake(){

        if (SharedInstance == null){

            SharedInstance = this;
            DontDestroyOnLoad(this.gameObject);
            
        } else {
            Destroy(this);
        }
    }

    public void FixedUpdate()
    {
        float distance = distanceReference.transform.position.x * -1f;
        if (distance < 0f)
            distance = 0;
        SetDistance((int)distance);
    }
    public void SetDistance(int amount)
    {
        distanceText.text = amount + "m";
        distanceTextEnd.text = distanceText.text;
    }

}
