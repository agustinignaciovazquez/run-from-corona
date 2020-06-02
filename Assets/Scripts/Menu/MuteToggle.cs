using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteToggle : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("MuteSound") == 0){
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else{
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    
}
