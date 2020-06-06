using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletTextSingleton : MonoBehaviour
{
    [SerializeField] private Text bulletText;
    public static BulletTextSingleton SharedInstance { get; private set; }
    private void Awake(){

        if (SharedInstance == null){

            SharedInstance = this;
            DontDestroyOnLoad(this.gameObject);
            
        } else {
            Destroy(this);
        }
    }
    
    public void SetBullets(int amount)
    {
        bulletText.text = "x" + amount;
    }
    
}
