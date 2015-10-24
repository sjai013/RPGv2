using System;
using Battle.Abilities;
using Battle.Events.BattleCharacter;
using ExtensionMethods;
using JainEventAggregator;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle
{
    public class PlayableCharacter : AbstractBattleCharacter
    {

        protected override void Initialise()
        {
            EventAggregator.RaiseEvent(new AddBattleCharacter() {BattleCharacter = this});            
        }

        protected override void TakeAction(AbstractBattleCharacter character)
        {        
            base.TakeAction(character);
            Debug.Log(General.Name + " performing action.");
        }

        public override void Highlight()
        {
            throw new System.NotImplementedException();
        }

        public override void Unhighlight()
        {
            throw new System.NotImplementedException();
        }

    }
}
