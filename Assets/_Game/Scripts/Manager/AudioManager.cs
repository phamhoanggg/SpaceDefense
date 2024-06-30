using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class MusicByIdDictionary : SerializedDictionary<MusicId, AudioClip> { }
[Serializable]
public class SoundByIdDictionary : SerializedDictionary<SoundId, AudioClip> { }

public class AudioManager : SingletonMB<AudioManager>
{
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource soundSource;
    [SerializeField]
    private MusicByIdDictionary musicById;
    [SerializeField]
    private SoundByIdDictionary soundById;

    private IEnumerator repeatCoroutine;

    private GameData gameData => DataManager.Instance.gameData;

    public void Initialize()
    {
        ToggleAudio();
    }

    public void ToggleAudio()
    {
        musicSource.enabled = gameData.isMusicEnabled;
        soundSource.enabled = gameData.isSFXEnabled;
    }

    [Button(ButtonStyle.FoldoutButton)]
    public void PlayMusic(MusicId id)
    {
        musicSource.clip = musicById[id];
        musicSource.Play();
    }

    [Button(ButtonStyle.FoldoutButton)]
    public void PlaySound(SoundId id)
    {
        if (!soundById.ContainsKey(id) || soundById[id] == null)
        {
            Debug.LogError($"There is no audio corresponding to this id: {id}");
            return;
        }

        if (soundSource.loop)
        {
            soundSource.loop = false;
            soundSource.Stop();
        }
        
        if (repeatCoroutine != null)
        {
            StopCoroutine(repeatCoroutine);
        }

        soundSource.PlayOneShot(soundById[id]);
    }

    public IEnumerator PlaySoundDelay(SoundId id, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        PlaySound(id);
    }

    [Button(ButtonStyle.FoldoutButton)]
    public void PlaySound(SoundId id, float duration)
    {
        if (!soundById.ContainsKey(id) || soundById[id] == null)
        {
            Debug.LogError($"There is no audio corresponding to this id: {id}");
            return;
        }

        if (repeatCoroutine != null)
        {
            StopCoroutine(repeatCoroutine);
        }

        soundSource.loop = true;
        soundSource.clip = soundById[id];
        soundSource.Play();

        ScheduleUtils.DelayTask(duration, () =>
        {
            if (soundSource.loop)
            {
                soundSource.loop = false;
                soundSource.Stop();
            }
        });
    }

    [Button(ButtonStyle.FoldoutButton)]
    public void PlaySoundRepeat(SoundId id, int time, float interval)
    {
        if (!soundById.ContainsKey(id) || soundById[id] == null)
        {
            Debug.LogError($"There is no audio corresponding to this id: {id}");
            return;
        }

        repeatCoroutine = PlaySoundRepeatRoutine(id, time, interval);
        StartCoroutine(repeatCoroutine);
    }

    private IEnumerator PlaySoundRepeatRoutine(SoundId id, int time, float interval)
    {
        WaitForSeconds wait = new WaitForSeconds(interval);
        soundSource.clip = soundById[id];

        for (int i = 0; i < time; i++)
        {
            soundSource.Play();
            yield return wait;
        }
    }

    public void ToggleMusic()
    {
        musicSource.enabled = !musicSource.enabled;
    }

    public void ToggleSound()
    {
        soundSource.enabled = !soundSource.enabled;
    }
}

public enum MusicId
{
    [HideInInspector]
    None = -1,
    Menu = 0,
    Game = 1,
}

public enum SoundId
{
    [HideInInspector]
    None = -1,
    Click = 0,
    Win = 1,
    Lose = 2,
    PopupOpen = 3,
    PopupClose = 4,
    Play_Click = 5,
    Bullet_hit = 6,
    Bullet_fire = 7,
    Explode = 8,
    EnemySpawn = 9,
}
