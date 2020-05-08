using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class RandomSingleton : MonoBehaviour
{
    private static Random random;
    private static RandomSingleton _sharedInstance;
    void Awake() {
        _sharedInstance = this;
        random = new Random();
    }

    public double NextDouble()
    {
        return random.NextDouble();
    }
    
    public double RandomBetween(double min, double max)
    {
        return NextDouble() * (max - min) + min;
    }
    
    public bool RollDice(float successPercentage)
    {
        var randomValue = NextDouble();
        
        if (successPercentage > 1f || successPercentage < 0f)
            throw new ArgumentException();
        
        if (randomValue < successPercentage)
            return true;
        
        return false;
    }

    public bool RollDiceUnSuccess(float notSuccessPercentage)
    {
       return RollDice(1f - notSuccessPercentage);
    }
    
    public Vector3 GetRandomVector(double? min, double? max)
    {
        float randomScale;
                
        if (!min.HasValue)
            min = Double.MinValue;
        if(!max.HasValue)
            max = Double.MaxValue;
        
        randomScale = (float) RandomBetween(min.Value, max.Value);
        
        return new Vector3(randomScale, randomScale, randomScale);
    }
    
    public Vector3 GetRandomVector()
    {
        return GetRandomVector(null, null);
    }
    public Vector3 GetAllRandomVector(double? min, double? max)
    {
        float x,y,z;
        
        if (!min.HasValue)
            min = Double.MinValue;
        if(!max.HasValue)
            max = Double.MaxValue;
        
        x = (float) RandomBetween(min.Value, max.Value);
        y = (float) RandomBetween(min.Value, max.Value);
        z = (float) RandomBetween(min.Value, max.Value);
        
        return new Vector3(x, y, z);
    }
    
    public Vector3 GetAllRandomVector()
    {
        return GetAllRandomVector(null, null);
    }
    public static RandomSingleton GetSharedInstance => _sharedInstance;
}
