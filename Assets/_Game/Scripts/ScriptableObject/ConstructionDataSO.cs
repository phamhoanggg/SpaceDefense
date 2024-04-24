using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Construction Data")]
public class ConstructionDataSO : ScriptableObject
{
    public ConstructionData[] constructionSet;
}

[System.Serializable]
public class ConstructionData
{
    public int constructionID;
    public string ConstructionName;
    public Sprite ConstructionImage;
    public int[] parentID_Required;
    public bool IsUnlocked;
    public ResourceData[] unlock_resources;
}

[System.Serializable]
public class ResourceData
{
    public ResourcesType res_type;
    public int res_amount;
}

