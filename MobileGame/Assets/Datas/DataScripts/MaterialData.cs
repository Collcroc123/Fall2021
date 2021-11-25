using UnityEngine;

[CreateAssetMenu]
public class MaterialData : ScriptableObject
{
    public Material wall1, wall3, wall5, floor;
    public Texture[] wallTextures, floorTextures;
    public int slot;
    public IntData lastSlot;
    
    public void Randomize()
    {
        lastSlot.value = slot;
        while (slot == lastSlot.value)
        {
            slot = Random.Range(0, wallTextures.Length);
        }
        wall1.mainTexture = wallTextures[slot];
        wall3.mainTexture = wallTextures[slot];
        wall5.mainTexture = wallTextures[slot];
        floor.mainTexture = floorTextures[slot];
    }

    public void Reset()
    {
        slot = 0;
        wall1.mainTexture = wallTextures[slot];
        wall3.mainTexture = wallTextures[slot];
        wall5.mainTexture = wallTextures[slot];
        floor.mainTexture = floorTextures[slot];
    }
}
