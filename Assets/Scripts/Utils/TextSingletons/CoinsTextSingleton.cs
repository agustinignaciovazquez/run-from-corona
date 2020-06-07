using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsTextSingleton : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    [SerializeField] private Text coinsTextEnd;
    public static CoinsTextSingleton SharedInstance { get; private set; }
    private void Awake(){

        if (SharedInstance == null){

            SharedInstance = this;
            //DontDestroyOnLoad(this.gameObject);
            
        } else {
            Destroy(this);
        }
    }
    
    public void SetCoins(int amount)
    {
        coinsText.text = "x" + amount;
        coinsTextEnd.text = coinsText.text;
    }
}
