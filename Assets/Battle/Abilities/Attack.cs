

using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Battle.Abilities.Damage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle.Abilities
{

    public class Attack : AbstractActionAbility
    {
        public Attack()
        {
            DamageBehaviour = new List<AbstractDamageBehaviour>
            {
                new PhysicalDamageBehaviour(16)
            };
        }

        protected override sealed List<AbstractDamageBehaviour> DamageBehaviour { get; set; }

        protected IEnumerator SequenceAction(AbstractBattleCharacter caster, AbstractBattleCharacter target)
        {
            //REPLACE THIS WITH A DEDICATED ANIMATION CLASS?

            //TODO: Animate caster moving in for attack

            Debug.Log(DamageBehaviour[0].DoDamage(caster, target));

            //TODO: Animate target taking damage

            yield break;
        }

        public override int ActionCost { get { return 3; }  }

        public override string Name { get {return "Attack";} }
        public override TargetTypes TargetType { get {return TargetTypes.OneEnemyOrFriendly;} }
        public override DefaultTargetType DefaultTarget { get { return DefaultTargetType.Enemy; } }

    }
}
