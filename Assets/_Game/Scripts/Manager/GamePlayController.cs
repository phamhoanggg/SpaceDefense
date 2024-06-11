using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : SingletonMB<GamePlayController>
{
    [SerializeField] private ScriptableLevel curLevel;
    [SerializeField] private GameObject nextWave_CD_obj;
    [SerializeField] private Text nextWaveSpawn_tmp;
    [SerializeField] private CenterModule centerModulePrefab;
    [SerializeField] private Transform constructorTileLayer;
    [SerializeField] private CenterModule centerModule;
    [SerializeField] private Image centerModule_hp_img;
    [SerializeField] private GameObject spawningPosition_Obj;
    [Header("ENEMY LIST")]
    [SerializeField] private List<Enemy> enemyListPrefab;

    private float playTime;
    private int currentWaveIndex;

    private void Start()
    {
        if (DataManager.Instance.gameData.currentLevelIndex == -1) nextWave_CD_obj.SetActive(false);
        StartCoroutine(
                LoadLevel(DataManager.Instance.gameData.currentLevelIndex));
        playTime = 0;
        currentWaveIndex = 0;
        Player.Instance.InitValue();
        spawningPosition_Obj.SetActive(false);
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        if (curLevel)
        {
            curLevel.EnemyWaves.Clear();
        }

        curLevel = TilemapManager.Instance.LoadMap(levelIndex);
        centerModule = Instantiate(centerModulePrefab, constructorTileLayer);
        centerModule.Place();
        centerModule.TF.localPosition = curLevel.centerModulePostion;

        yield return new WaitForSeconds(2);
        GridTileManager.Instance.SpawnAllTiles(curLevel.Map_Width, curLevel.Map_Height);
    }

    private void Update()
    {
        if (DataManager.Instance.gameData.currentLevelIndex == -1 && TutorialController.Instance.CurrentTut_index < 10) return;
        else nextWave_CD_obj.SetActive(true);
        playTime += Time.deltaTime;

        if (curLevel && currentWaveIndex < curLevel.EnemyWaves.Count)
        {
            nextWaveSpawn_tmp.text = TimeFormatter(curLevel.EnemyWaves[currentWaveIndex].spawnTime - Mathf.FloorToInt(playTime));
            if (curLevel.EnemyWaves[currentWaveIndex].spawnTime - Mathf.FloorToInt(playTime) < 30)
            {
                spawningPosition_Obj.SetActive(true);
                spawningPosition_Obj.transform.position = curLevel.EnemyWaves[currentWaveIndex].spawnPosition;
            }
            else
            {
                spawningPosition_Obj.SetActive(false);
            }

            if (playTime >= curLevel.EnemyWaves[currentWaveIndex].spawnTime)
            {
                StartCoroutine(SpawnNewWave(currentWaveIndex));
                if (currentWaveIndex == 0 && DataManager.Instance.gameData.currentLevelIndex == -1)
                {
                    TutorialController.Instance.NextTutorial(0.5f);
                }
                currentWaveIndex++;
            }
        }
        else
        {
            nextWaveSpawn_tmp.gameObject.SetActive(false);
            spawningPosition_Obj.SetActive(false);
        }

        if (centerModule)
        {
            centerModule_hp_img.fillAmount = centerModule.CurrentHp / centerModule.maxHP;
        }
        else
        {
            centerModule = FindObjectOfType<CenterModule>();
            centerModule_hp_img.fillAmount = 0;
        }
    }

    public IEnumerator SpawnNewWave(int waveIndex)
    {
        Vector2 spawnPos = curLevel.EnemyWaves[waveIndex].spawnPosition;
        for (int i = 0; i < curLevel.EnemyWaves[waveIndex].amount; i++)
        {
            Instantiate(enemyListPrefab[(int)curLevel.EnemyWaves[waveIndex].type], spawnPos, Quaternion.identity);
            ParticlePoolController.Instance.Play(ParticleType.Spawn, spawnPos);
            yield return new WaitForSeconds(1);
        }
    }

    string TimeFormatter(int second)
    {
        string time = "";
        if (second < 60)
        {
            time += "00:";
            if (second < 10)
            {
                time += "0";
            }

            time += second;
        }
        else
        {
            int min = second / 60;      // min is always < 10
            int newSec = second % 60;
            time += $"0{min}:";
            if (newSec < 10)
            {
                time += "0";
            }

            time += newSec;
        }

        return time;
    }

    public IEnumerator CheckWinLevel()
    {
        yield return new WaitForSeconds(1);
        if (currentWaveIndex == curLevel.EnemyWaves.Count)
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            if (enemies.Length == 0)
            {
                if (DataManager.Instance.gameData.currentLevelIndex == -1)
                {
                    TutorialController.Instance.NextTutorial(1);
                }
                else
                {
                    OnWinLevel();
                }
            }
        }
    }

    public void OnWinLevel()
    {
        if (DataManager.Instance.gameData.currentLevelIndex == DataManager.Instance.gameData.levelUnlocked)
        {
            DataManager.Instance.gameData.levelUnlocked++;
        }

        FormGameplay.Instance.OpenPopupWin(TimeFormatter((int)playTime));
    }

    public void OnLoseLevel()
    {

    }
}
