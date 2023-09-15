using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    public void Initialize(string text)
    {
        gameObject.SetActive(true);
        _textMeshProUGUI.text = text;
    }
}
