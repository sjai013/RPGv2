using System;
using System.Collections;
using System.Collections.Generic;
using Battle.Abilities.Damage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle.Abilities
{

    public class Summon : AbstractAbilityContainer
    {
        public override int ActionCost { get { return 6; } }
        public override string Name { get { return "Summon"; } }
    }
}
