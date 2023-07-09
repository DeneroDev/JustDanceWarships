using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Video;
using Random = System.Random;

public class WarshipsField : MonoBehaviour
{
    private const int TargetCount = 5;
    [SerializeField] private List<WarshipsCell> husbandCells;
    [SerializeField] private List<WarshipsCell> wifeCells;
    [SerializeField] private Transform wifeField;
    [SerializeField] private Transform husbandField;
    [SerializeField] private VideoClipsLibrary videoLibrary;
    [SerializeField] private VideoHandler videoPlayerHandler;
    [SerializeField] private LoadingScreenHandler loadingScreenHandler;
    [SerializeField] private SoundsHandler soundsHandler;
    [SerializeField] private SoundsLibrary soundsLibrary;
    [SerializeField] private HintHandler hintHandler;
    [SerializeField] private FinishScreen finishScreen;

    private FieldState _state;
    public int _scoreHusband = 0;
    private int _scoreWife = 0;

    private void Awake()
    {
        InitField(videoLibrary.GetHusbandDanceQueue(), videoLibrary.GetHusbandKaraokeQueue(), wifeCells);
        InitField(videoLibrary.GetWifeDanceQueue(), videoLibrary.GetWifeKaraokeQueue(), husbandCells);
        ShowWifeField();
    }

    private void ShowWifeField()
    {
        _state = FieldState.Wife;
        wifeField.gameObject.SetActive(true);
        husbandField.gameObject.SetActive(false);
    }

    private void ShowHusbandField()
    {
        _state = FieldState.Husband;
        wifeField.gameObject.SetActive(false);
        husbandField.gameObject.SetActive(true);
    }

    private void InitField(Queue<VideoClip> danceVideos, Queue<VideoClip> karaokeVideos, List<WarshipsCell> field)
    {
        var random = new Random();
        var videosCount = danceVideos.Count + karaokeVideos.Count;
        var resultsIds = new List<int>();
        for (var i = 0; i < videosCount; i++)
        {
            var number = random.Next(0, field.Count);
            if (resultsIds.Contains(number))
            {
                number = random.Next(0, field.Count);
            }
            
            resultsIds.Add(number);
        }

        for (var i = 0; i < field.Count; i++)
        {
            if (resultsIds.Contains(i))
            {
                if (danceVideos.Count > 0)
                {
                    field[i].Init(StartVideo, WarshipsCell.CellType.Dance, danceVideos.Dequeue());
                    continue;
                }
                
                if (karaokeVideos.Count > 0)
                    field[i].Init(StartVideo, WarshipsCell.CellType.Karaoke, karaokeVideos.Dequeue());
            }
            else
                field[i].Init(MissClick);
        }   
    }

    private void StartVideo(VideoClip videoClip, WarshipsCell.CellType cellType)
    {
        if(videoPlayerHandler.IsShow())
            return;

        if (_state == FieldState.Wife)
            _scoreWife++;
        else
            _scoreHusband++;
        
        soundsHandler.PauseBackground();
        soundsHandler.Play(soundsLibrary.GetAudioClip(AudioClipName.Hit));
        
        hintHandler.Show($"Испытание {GetNameChallenge(cellType)}");
        DOVirtual.DelayedCall(2f, () =>
        {
            videoPlayerHandler.LoadClip(videoClip);
            loadingScreenHandler.Show(() =>
            {
                videoPlayerHandler.SetZeroFrame(()=>
                {
                    soundsHandler.PlayBackground();
                    NextField();
                });
                loadingScreenHandler.Hide(null);
                videoPlayerHandler.ShowScreen();
            });
        });
    }

    private void MissClick()
    {
        if(videoPlayerHandler.IsShow())
            return;
        
        soundsHandler.Play(soundsLibrary.GetAudioClip(AudioClipName.Miss));
        DOVirtual.DelayedCall(2f, NextField);
    }

    private void NextField()
    {
        loadingScreenHandler.Show(() =>
        {
            videoPlayerHandler.HideScreen();

            if (_scoreHusband >= TargetCount
                || _scoreWife >= TargetCount)
            {
                _state = _scoreHusband >= TargetCount ? FieldState.Husband : FieldState.Wife;
                finishScreen.Show(_scoreHusband, _scoreWife, TargetCount, GetCurrentFieldName());
                soundsHandler.PauseBackground();
                soundsHandler.Play(soundsLibrary.GetAudioClip(AudioClipName.Finish));
                loadingScreenHandler.Hide(null);
                return;
            }
            
            switch (_state)
            {
                case FieldState.Wife:
                    ShowHusbandField();
                    break;
                case FieldState.Husband:
                    ShowWifeField();
                    break;
            }
            loadingScreenHandler.Hide(()=>hintHandler.Show($"Ход {GetCurrentFieldName()}"));
        });
    }

    private string GetCurrentFieldName()
    {
        switch (_state)
        {
            case FieldState.Wife:
                return "Невесты";
            case FieldState.Husband:
                return "Жениха";
        }

        return "";
    }
    
    private int GetCurrentScore()
    {
        switch (_state)
        {
            case FieldState.Wife:
                return _scoreWife;
            case FieldState.Husband:
                return _scoreHusband;
        }

        return 0;
    }

    private string GetNameChallenge(WarshipsCell.CellType type)
    {
        switch (type)
        {
            case WarshipsCell.CellType.Karaoke: return "Караоке!";
            case WarshipsCell.CellType.Dance: return "Танцем!";
        }

        return "";
    }

    private enum FieldState
    {
        Wife,
        Husband
    }
}
