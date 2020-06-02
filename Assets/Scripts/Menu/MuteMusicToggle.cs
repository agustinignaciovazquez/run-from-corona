using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteMusicToggle : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("MuteMusic") == 0){
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else{
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
