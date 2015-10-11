using Battle.Abilities;
using ExtensionMethods;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle
{
    public class PlayableCharacter : AbstractBattleCharacter
    {
        public static event BattleChar BeginTurn;

        protected void Start()
        {
            OnAddCharacter(this);
            OnUpdateTurns(this);
            
        }

        protected override void TakeAction(AbstractBattleCharacter character)
        {        
            base.TakeAction(character);
            OnBeginTurn(character);
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

        protected virtual void OnBeginTurn(AbstractBattleCharacter thisbattlecharacter)
        {
            var handler = BeginTurn;
            if (handler != null) handler(thisbattlecharacter);
        }
    }
}
