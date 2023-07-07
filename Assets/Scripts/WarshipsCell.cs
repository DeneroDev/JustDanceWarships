using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class WarshipsCell : MonoBehaviour
{
    [SerializeField] private Button coverBtn;
    [SerializeField] private Image result;

    private bool _hasResult;
    private VideoClip _source;
    private CellType _cellType;

    public void Init(Action<VideoClip, CellType> onOpen, CellType cellType, VideoClip source = default)
    {
        _hasResult = source != default;
        coverBtn.onClick.AddListener(() => coverBtn.interactable = false);
        _cellType = cellType;
        if (_hasResult)
        {
            _source = source;
            coverBtn.onClick.AddListener(() => onOpen?.Invoke(_source, _cellType));
        }
    }

    public void Init(Action onMissClick)
    {
        _hasResult = false;
        coverBtn.onClick.AddListener(() => onMissClick?.Invoke());
        coverBtn.onClick.AddListener(() => coverBtn.interactable = false);
        result.color = Color.clear;
    }
    
    public enum CellType
    {
        Karaoke,
        Dance
    }
}
