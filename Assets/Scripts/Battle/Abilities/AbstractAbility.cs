using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Abilities;
using Battle.Events.Abilities;
using JainEventAggregator;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Abilities
{
    /// <summary>
    /// Determines where the ability type is drawn
    /// </summary>
    [Flags]
    public enum AbilityType
    {
        /// <summary>
        /// Nothing
        /// </summary>
        None = 0,

        /// <summary>
        /// Appear in main menu
        /// </summary>
        Base = 1 << 0,

        /// <summary>
        /// Performs action
        /// </summary>
        Action = 1 << 1,

        /// <summary>
        /// Opens a sub-menu when selected
        /// </summary>
        Container = 1 << 2,

        /// <summary>
        /// Shows up in the Summon submenu
        /// </summary>
        Summon = 1 << 3,

        /// <summary>
        /// Shows up in the Skill submenu
        /// </summary>
        Skill = 1 << 4,

        /// <summary>
        /// Shows up in the Special submenu
        /// </summary>
        Special = 1 << 5,

        /// <summary>
        /// Shows up in the WhtMgc submenu
        /// </summary>
        WhiteMagic = 1 << 6,

        /// <summary>
        /// Shows up in the BlkMgc submenu
        /// </summary>
        BlackMagic = 1 << 7,

        /// <summary>
        /// Shows up in the Item submenu
        /// </summary>
        Item = 1 << 8, 
    }

    public abstract class AbstractAbility: IListener<AbilitySubmitted>
    {
        public AbilityType AbilityType { get; private set; }
        public AbilityButton AbilityButton { get { return null; } set {} }
        public String Name { get; private set; }
        public int ActionCost { get; private set; }

        protected AbstractAbility(string name, int actionCost, AbilityType abilityType)
        {
            this.Name = name;
            this.ActionCost = actionCost;
            this.AbilityType = abilityType;
            this.RegisterAllListeners();
        }

        protected abstract void DoAction();


        public void Handle(AbilitySubmitted message)
        {
            if (message.Ability != this) return;
            DoAction();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

namespace ExtensionMethods
{
    public static class AbilityExtensions
    {
        public static List<T> FilterAbilities<T>(this List<T> list, AbilityType abilityType) where T : AbstractAbility
        {
            List<T> abilities = list.Where(item => (item.AbilityType & abilityType) != 0).ToList();
            return abilities;
        }

    }
}
