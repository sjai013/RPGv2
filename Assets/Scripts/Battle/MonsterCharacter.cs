using Battle.Events.TurnSystem;
using JainEventAggregator;
using UnityEngine;

namespace Battle
{
    public class MonsterCharacter : AbstractBattleCharacter
    {

        protected override void Initialise()
        {
            throw new System.NotImplementedException();
        }

        // Update is called once per frame
        void Update () {
	
        }

        protected override void TakeAction()
        {
            EventAggregator.RaiseEvent(new TakeManualAction() { BattleCharacter = this as AbstractBattleCharacter });

            Debug.Log(this.name + " taking action.");
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
