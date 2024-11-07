using System.Collections;
using UnityEngine;

public class Signaling : MonoBehaviour
{
    [SerializeField] private AudioClip _sound;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private Door _door;

    [SerializeField] private float _sleepTimer;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxVolume;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _audio.clip = _sound;

        _door.CrookDetected += PlayMusic;
        _door.CrookLost += StopMusic;
    }

    private void OnDisable()
    {
        _door.CrookDetected -= PlayMusic;
        _door.CrookLost -= StopMusic;
    }

    private void PlayMusic()
    {
        _audio.Play();

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeVolume(_maxVolume));
    }

    private void StopMusic()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeVolume(0));
    }

    private IEnumerator ChangeVolume(float volume)
    {
        while (_audio.volume != volume)
        {
            _audio.volume = Mathf.MoveTowards(_audio.volume, volume, _speed * Time.deltaTime);

            yield return null;
        }

        if (_audio.volume == 0)
            _audio.Stop();
    }
}