using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HintHandler : MonoBehaviour
{
    private readonly int AppearTriggerId = Animator.StringToHash("Appear");
    
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Show(string text)
    {
        textMeshPro.text = text;
        _animator.SetTrigger(AppearTriggerId);
        DOVirtual.DelayedCall(0.01f, ()=>_animator.ResetTrigger(AppearTriggerId));
    }
}
