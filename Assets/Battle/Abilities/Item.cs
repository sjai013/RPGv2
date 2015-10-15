using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Abilities.Damage;

namespace Battle.Abilities
{

    public class Item : AbstractAbilityContainer
    {
        public override int ActionCost { get { return 2; } }
        public override string Name { get { return "Item"; } }
    }
}
