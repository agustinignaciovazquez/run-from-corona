using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Shop/Jetpack Item")]
public class JetpackShopItem : ShopItem
{
    //Jetpack Energy Vars
    [SerializeField] private float startEnergy = 25f;
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float energyRegen = 0.5f;
    [SerializeField] private float energySpend = 0.3f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float jetpackForce = 40f;
    [SerializeField] private float normalizeRotationSpeed = 3f;
    [SerializeField] private float moveSpeed = 3f;
    public float StartEnergy => startEnergy;

    public float MaxEnergy => maxEnergy;

    public float EnergyRegen => energyRegen;

    public float EnergySpend => energySpend;

    public float RotationSpeed => rotationSpeed;

    public float JetpackForce => jetpackForce;

    public float NormalizeRotationSpeed => normalizeRotationSpeed;

    public float MoveSpeed => moveSpeed;
}
