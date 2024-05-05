using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreManager : SingletonMB<CoreManager>
{
    public Camera GameplayCamera;
    public Construction selectingPrefab;
    public Construction selectingConstruction;
    public List<Construction> placingConstructionList;
    public int ConstructionDirect;
    // Start is called before the first frame update
    void Start()
    {
        placingConstructionList = new List<Construction>();
        ConstructionDirect = 0;
    }
}
