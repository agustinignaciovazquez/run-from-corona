using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullets : MonoBehaviour
{
    public Text bulletText;
    
    public void SetBullets(int amount)
    {
        bulletText.text = 'x' + amount.ToString();
    }
    
}
