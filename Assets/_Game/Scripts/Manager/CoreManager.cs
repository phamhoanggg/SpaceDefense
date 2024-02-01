using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreManager : SingletonMB<CoreManager>
{
    public Camera GameplayCamera;
    public Contruction selectingPrefab;
    public Contruction selectingContruction;
    public List<Contruction> placingContructionList;
    public int ConstructionDirect;
    // Start is called before the first frame update
    void Start()
    {
        placingContructionList = new List<Contruction>();
        ConstructionDirect = 0;
    }
}
