using System;
using UnityEngine;

namespace Battle.Turn
{
    
    public abstract class AbstractTurnSystem : MonoBehaviour
    {
        /// <summary>
        /// Raised when it is a character's turn.  Function signature is void (AbstractBattleChar), 
        /// where the AbstractBattleChar contains information on which character is to move.
        /// </summary>
        public static AbstractTurnSystem TurnSystem;
        public event BattleCharDelegate TakeAction;

        protected abstract String Identifier { get;}

        private void Awake()
        {
            if (TurnSystem != null)
            {
                Debug.LogWarning("Multiple turn systems trying to load.  Ignoring " + Identifier + ".");
            }
            else
            {
                Debug.Log("Loading Turn System: " + Identifier + ".");
                Initialise();
                TurnSystem = this;
            }
        }

        protected abstract void Initialise();

        protected virtual void OnTakeAction(AbstractBattleCharacter thisbattlecharacter)
        {
            var handler = TakeAction;
            if (handler != null) handler(thisbattlecharacter);
        }
    }
}
