using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    private GemAdsHandler gemAdHandler;

    
    // Start is called before the first frame update
    void Start()
    {
        gemAdHandler = GetComponent<GemAdsHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Back()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene("Menu");
    }

    public void GetGems()
    {
        gemAdHandler.ShowRewardBasedAd();
    }
   
}
