using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PreferenceSpriteSwap : MonoBehaviour
{
    // The name of the sprite sheet to use
    private string SpriteSheetName;

    // The name of the currently loaded sprite sheet
    private string LoadedSpriteSheetName;

    // The dictionary containing all the sliced up sprites in the sprite sheet
    private Dictionary<string, Sprite> spriteSheet;

    // The Unity sprite renderer so that we don't have to get it multiple times
    protected SpriteRenderer spriteRenderer;
    public enum SpriteType { Weapon, Jetpack, Skin };
    public SpriteType spriteType;

    private PlayerItemsState playerItemsState;
    protected void Start()
    {
        // Get and cache the sprite renderer for this game object
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerItemsState = PlayerItemsState.Instance;
        SpriteSheetName = GetSpriteSheetName();
        
        this.LoadSpriteSheet();
    }

    // Runs after the animation has done its work
    private void LateUpdate()
    {
        // Check if the sprite sheet name has changed (possibly manually in the inspector)
        if (this.LoadedSpriteSheetName != this.SpriteSheetName)
        {
            // Load the new sprite sheet
            this.LoadSpriteSheet();
        }

        // Swap out the sprite to be rendered by its name
        // Important: The name of the sprite must be the same!
        this.spriteRenderer.sprite = this.spriteSheet[this.spriteRenderer.sprite.name];
    }

    // Loads the sprites from a sprite sheet
    protected void LoadSpriteSheet()
    {
        // Load the sprites from a sprite sheet file (png). 
        // Note: The file specified must exist in a folder named Resources
        var sprites = Resources.LoadAll<Sprite>(this.SpriteSheetName);
        this.spriteSheet = sprites.ToDictionary(x => x.name, x => x);

        // Remember the name of the sprite sheet in case it is changed later
        this.LoadedSpriteSheetName = this.SpriteSheetName;
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
