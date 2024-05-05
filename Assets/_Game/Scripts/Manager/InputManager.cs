using UnityEngine;
using UnityEngine.EventSystems;

public enum InputMode
{
    Default,
    PlacingConstruction,
}

public class InputManager : SingletonMB<InputManager>
{
    public Camera mainCamera;
    public LayerMask tileLayer, constructionLayer, resourcesLayer, uiLayer, oreLayer;
    public Transform gridContructParent;
    private GamePlayState currentState;

    private GamePlayState defaultState;
    public GamePlayState DefaultState => defaultState;

    private GamePlayState placeConstructionState;
    public GamePlayState PlaceConstructionState => placeConstructionState;

    public GameObject Selecting_Block;

    public bool IsBlockInput => isBlockInput;
    private bool isBlockInput;
    // Start is called before the first frame update
    void Start()
    {
        defaultState = new Input_Default_State();
        placeConstructionState = new Input_PlaceConstruction_State();
        ChangeState(defaultState);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute();
        }
    }

    public bool IsGameState(GamePlayState state) => currentState == state;

    public void ChangeState(GamePlayState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = newState;
        currentState.OnEnter();
    }

    public void PlaceNewConstruction(Vector3 tilePos, int ConstructionSize)
    {
        Vector3 pos = tilePos;
        if (ConstructionSize == 1)
        {

        }
        else if (ConstructionSize == 2)
        {
            Vector3 leftTilePos = pos - new Vector3(GameConstant.TileSize, 0, 0);
            Vector3 underTilePos = pos - new Vector3(0, GameConstant.TileSize, 0);
            Vector3 left_under_TilePos = pos - new Vector3(GameConstant.TileSize, GameConstant.TileSize, 0);

            if (Physics2D.Raycast(leftTilePos, Vector3.forward, 100, constructionLayer))
            {
                Debug.Log("On Left");
                return;
            }

            if (Physics2D.Raycast(underTilePos, Vector3.forward, 100, constructionLayer))
            {
                Debug.Log("On Under");
                return;
            }

            if (Physics2D.Raycast(left_under_TilePos, Vector3.forward, 100, constructionLayer))
            {
                Debug.Log("On Left Under");
                return;
            }
            pos -= new Vector3(GameConstant.TileSize / 2, GameConstant.TileSize / 2, 0);
        }

        Construction newConstruction = Instantiate(CoreManager.Instance.selectingPrefab, pos, Quaternion.identity, gridContructParent);
        newConstruction.transform.eulerAngles = new Vector3(0, 0, -90 * CoreManager.Instance.ConstructionDirect);
        newConstruction.PlayAnimPrepare(true);
        CoreManager.Instance.placingConstructionList.Add(newConstruction);
        CoreManager.Instance.selectingConstruction = newConstruction;

        newConstruction.ConsumeResources();

        Selecting_Block.SetActive(true);
        Selecting_Block.transform.position = pos;
        Selecting_Block.transform.localScale = Vector3.one * ConstructionSize;
    }

    public void SetBlockInput(bool isBlock)
    {
        isBlockInput = isBlock;
    }
}
