using UnityEngine;


[CreateAssetMenu(fileName = "Glyphs", menuName = "CuffBoard/GlyphCollection", order = 1)]
public class BoardGlyphCollection : ScriptableObject
{
    public string[] lowerLeft;
    public string[] lowerRight;

    public string[] upperLeft;
    public string[] upperRight;

    public string[] altLeft;
    public string[] altRight;

    public string[] altUpperLeft;
    public string[] altUpperRight;
}