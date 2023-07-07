using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoClipsLibrary : MonoBehaviour
{
    [SerializeField] private List<VideoClip> husbandDanceSource;
    [SerializeField] private List<VideoClip> husbandKaraokeSource;
    [SerializeField] private List<VideoClip> wifeDanceSource;
    [SerializeField] private List<VideoClip> wifeKaraokeSource;

    public Queue<VideoClip> GetHusbandDanceQueue()
    {
        var result = new Queue<VideoClip>();
        foreach (var source in husbandDanceSource)
        {
            result.Enqueue(source);
        }

        return result;
    }
    
    public Queue<VideoClip> GetWifeDanceQueue()
    {
        var result = new Queue<VideoClip>();
        foreach (var source in wifeDanceSource)
        {
            result.Enqueue(source);
        }

        return result;
    }
    
    public Queue<VideoClip> GetHusbandKaraokeQueue()
    {
        var result = new Queue<VideoClip>();
        foreach (var source in husbandKaraokeSource)
        {
            result.Enqueue(source);
        }

        return result;
    }
    
    public Queue<VideoClip> GetWifeKaraokeQueue()
    {
        var result = new Queue<VideoClip>();
        foreach (var source in wifeKaraokeSource)
        {
            result.Enqueue(source);
        }

        return result;
    }
}
