using UnityEngine;
using UnityEngine.EventSystems;

public enum InputMode
{
    Default,
    PlacingContruction,
}

public class InputManager : SingletonMB<InputManager>
{
    public Camera mainCamera;
    public LayerMask tileLayer, constructionLayer, resourcesLayer, uiLayer, oreLayer;
    public Transform gridContructParent;
    private GridTile selectingTile;
    private InputMode currentInputMode;
    private GamePlayState currentState;

    private GamePlayState defaultState;
    public GamePlayState DefaultState => defaultState;

    private GamePlayState placeContructionState;
    public GamePlayState PlaceContructionState => placeContructionState;

    public GameObject Selecting_Block;

    // Start is called before the first frame update
    void Start()
    {
        defaultState = new Input_Default_State();
        placeContructionState = new Input_PlaceContruction_State();
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

    public void PlaceNewContruction(Vector3 tilePos, int contructionSize)
    {
        Vector3 pos = tilePos;
        if (contructionSize == 1)
        {

        }
        else if (contructionSize == 2)
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

        Contruction newContruction = Instantiate(CoreManager.Instance.selectingPrefab, pos, Quaternion.identity, gridContructParent);
        newContruction.transform.eulerAngles = new Vector3(0, 0, -90 * CoreManager.Instance.ConstructionDirect);
        newContruction.PlayAnimPrepare(true);
        CoreManager.Instance.placingContructionList.Add(newContruction);
        CoreManager.Instance.selectingContruction = newContruction;

        newContruction.ConsumeResources();

        Selecting_Block.SetActive(true);
        Selecting_Block.transform.position = pos;
        Selecting_Block.transform.localScale = Vector3.one * contructionSize;
    }

}
