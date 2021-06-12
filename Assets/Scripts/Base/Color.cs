using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Color
{
    Fire,
    Wood,
    Water,
}

public class Element
{
    private static Element element;
    Dictionary<Color, Color32> colorMap;
    private Element()
    {
        colorMap = new Dictionary<Color, Color32>();
        colorMap[Color.Fire] = new Color32(240, 0, 0, 255);
        colorMap[Color.Wood] = new Color32(0, 200, 0, 255);
        colorMap[Color.Water] = new Color32(0, 0, 200, 255);
    }
    public Color32 GetColor(Color color)
    {
        if(colorMap.ContainsKey(color))
        {
            return colorMap[color];
        }
        return colorMap[Color.Fire];
    }
    public static Element _Elemet
    {
        get
        {
            if(element==null)
            {
                element = new Element();
                return element;
            }
            else
            {
                return element;
            }
        }
    }
}
