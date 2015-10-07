using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Turn;
using UnityEngine;
using UnityEngineInternal;

namespace Battle
{

    /// <summary>
    /// Class for managing all characters which can take action (i.e. player, monsters)
    /// </summary>
    public abstract class AbstractBattleCharacter: MonoBehaviour
    {
        /// <summary>
        /// General stats, such as character name.
        /// </summary>
        /// 
        public General General { get { return _general; } }
        [SerializeField] protected General _general;

        /// <summary>
        /// Character stats, which determine combat proficiency.
        /// </summary>
        public Stats Stats { get { return _stats; } }
        [SerializeField] protected Stats _stats;

        /// <summary>
        /// Defines the TurnSystem used.
        /// </summary>
        private ITurn _turnSystem;
        public ITurn TurnSystem { get { return _turnSystem; }
            set
            {
                if (_turnSystem == null)
                {
                    _turnSystem = value;
                }
                else
                {
                    Debug.LogError("Attempting to assign multiple Turn Systems.");
                }
            }
        }

        /// <summary>
        /// Boolean to indicate whether character is in combat (i.e. it could be on the "side-benches").
        /// </summary>
        [SerializeField] protected Boolean _inCombat;

        /// <summary>
        /// Storage for all AbstractBattleCharacters instantiated.
        /// </summary>
        protected static List<AbstractBattleCharacter> _instances = new List<AbstractBattleCharacter>();

        public static List<AbstractBattleCharacter> Characters { get { return _instances; } }

        /// <summary>
        /// Current active character (i.e. the one that is waiting to perform an action).
        /// </summary>
        [SerializeField] public static AbstractBattleCharacter ActiveBattleCharacter { get; private set; }

        [SerializeField] private Sprite _charSprite;
        public Sprite CharSprite { get { return _charSprite; }}

        [SerializeField] private Boolean isActive;

        public delegate void BattleCharBattleChar(AbstractBattleCharacter thisBattleCharacter, AbstractBattleCharacter otherBattleCharacter);
        public delegate void BattleChar(AbstractBattleCharacter thisBattleCharacter);
        public delegate void BattleCharInt(AbstractBattleCharacter thisBattleCharacter, int value);

        /// <summary>
        /// Methods to run when a new character is added to the list.  Called in the Awake method of sub classes.
        /// </summary>
        public event BattleChar AddCharacter;

        /// <summary>
        /// Methods to run when a character is removed from the list (in the case of monsters - when they die).  NEVER CALLED.
        /// </summary>
        public event BattleChar RemoveCharacter;

        /// <summary>
        /// Methods to run when a character replaces another character (i.e. swap side-bench and main).  NEVER CALLED.
        /// </summary>
        public event BattleCharBattleChar ReplaceCharacter;

        /// <summary>
        /// Methods to run when character is highlighted (i.e. targetted).  NEVER CALLED.
        /// </summary>
        public event BattleChar Highlight;

        /// <summary>
        /// Methods to ru nwhen character is unhighlighted (i.e. no longer targetted).  NEVER CALLED.
        /// </summary>
        public event BattleChar Unhighlight;

        /// <summary>
        /// Methods to run when turns are updated.  NEVER CALLED.
        /// </summary>
        public event BattleChar UpdateTurns;

        protected virtual void Awake()
        {

            _instances.Add(this);

            if (isActive)
                ActiveBattleCharacter = this;
        }


        void Start()
        {
                
        }


        public void RefreshHandlers()
        {
            Stats.Refresh();
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


    }

}
