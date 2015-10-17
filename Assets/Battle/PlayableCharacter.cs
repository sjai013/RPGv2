using Battle.Abilities;
using ExtensionMethods;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle
{
    public class PlayableCharacter : AbstractBattleCharacter
    {

        protected void Start()
        {
            OnAddCharacter(this);
            OnUpdateTurns(this);
            
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
