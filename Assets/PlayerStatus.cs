using UnityEngine;
using System.Collections;
using Battle;
using UnityEngine.UI;
using System;
using Battle.Events.BattleCharacter;
using Battle.Events.BattleCharacter.Stats;
using JainEventAggregator;

public class PlayerStatus : MonoBehaviour, IListener<HpChanged>, IListener<MpChanged>, IListener<HighlightBattleCharacter>
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

    void OnDestroy()
    {
        this.UnregisterAllListeners();
    }

    void RegisterEventHandlers(AbstractBattleCharacter battleCharacter)
    {
        this.RegisterAllListeners();
    }

    void UnregisterEventHandlers(AbstractBattleCharacter battleCharacter)
    {
        this.UnregisterAllListeners();
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

    void UpdateHp(HpChanged value)
    {
        _hpText.text = value.Hp.ToString();

        switch (value.State)
        {
            case HpChanged.EState.Critical:
                _hpText.color = new Color(1, 1, 0);
                _name.color = new Color(1, 1, 0);
                break;
            case HpChanged.EState.Dead:
                _hpText.color = new Color(1, 0, 0);
                _name.color = new Color(1, 0, 0);
                break;
            case HpChanged.EState.None:
                _hpText.color = new Color(1, 1, 1);
                _name.color = new Color(1, 1, 1);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    private void UpdateMp(MpChanged value)
    {
        _mpText.text = value.Mp.ToString();

        switch (value.State)
        {
            case MpChanged.EState.None:
                _mpText.color = new Color(1,1,1);
                break;
            case MpChanged.EState.Critical:
                _mpText.color = new Color(1,1,0);
                break;
            case MpChanged.EState.Empty:
                _mpText.color = new Color(1,0,0);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    public void Handle(HighlightBattleCharacter message)
    {
        if (message.BattleCharacter != this.Character) return;
        _highlightImageGameObject.SetActive(message.Highlighted);
    }

    public void Handle(HpChanged message)
    {
        if (message.BattleCharacter == _character)
        {
            UpdateHp(message);
        }
    }

    public void Handle(MpChanged message)
    {
        if (message.BattleCharacter == _character)
        {
            UpdateMp(message);
        }
    }

}
