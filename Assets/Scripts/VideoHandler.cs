using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoHandler : MonoBehaviour
{
    [SerializeField] private GameObject Screen;
    
    private bool _playing;
    private VideoPlayer _player;

    private Action _callback;

    private void Awake()
    {
        _player = GetComponent<VideoPlayer>();
        HideScreen();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playing = !_playing;

            if (_playing)
            {
                _player.Play();
            }
            else
            {
                _player.Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            _callback?.Invoke();
            _callback = null;
        }
    }

    public void Play(Action onComplete)
    {
        _callback = onComplete;
        _player.Play();
    }

    public void SetZeroFrame(Action onComplete)
    {
        _callback = onComplete;
        _player.Play();
        DOVirtual.DelayedCall(0.1f, () => _player.Pause());
    }

    public void LoadClip(VideoClip clip)
    {
        _player.clip = clip;
        _player.Prepare();
    }

    public void ShowScreen()
    {
        Screen.gameObject.SetActive(true);
    }

    public void HideScreen()
    {
        Screen.gameObject.SetActive(false);
    }
    
    public bool IsShow() => Screen.gameObject.activeInHierarchy;
}
