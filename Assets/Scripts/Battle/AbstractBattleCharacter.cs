using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Abilities;
using Battle.Abilities.Damage;
using Battle.Events.BattleCharacter;
using Battle.Events.BattleCharacter.Turns;
using JainEventAggregator;
using UnityEngine;

namespace Battle
{

    /// <summary>
    /// Class for managing all characters which can take action (i.e. player, monsters)
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public abstract class AbstractBattleCharacter: MonoBehaviour, IListener<AddBattleCharacter>, IListener<RemoveBattleCharacter>, IListener<TakeAction>
    {
        public AnimationParameters AnimParameters { get; private set; }

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

        public Vector3 OriginalPosition { get; private set; }
        public Quaternion OriginalRotation { get; private set; }

        private List<AbstractAbility> _knownAbilities = new List<AbstractAbility>() {new Attack(), new Item()};

        public List<AbstractAbility> Abilities { get { return _knownAbilities; } }

            /// <summary>
        /// Boolean to indicate whether character is in combat (i.e. it could be on the "side-benches").
        /// </summary>
        [SerializeField] protected Boolean _inCombat;

        /// <summary>
        /// Storage for all AbstractBattleCharacters instantiated.
        /// </summary>
        private static List<AbstractBattleCharacter> _instances = new List<AbstractBattleCharacter>();
        public static List<AbstractBattleCharacter> Characters { get { return _instances; } }

        /// <summary>
        /// Storage for all friendly characters (PlayableCharacter)
        /// </summary>
        private static List<AbstractBattleCharacter> _friendly = new List<AbstractBattleCharacter>();
        public static List<AbstractBattleCharacter> Friendlies { get { return _friendly; } }

        /// <summary>
        /// Storage for all enemy characters (MonsterCharacter)
        /// </summary>
        private static List<AbstractBattleCharacter> _enemies = new List<AbstractBattleCharacter>();
        public static List<AbstractBattleCharacter> Enemies { get { return _enemies; } }

        /// <summary>
        /// Current active character (i.e. the one that is waiting to perform an action).
        /// </summary>
        [SerializeField] public static AbstractBattleCharacter ActiveBattleCharacter { get; private set; }

        [SerializeField] private Sprite _charSprite;
        public Sprite CharSprite { get { return _charSprite; }}

        [SerializeField] private Boolean isActive;

        protected void Awake()
        {  
            if (isActive)
                ActiveBattleCharacter = this;

            RegisterEventHandlers();
            AnimParameters = new AnimationParameters(GetComponent<Animator>());
            EventAggregator.RaiseEvent(new AddBattleCharacter() { BattleCharacter = this });
        }

        protected void Start()
        {
            
        }

        protected abstract void Initialise();

        protected void OnEnable()
        {
            OriginalPosition = transform.position;
            OriginalRotation = transform.rotation;
        }

        
        private void AnimateCharacter(AbstractBattleCharacter caster, List<AbstractBattleCharacter> target, AbstractActionAbility ability)
        {
            if (this != caster) return;
            EventAggregator.RaiseEvent(new CharacterAnimating() {Caster =  caster, Targets = target, Ability = ability});
        }
        
        
        private void RegisterEventHandlers()
        {
           this.RegisterAllListeners();
            //TODO: Fix
           //AbstractTurnSystem.TurnSystem.TakeAction += SetActiveCharacter;
        }

        private static void SetActiveCharacter(AbstractBattleCharacter thisbattlecharacter)
        {
            ActiveBattleCharacter = thisbattlecharacter;
        }

        public bool IsFriendly()
        {
            return Friendlies.Any(c => c.Equals(this));
        }

        public bool IsEnemy()
        {
            return Enemies.Any(c => c.Equals(this));
        }

        protected virtual void TakeAction(AbstractBattleCharacter character)
        {
            ActiveBattleCharacter = this;
        }

        public void RefreshHandlers()
        {
            Stats.Refresh();
        }

        public void Remove()
        {
            EventAggregator.RaiseEvent(new RemoveBattleCharacter() { BattleCharacter = this });
        }

        
        public void DoDamage(List<AbstractBattleCharacter> targets, AbstractActionAbility ability)
        {
            var damage = ability.DoDamage(this,targets);
            EventAggregator.RaiseEvent(new DoingDamage() { Damage = damage, Targets = targets });
            TakeDamage(targets, damage);
        }


        private void TakeDamage(List<AbstractBattleCharacter> targets, AbstractDamageBehaviour.Damage damage)
        {
            //TODO: Need a KeyValue Pair for target and damage, so each target has a separate associated damage instance
            foreach (var target in targets)
            {
                target.Stats.Hp -= damage.damage;
                var animator = target.GetComponent<Animator>();
                if (animator != null)
                    animator.SetTrigger("takeDamage");

                EventAggregator.RaiseEvent(new TakingDamage() {Damage = damage,Target = target});
            }
        }

        public abstract void Highlight();
        public abstract void Unhighlight();

        public void Handle(AddBattleCharacter message)
        {
            if (message.BattleCharacter != this) return;
            _instances.Add(message.BattleCharacter);
            if (message.BattleCharacter.GetType() == typeof(PlayableCharacter))
            {
                _friendly.Add(message.BattleCharacter);
            }
            else if (message.BattleCharacter.GetType() == typeof(MonsterCharacter))
            {
                _enemies.Add(message.BattleCharacter);
            }
            else
            {
                Debug.Log("Unknown character allegiance");
            }
        }

        public void Handle(RemoveBattleCharacter message)
        {
            if (message.BattleCharacter != this) return;
            _instances.Add(message.BattleCharacter);
            if (message.BattleCharacter.GetType() == typeof(PlayableCharacter))
            {
                _friendly.Remove(message.BattleCharacter);
            }
            else if (message.BattleCharacter.GetType() == typeof(MonsterCharacter))
            {
                _enemies.Remove(message.BattleCharacter);
            }
            else
            {
                Debug.Log("Unknown character allegiance");
            }
        }

        public void Handle(TakeAction message)
        {
            if (this != message.BattleCharacter) return;

            ActiveBattleCharacter = message.BattleCharacter;
        }

        public override string ToString()
        {
            return General.Name;
        }


    }

}
