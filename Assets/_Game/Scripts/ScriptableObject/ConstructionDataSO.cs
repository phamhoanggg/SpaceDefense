using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Construction Data")]
public class ConstructionDataSO : ScriptableObject
{
    public Construction[] constructionSet;
}

[System.Serializable]
public class ConstructionData
{
    public int constructionID;
    public string ConstructionName;
    public Sprite avatarSprite;

    public List<ResourceData> buildResources = new List<ResourceData>();
    public List<ResourceData> unlockResources = new List<ResourceData>();
    public string Description;

    public int[] parentID_Required;
    public bool IsUnlocked;
}

[System.Serializable]
public class ResourceData
{
    public ResourcesType res_type;
    public int res_amount;
}

