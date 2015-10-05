using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Represents "box" showing how long until a character is able to move.
/// </summary>
public class TurnDisplay : MonoBehaviour
{ 

    public Color BaseColor;
    public Color OverColor;

    [SerializeField] private Text _text;
    [SerializeField] private Image _baseTicksImage;
    [SerializeField] private Image _overTicksImage;
    [SerializeField] private int _maxBaseTicks;
    [SerializeField] private Vector2 _tickBarMaxSize;
    
    void Awake()
    {
        _baseTicksImage.color = BaseColor;
        _overTicksImage.color = OverColor;
    }

    /// <summary>
    /// Draws tick
    /// </summary>
    /// <param name="ticks">Ticks until action</param>
    /// <param name="name">Name of character (TO BE REPLACED BY AbstractBattleCharacter)</param>
    public void DrawTurn(int ticks, String name)
    {
        var bars = (float) ticks/_maxBaseTicks;

        var baseBarSize = _tickBarMaxSize.x * bars;
        if (baseBarSize > _tickBarMaxSize.x)
            baseBarSize = _tickBarMaxSize.x;

        var overBarSize = (_tickBarMaxSize.x* (bars - 1))/2;
        if (overBarSize > _tickBarMaxSize.x)
            overBarSize = _tickBarMaxSize.x;

        _baseTicksImage.rectTransform.sizeDelta = new Vector2(baseBarSize,
                                                  _tickBarMaxSize.y);

        
        _overTicksImage.rectTransform.sizeDelta = new Vector2(overBarSize,
                                          _tickBarMaxSize.y);

        _text.text = name;

        _baseTicksImage.rectTransform.anchoredPosition = new Vector2(0, 0);
        _overTicksImage.rectTransform.anchoredPosition = new Vector2(0, 0);
        _text.rectTransform.anchoredPosition = new Vector2(baseBarSize + 2,0);

        if (_baseTicksImage.rectTransform.sizeDelta.x > 0)
            _baseTicksImage.gameObject.SetActive(true);

        if (_overTicksImage.rectTransform.sizeDelta.x > 0)
            _overTicksImage.gameObject.SetActive(true);


        _text.gameObject.SetActive(true);
    }
}
