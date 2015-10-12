using System;
using Battle.Abilities.Damage;

namespace Battle.Abilities
{

    public class Item : AbstractAbilityContainer
    {
        public override AbstractDamageBehaviour DamageBehaviour
        {
            get { throw new NotImplementedException(); }
        }

        public override int ActionCost { get { return 2; } }
        public override string Name { get { return "Item"; } }
        public override TargetTypes TargetType { get { throw new NotImplementedException(); } }
        public override DefaultTargetType DefaultTarget { get { throw new NotImplementedException(); } }
    }
}
