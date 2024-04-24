using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Resources Data Wrapper")]
public class ResourcesDataWrapperSO : ScriptableObject
{
    public ResourcesListData[] sets;
}

[System.Serializable]
public class ResourcesListData
{
    public ResourcesListData[] resourcesList;
}

[System.Serializable]
public class ResourcesData
{
    public string resName;
    public ResourcesType resType;
    public bool availableNature;
}
