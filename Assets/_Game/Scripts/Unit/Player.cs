using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SingletonMB<Player>, IFlyable
{
    public float Drill_Distance;
    public ResourcesType drillingType;

    [SerializeField] Transform TF;
    [SerializeField] int moveSpeed;
    [SerializeField] private float maxHP;
    [SerializeField] private Transform centerModule;
    [SerializeField] private DrillLaser laserPrefabs;
    [SerializeField] private Transform laser_start_pos;

    private float curHP;
    private bool isMoving;

    #region STATE MACHINE
    private PlayerState currentState;
    public PlayerState drillState = new Player_Drill_State();
    public PlayerState normalState = new Player_Normal_State();

    public bool IsState(PlayerState state) => currentState == state;
    public void ChangeState(PlayerState state)
    {
        if (currentState != null) currentState.OnExit();
        currentState = state;
        currentState.OnEnter();
    }

    #endregion
    public void InitValue()
    {
        centerModule = FindObjectOfType<CenterModule>().TF;
        curHP = maxHP;
        ChangeState(normalState);
    }
    public void TakeAirDamage(float dmg)
    {
        if (GameManager.Instance.gameConfig.isUndying) return;
        curHP -= dmg;
        if (curHP <= 0)
        {
            OnDespawn();
        }
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute();
        }
    }

    private void LateUpdate()
    {
        if (Vector2.Distance(TF.position, CoreManager.Instance.GameplayCamera.transform.position) > 3f)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            TF.position = Vector2.MoveTowards(TF.position, CoreManager.Instance.GameplayCamera.transform.position, moveSpeed * Time.deltaTime);
            Vector2 direct = (CoreManager.Instance.GameplayCamera.transform.position - TF.position);
            float angleZ = Mathf.Atan2(direct.x, direct.y) * Mathf.Rad2Deg;
            TF.eulerAngles = new Vector3(0, 0, -angleZ);
            if (Vector2.Distance(TF.position, CoreManager.Instance.GameplayCamera.transform.position) < 0.1f)
            {
                isMoving = false;
            }
        }
    }

    void OnDespawn()
    {
        gameObject.SetActive(false);
        ParticlePoolController.Instance.Play(ParticleType.DeathUnit, transform.position);
        AudioManager.Instance.PlaySound(SoundId.Explode);
        Invoke(nameof(Respawn), 2);
    }

    void Respawn()
    {
        TF.position = centerModule.position;
        ParticlePoolController.Instance.Play(ParticleType.Spawn, TF.position);
        gameObject.SetActive(true);
        InitValue();
    }

    public void DisplayDrillLaser(Transform endpos)
    {
        if (currentState == normalState)
        {
            ChangeState(drillState);
            DrillLaser laser = Instantiate(laserPrefabs);
            laser.OnInit(laser_start_pos, endpos);
        }   
    }
}
