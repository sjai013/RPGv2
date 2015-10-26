using UnityEngine;
using System.Collections;
using Battle.Events.BattleCharacter;
using JainEventAggregator;
using UnityEngine.UI;

public class DamageText : MonoBehaviour, IListener<TakingDamage>
{

    [SerializeField] private GameObject _damageText;
	// Use this for initialization
	void Start ()
    {
	    this.RegisterAllListeners();
	}
	

    public void Handle(TakingDamage message)
    {
        var go = Instantiate(_damageText);
        go.transform.localScale = _damageText.transform.localScale;
        go.transform.position = message.Target.transform.position + _damageText.transform.localPosition + message.Target.transform.forward * -1;
        go.GetComponentInChildren<Text>().text = message.Damage.damage.ToString();
    }
}
