﻿using System;
using Battle.Abilities;
using UnityEngine;

namespace Battle.Events.Abilities
{
    public class HighlightedAbilityChanged
    {
        public AbstractAbility Ability;
    }

    public class AbilitySubmitted
    {
        public AbstractAbility Ability;
        public AbstractBattleCharacter Caster;
    }

    public class AbilityContainerOpened
    {
        public AbilityContainer AbilityContainer;
    }
}
