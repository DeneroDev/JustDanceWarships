using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundsHandler : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;

    private AudioSource _source;
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        backgroundMusic.Play();
    }

    public void Play(AudioClip clip)
    {
        _source.clip = clip;
        _source.Play();
    }

    public void PlayBackground()
    {
        backgroundMusic.Play();
    }

    public void PauseBackground()
    {
        backgroundMusic.Pause();
    }
}
