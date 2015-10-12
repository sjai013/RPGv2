

using Battle.Abilities.Damage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle.Abilities
{

    public class Attack : AbstractAbility
    {
        public override AbstractDamageBehaviour DamageBehaviour { get { return new PhysicalDamageBehaviour(16); } }
        public override int ActionCost { get { return 3; }  }
        public override string Name { get {return "Attack";} }
        public override TargetTypes TargetType { get {return TargetTypes.OneEnemyOrFriendly;} }
        public override DefaultTargetType DefaultTarget { get { return DefaultTargetType.Enemy; } }

    }
}
