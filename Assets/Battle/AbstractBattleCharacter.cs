using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{

    public abstract class AbstractBattleCharacter: MonoBehaviour
    {
        [SerializeField] protected General _general;
        [SerializeField] protected Stats _stats;
        [SerializeField] protected Ticks _ticks;
        [SerializeField] protected Boolean _inCombat;

        protected static List<AbstractBattleCharacter> _instances = new List<AbstractBattleCharacter>();

        public delegate void BattleCharBattleChar(AbstractBattleCharacter thisBattleCharacter, AbstractBattleCharacter otherBattleCharacter);
        public delegate void BattleChar(AbstractBattleCharacter thisBattleCharacter);
        public delegate void BattleCharInt(AbstractBattleCharacter thisBattleCharacter, int value);

        public event BattleChar AddCharacter;
        public event BattleChar RemoveCharacter;
        public event BattleCharBattleChar ReplaceCharacter;
        public event BattleChar Highlight;
        public event BattleChar Unhighlight;
        public event BattleChar UpdateTurns;

        public static event BattleCharInt TakeAction;

        protected virtual void Awake()
        {
            _instances.Add(this);
            _ticks = new Ticks(_stats.Agi);

            TakeAction += UpdateTicks;
        }


        public Stats Stats
        {
            get { return _stats; }
        }

        public General General
        {
            get { return _general; }
        }

        public void RefreshHandlers()
        {
            _stats.Refresh();

        }

        public void Remove()
        {
            OnRemoveCharacter(this);
        }

        protected void OnAddCharacter(AbstractBattleCharacter thisBattleCharacter)
        {
            var handler = AddCharacter;
            if (handler != null) handler(thisBattleCharacter);
        }

        protected void OnRemoveCharacter(AbstractBattleCharacter thisBattleCharacter)
        {

            var handler = RemoveCharacter;
            if (handler != null) handler(thisBattleCharacter);
        }


        protected void OnReplaceCharacter(AbstractBattleCharacter thisBattleCharacter, AbstractBattleCharacter otherBattleCharacter)
        {
            var handler = ReplaceCharacter;
            if (handler != null) handler(thisBattleCharacter, otherBattleCharacter);
        }

        public void Replace(AbstractBattleCharacter otherBattleCharacter)
        {
            OnReplaceCharacter(this, otherBattleCharacter);
        }

        public List<int> CalculateTicks(int turns = 16)
        {
            return _ticks.PreviewTicksList(agi: _stats.Agi, nTurns: turns);
        }

        protected virtual void OnHighlight(AbstractBattleCharacter thisbattlecharacter)
        {
            var handler = Highlight;
            if (handler != null) handler(thisbattlecharacter);
        }

        protected virtual void OnUnhighlight(AbstractBattleCharacter thisbattlecharacter)
        {
            var handler = Unhighlight;
            if (handler != null) handler(thisbattlecharacter);
        }

        protected virtual void OnUpdateTurns(AbstractBattleCharacter thisbattlecharacter)
        {
            var handler = UpdateTurns;
            if (handler != null) handler(thisbattlecharacter);
        }

        protected virtual void OnTakeAction(AbstractBattleCharacter thisbattlecharacter, int tickCost)
        {
            var handler = TakeAction;
            if (handler != null) handler(thisbattlecharacter, tickCost);

        }

        protected virtual void UpdateTicks(AbstractBattleCharacter battleCharacter, int tickCost)
        {
            if (battleCharacter == this)
            {
                _ticks.ResetTicks();
                return;
            }

            _ticks.SubtractTicks(tickCost);
        }

        public static List<KeyValuePair<AbstractBattleCharacter,int>> PreviewTurnsList()
        {
            List<KeyValuePair<AbstractBattleCharacter, int>> turns = new List<KeyValuePair<AbstractBattleCharacter, int>>();
            foreach (var character in _instances)
            {
                var ticks = character.CalculateTicks();
                turns.AddRange(ticks.Select(tick => new KeyValuePair<AbstractBattleCharacter, int>(character, tick)));
            }

            turns = turns.OrderBy(turn => turn.Value).Take(16).ToList();

            return turns;
        }

        public static AbstractBattleCharacter NextTurn()
        {
            List<KeyValuePair<AbstractBattleCharacter, int>> turns = new List<KeyValuePair<AbstractBattleCharacter, int>>();
            foreach (var character in _instances)
            {
                if (!character._inCombat) continue;
                var ticks = character.CalculateTicks();
                turns.AddRange(ticks.Select(tick => new KeyValuePair<AbstractBattleCharacter, int>(character, tick)));
            }

            return turns.OrderBy(turn => turn.Value).First().Key;
        } 


    }

}
