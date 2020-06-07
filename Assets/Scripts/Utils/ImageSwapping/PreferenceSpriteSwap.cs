using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferenceSpriteSwap : SpriteSwap
{
    public enum SpriteType { Weapon, Jetpack, Skin };
    public SpriteType spriteType;

    private PlayerItemsState playerItemsState;
    protected override void Start()
    {
        // Get and cache the sprite renderer for this game object
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerItemsState = PlayerItemsState.Instance;
        SpriteSheetName = GetSpriteSheetName();
        
        this.LoadSpriteSheet();
    }

    private string GetSpriteSheetName()
    {
        switch (spriteType)
        {
            case SpriteType.Weapon:
                return playerItemsState.CurrentWeapon.SpriteName;
            case SpriteType.Skin:
                return playerItemsState.CurrentSkin.SpriteName;
            case SpriteType.Jetpack:
                return playerItemsState.CurrentJetpack.SpriteName;
        }

        return null;
    }
}
