using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethods
{
    public static Sprite LayerSprite(List<Sprite> spriteList)
    {
        Resources.UnloadUnusedAssets();

        Texture2D newTexture = new Texture2D(100, 100);

        // Fill with transparent pixels
        for (int x = 0; x < newTexture.width; ++x)
        {
            for (int y = 0; y < newTexture.height; ++y)
            {
                newTexture.SetPixel(x, y, new Color(1, 1, 1, 0));
            }
        }

        for (int i = 0; i < spriteList.Count; ++i)
        {
            for (int x = 0; x < newTexture.width; ++x)
            {
                for (int y = 0; y < newTexture.height; ++y)
                {
                    // If sprite list pixel is transparent
                    Color thisPixelColor = spriteList[i].texture.GetPixel(
                        x, y).a == 0 ?
                        // Then don't get new pixel
                        newTexture.GetPixel(x, y) :
                        // Else get new pixel
                        spriteList[i].texture.GetPixel(x, y);

                    newTexture.SetPixel(x, y, thisPixelColor);
                }
            }
        }

        newTexture.Apply();
        Sprite finalSprite = Sprite.Create(newTexture, new Rect
            (0, 0, newTexture.width, newTexture.height)
            , new Vector2(0.5f, 0.5f));

        string newName = "";
        foreach(Sprite sprite in spriteList)
        {
            newName += sprite.name + ".";
        }
        newName += "Sprite";
        finalSprite.name = newName;

        //Debug.Log("finalSprite: " + finalSprite.name);

        return finalSprite;
    }

    public static T FindChildObject<T>(this GameObject target)
    {
        return RecursiveFindChild<T>(target.transform);
    }

    public static T RecursiveFindChild<T>(Transform parent)
    {
        foreach (Transform child in parent)
        {
            T tryGet = child.GetComponent<T>();

            if (tryGet != null)
            {
                return tryGet;
            }
            else
            {
                T found = RecursiveFindChild<T>(child);
                if (found != null)
                {
                    return found;
                }
            }
        }
        return default(T);
    }
}