using System;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] AudioMixerSnapshot _patrolSnapshot, _redAlertSnapshot;
    [SerializeField] AudioClip[] _patrolMusic, _redAlertMusic;
    [SerializeField] AudioSource _patrolAudioSource, _redAlertAudioSource;

    int _patrolMusicIndex, _redAlertMusicIndex;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlayRedAlert()
    {
        _redAlertAudioSource.Stop();
        _redAlertAudioSource.clip = _redAlertMusic[_redAlertMusicIndex];
        _redAlertAudioSource.Play();
        _redAlertSnapshot.TransitionTo(1f);
        _redAlertMusicIndex = (_redAlertMusicIndex + 1) % _redAlertMusic.Length;        
    }

    public void PlayPatrolMusic()
    {
        _patrolAudioSource.Stop();
        _patrolAudioSource.clip = _patrolMusic[_patrolMusicIndex];
        _patrolAudioSource.Play();
        _patrolSnapshot.TransitionTo(1f);
        _patrolMusicIndex = (_patrolMusicIndex + 1) % _patrolMusic.Length;
    }
}
