using UnityEngine;

public class SoundsLibrary : MonoBehaviour
{
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip missSound;
    [SerializeField] private AudioClip finishSound;

    public AudioClip GetAudioClip(AudioClipName clipName)
    {
        return clipName switch
        {
            AudioClipName.Hit => hitSound,
            AudioClipName.Miss => missSound,
            AudioClipName.Finish => finishSound,
            _ => null
        };
    }
}

public enum AudioClipName
{
    Hit,
    Miss,
    Finish
}
