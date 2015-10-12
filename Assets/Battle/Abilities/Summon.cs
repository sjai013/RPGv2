﻿using System;
using Battle.Abilities.Damage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battle.Abilities
{

    public class Summon : AbstractAbilityContainer
    {
        public override AbstractDamageBehaviour DamageBehaviour
        {
            get { throw new NotImplementedException(); }
        }

        public override int ActionCost { get { return 3; } }
        public override string Name { get { return "Summon"; } }
        public override TargetTypes TargetType { get { throw new NotImplementedException(); } }
        public override DefaultTargetType DefaultTarget { get { throw new NotImplementedException(); } }
    }
}
