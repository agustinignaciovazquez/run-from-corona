using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class AdsInitiliazer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Amplitude amplitude = Amplitude.Instance;
        amplitude.logging = true;
        amplitude.init("5e6cc1858b1aec3c87d0083e303d180e");
        amplitude.logEvent("MENU_START");
        MobileAds.Initialize(initStatus => { });
    }
    
}
