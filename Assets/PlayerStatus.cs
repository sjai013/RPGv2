using UnityEngine;
using System.Collections;
using Battle;
using UnityEngine.UI;
using System;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private AbstractBattleCharacter _character;
    [SerializeField] private Text _name;
    [SerializeField] private Text _mpText;
    [SerializeField] private Text _hpText;
    [SerializeField] private GameObject _uiContainerGameObject;
    [SerializeField] private GameObject _highlightImageGameObject;

    [SerializeField] public AbstractBattleCharacter Character {  get { return _character; } set { ChangeCharacter(value);} }

    void Awake()
    {

    } 

    // Use this for initialization
	void Start ()
	{
	    if (_character == null)
	    {
	        //Debug.LogError("Battle Character missing: " + gameObject.name);
            _uiContainerGameObject.SetActive(false);
	        return;
	    }

        _uiContainerGameObject.SetActive(true);

        RegisterEventHandlers(_character);
        UpdateName(_character.General.Name);

        _character.RefreshHandlers();
    }

    void RegisterEventHandlers(AbstractBattleCharacter battleCharacter)
    {
        _character.Stats.HpChanged += UpdateHp;
        _character.Stats.MpChanged += UpdateMp;
        _character.Stats.HpCritical += HpCritical;
        _character.Highlight += HighlightThis;
        _character.Unhighlight += UnhighlightThis;
    }

    void UnregisterEventHandlers(AbstractBattleCharacter battleCharacter)
    {
        _character.Stats.HpChanged -= UpdateHp;
        _character.Stats.MpChanged -= UpdateMp;
        _character.Stats.HpCritical -= HpCritical;
        _character.Highlight -= HighlightThis;
        _character.Unhighlight -= UnhighlightThis;
    }


    void ChangeCharacter(AbstractBattleCharacter newBattleChar)
    {
        
    }

    // Update is called once per frame
    void Update ()
    {

    }

    void UpdateName(string value)
    {
        _name.text = value.ToString();
    }

    void UpdateHp(int value)
    {
        _hpText.text = value.ToString();
    }

    void UpdateMp(int value)
    {
        _mpText.text = value.ToString();
    }

    void HpCritical(bool value)
    {
        _hpText.color = new Color(1, 1, 1);
        _name.color = new Color(1,1,1);
        if (value)
        {
            _hpText.color = new Color(1,1,0);
            _name.color = new Color(1,1,0);
        }
    }

    void HighlightThis(AbstractBattleCharacter battleCharacter)
    {
        if (battleCharacter == (AbstractBattleCharacter) _character)
        {
            _highlightImageGameObject.SetActive(true);
        }
        
    }

    void UnhighlightThis(AbstractBattleCharacter battleCharacter)
    {
        if (battleCharacter == (AbstractBattleCharacter)_character)
        {
            _highlightImageGameObject.SetActive(false);
        }
    }


}
