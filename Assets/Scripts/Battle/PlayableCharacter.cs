using Battle.Events.BattleCharacter;
using Battle.Events.TurnSystem;
using JainEventAggregator;
using UnityEngine;

namespace Battle
{
    public class PlayableCharacter : AbstractBattleCharacter
    {

        protected override void Initialise()
        {
            EventAggregator.RaiseEvent(new AddBattleCharacter() {BattleCharacter = this});            
        }

        protected override void TakeAction()
        {        
            EventAggregator.RaiseEvent(new TakeManualAction() {BattleCharacter = this as AbstractBattleCharacter});
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
