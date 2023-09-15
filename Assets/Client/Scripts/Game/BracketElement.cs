using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class BracketElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private TournamentStage _tournamentStage;
    [SerializeField] private TournamentSide _tournamentSide;

    public TournamentStage Stage => _tournamentStage;
    public TournamentSide Side => _tournamentSide;

    public bool Used { get; private set; }

    public void Initialize(string text)
    {
        _textMeshProUGUI.text = text;
    }

    public void Clear()
    {
        _textMeshProUGUI.text = "";
    }
    
    public enum TournamentStage
    {
        Semifinal,
        Final,
        Winner
    }
    
    public enum TournamentSide
    {
        Left,
        Right,
        Centre
    }
}
