using TMPro;
using UnityEngine;

public class FinishScreen : MonoBehaviour
{
    [SerializeField] private Color husbandColor;
    [SerializeField] private Color wifeColor;
    [SerializeField] private TextMeshProUGUI _winTitle;
    [SerializeField] private TextMeshProUGUI _teamTitle;
    [SerializeField] private TextMeshProUGUI _scoreHusband;
    [SerializeField] private TextMeshProUGUI _scoreWife;
    [SerializeField] private Transform Body;

    public void Show(int scoreHusband, int scoreWife, int targetCount, string teamName)
    {
        Body.gameObject.SetActive(true);
        var isWifeWin = scoreWife >= targetCount;

        Debug.Log($"isWifeWin - {isWifeWin}");
        
        _winTitle.color = isWifeWin ? wifeColor : husbandColor;
        _teamTitle.color = isWifeWin ? wifeColor : husbandColor;
        _scoreHusband.text = scoreHusband.ToString();
        _scoreWife.text = scoreWife.ToString();

        _teamTitle.text = $"КОМАНДА {teamName.ToUpper()}";
    }
}
