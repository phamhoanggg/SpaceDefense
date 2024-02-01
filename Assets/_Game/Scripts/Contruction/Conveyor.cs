using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Conveyor : Contruction
{
    [SerializeField] private Animator transitionAnim;
    [Header("CONVEYOR PREFABS")]
    [SerializeField] protected Conveyor straitConveyor;
    [SerializeField] protected Conveyor cornerConveyor_clockwise;
    [SerializeField] protected Conveyor cornerConveyor_counter_clockwise;
    [SerializeField] protected Conveyor branchedConveyor_clockwise;
    [SerializeField] protected Conveyor branchedConveyor_counter_clockwise;

    [Header("COLLIDERS")]
    [SerializeField] private List<Pair_InOut> IO_Pair_list = new List<Pair_InOut>();
    [SerializeField] private GameObject[] sensorList;

    [Header("BASE VALUES")]
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private int baseInput, baseOutput;

    [Header("IN - OUT")]
    public List<GameObject> InputList = new List<GameObject>(); 
    public List<GameObject> OutputList = new List<GameObject>();

    private ConveyorType currentType;
    private Vector2 currentDirect;

    private void Start()
    {
        Debug.Log(name);
    }
    public Transform GetOutputPositionOfInput(Transform inputPos)
    {
        for (int i = 0; i < IO_Pair_list.Count; i++)
        {
            if (IO_Pair_list[i].inputPos == inputPos)
            {
                return IO_Pair_list[i].outputPos;
            }
        }
        return null;
    }

    private void Update()
    {
        if (InputList.Count > baseInput)
        {
            UpdateConveyorStyle(true, false);
        }

        if (currentType == ConveyorType.Strait)
        {
            if (InputList.Count > 0 && OutputList.Count > 0)
            {
                Vector2 direct = OutputList[0].transform.position - InputList[0].transform.position;
                if (direct.x != 0 && direct.y != 0)
                {
                    UpdateConveyorStyle(true, false);
                }
            }
        }
    }

    public override void Place()
    {
        base.Place();
        transitionAnim.ResetTrigger(GameConstant.ANIM_CONVEYOR_STOP);
        transitionAnim.SetTrigger(GameConstant.ANIM_CONVEYOR_TRANSITION);
        for (int i = 0; i < IO_Pair_list.Count; i++)
        {
            IO_Pair_list[i].inputPos.gameObject.SetActive(true);
            IO_Pair_list[i].outputPos.gameObject.SetActive(true);
        }

        for (int i = 0; i < sensorList.Length; i++)
        {
            sensorList[i].SetActive(true);
        }

        if (startPos && endPos)
        {
            currentType = ConveyorType.Strait;
            currentDirect = (endPos.position - startPos.position).normalized;
        }
    }

    public void SetStyle(ConveyorType type, Vector2 direct)
    {
        currentType = type;
        currentDirect = direct;
        Invoke(nameof(Place), 0.2f);
    }

    public void UpdateConveyorStyle(bool isIncreaseInput, bool isIncreaseOutput)
    {
        if (!(isIncreaseInput && InputList.Count >= baseInput)) return;
        if (currentType == ConveyorType.None) return;

        if (OutputList.Count == 1 && InputList.Count == 1)
        {
            Vector2 direct = OutputList[0].transform.position - InputList[0].transform.position;
            if (direct.x == 0)
            {
                //if (currentDirect == direct.normalized && currentType == ConveyorType.Strait) return;

                //Conveyor newConveyor = Instantiate(straitConveyor, transform.position, Quaternion.identity, transform.parent);
                //newConveyor.TF.eulerAngles = (direct.y > 0) ? new Vector3(0, 0, 90) : new Vector3(0, 0, -90);
                //newConveyor.SetStyle(ConveyorType.Strait, direct.normalized);
                //DestroyContruction();
            }
            else if (direct.y == 0)
            {
                //if (currentDirect == direct.normalized && currentType == ConveyorType.Strait) return;

                //Conveyor newConveyor = Instantiate(straitConveyor, transform.position, Quaternion.identity, transform.parent);
                //newConveyor.TF.eulerAngles = (direct.x > 0) ? new Vector3(0, 0, 0) : new Vector3(0, 0, -180);
                //newConveyor.SetStyle(ConveyorType.Strait, direct.normalized);

                //DestroyContruction();
            }
            else
            {
                if (currentDirect == direct.normalized && currentType == ConveyorType.Corner) return;

                Vector2 inputDirect = TF.position - InputList[0].transform.position;

                float diffAngle = Mathf.Atan2(inputDirect.y, inputDirect.x) * Mathf.Rad2Deg - Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;

                if (diffAngle > 0)
                {
                    float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
                    Conveyor newConveyor = Instantiate(cornerConveyor_clockwise, transform.position, Quaternion.identity, transform.parent);
                    newConveyor.TF.eulerAngles = new Vector3(0, 0, (angle - 135));
                    newConveyor.SetStyle(ConveyorType.Corner, direct.normalized);
                    DestroyContruction();
                }
                else
                {
                    float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
                    Conveyor newConveyor = Instantiate(cornerConveyor_counter_clockwise, transform.position, Quaternion.identity, transform.parent);
                    newConveyor.TF.eulerAngles = new Vector3(0, 0, (angle + 45));
                    newConveyor.SetStyle(ConveyorType.Corner, direct.normalized);
                    DestroyContruction();
                }          


            }
        }
        else if (InputList.Count == 2 && OutputList.Count == 1)
        {
            Vector2 mainAxis = OutputList[0].transform.position - TF.position;

            Vector2 inputDirect = Vector2.zero;
            Vector2 in_out_direct = Vector2.zero;
            for (int i = 0; i < InputList.Count; i++)
            {
                Vector2 tmpDirect = TF.position - InputList[i].transform.position;
                if (tmpDirect * mainAxis == Vector2.zero)
                {
                    inputDirect = tmpDirect;
                    in_out_direct = OutputList[0].transform.position - InputList[i].transform.position;
                    break;
                }
            }

            if (currentDirect == in_out_direct.normalized && currentType == ConveyorType.Branch) return;

            if (inputDirect == Vector2.zero || in_out_direct == Vector2.zero) return;

            Debug.Log("Input direct: " + inputDirect.normalized);
            Debug.Log("In_out direct: " + in_out_direct.normalized);
            float diffAngle = Mathf.Atan2(inputDirect.y, inputDirect.x) * Mathf.Rad2Deg - Mathf.Atan2(in_out_direct.y, in_out_direct.x) * Mathf.Rad2Deg;
            
            if (diffAngle > 180)
            {
                diffAngle -= 360;
            }

            if (diffAngle > 0)
            {
                float angle = Mathf.Atan2(in_out_direct.y, in_out_direct.x) * Mathf.Rad2Deg;
                Conveyor newConveyor = Instantiate(branchedConveyor_clockwise, transform.position, Quaternion.identity, transform.parent);
                newConveyor.TF.eulerAngles = new Vector3(0, 0, (angle - 45));
                newConveyor.SetStyle(ConveyorType.Branch, in_out_direct.normalized);
                DestroyContruction();
            }
            else
            {
                float angle = Mathf.Atan2(in_out_direct.y, in_out_direct.x) * Mathf.Rad2Deg;
                Conveyor newConveyor = Instantiate(branchedConveyor_counter_clockwise, transform.position, Quaternion.identity, transform.parent);
                newConveyor.TF.eulerAngles = new Vector3(0, 0, (angle - 135));
                newConveyor.SetStyle(ConveyorType.Branch, in_out_direct.normalized);
                DestroyContruction();
            }
            
        }
    }
}

[Serializable]
public class Pair_InOut
{
    public Transform inputPos;
    public Transform outputPos;
}
