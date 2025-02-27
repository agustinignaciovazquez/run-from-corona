﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Skin Item")]
public class SkinShopItem : ShopItem
{
    [SerializeField] private float infectionDefense = 0.01f;
    [SerializeField] private string audioName;
    
    public float InfectionDefense => infectionDefense;
    public string AudioName => audioName;
}
