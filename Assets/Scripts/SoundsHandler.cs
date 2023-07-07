using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundsHandler : MonoBehaviour
{
    private AudioSource _source;
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        _source.clip = clip;
        _source.Play();
    }
}
