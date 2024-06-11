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
    public Construction constructionPrefab;
    public int constructionID;
    public ConstructionType constructionType;
    public int[] parent_Required_ID;
    public bool IsUnlocked;
}

[System.Serializable]
public class ResourceData
{
    public ResourcesType res_type;
    public int res_amount;
}

public enum ConstructionType
{
    Drill,
    Conveyor,
    Turret,
    Defender,
}

