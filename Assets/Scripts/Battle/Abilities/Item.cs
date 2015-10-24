using System;
using UnityEngine;


namespace Battle.Abilities
{
    public class Item : AbilityContainer
    {
        private const String _name = "Item";
        private static int _actionCost = 2;
        private static AbilityType _abilityType = AbilityType.Container | AbilityType.Base;

        public Item() : base(_name, _actionCost, _abilityType)
        {
        }

        protected override void DoAction()
        {
            Debug.Log(Name);
        }
    }
}
