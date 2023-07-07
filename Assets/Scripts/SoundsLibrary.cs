using UnityEngine;

public class SoundsLibrary : MonoBehaviour
{
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip missSound;

    public AudioClip GetAudioClip(AudioClipName clipName)
    {
        return clipName switch
        {
            AudioClipName.Hit => hitSound,
            AudioClipName.Miss => missSound,
            _ => null
        };
    }
}

public enum AudioClipName
{
    Hit,
    Miss
}
