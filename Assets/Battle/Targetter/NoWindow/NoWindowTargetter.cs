using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Abilities;
using Battle.Turn;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Targetter.NoWindow
{
    public class NoWindowTargetter : AbstractTargetter
    {
        private Dictionary<AbstractBattleCharacter, GameObject> _friendlyPointers = new Dictionary<AbstractBattleCharacter, GameObject>();
        private Dictionary<AbstractBattleCharacter, GameObject> _enemyPointers = new Dictionary<AbstractBattleCharacter, GameObject>();
        [SerializeField] protected GameObject _allyPointerPrefab;
        [SerializeField] protected GameObject _enemyPointerPrefab;

        void Awake()
        {
            base.Awake();
            AbstractAbility.AbilitySubmit += PrepareTargets;
        }

        public override void PrepareTargets(AbstractAbility ability)
        {
            //Remove any existing pointers
            foreach (var item in _friendlyPointers)
            {
                Destroy(item.Value);
            }

            _friendlyPointers.Clear();

            foreach (var item in _enemyPointers)
            {
                Destroy(item.Value);
            }

            _enemyPointers.Clear();

            List<AbstractBattleCharacter> sortedFriendly = AbstractBattleCharacter.Friendlies.OrderBy(x => x.transform.localPosition.x).ToList();
            List<AbstractBattleCharacter> sortedEnemy = AbstractBattleCharacter.Enemies.OrderBy(x => x.transform.localPosition.x).ToList();

            foreach (var character in AbstractBattleCharacter.Friendlies)
            {
                //Spawn a pointer at the character
                GameObject pointer = (GameObject)Instantiate(_allyPointerPrefab);
                pointer.transform.SetParent(character.gameObject.transform);
                pointer.transform.localPosition = new Vector3(-character.Size().x, character.Size().y, 0);
                _friendlyPointers.Add(character,pointer);
            }

            foreach (var character in AbstractBattleCharacter.Enemies)
            {
                //Spawn a pointer at the character
                GameObject pointer = (GameObject)Instantiate(_enemyPointerPrefab);
                pointer.transform.SetParent(character.gameObject.transform);
                pointer.transform.localPosition = new Vector3(-character.Size().x, character.Size().y, 0);
                _enemyPointers.Add(character, pointer);
            }

            foreach (var character in AbstractBattleCharacter.Friendlies)
            {
                SetButtonNavigation(_friendlyPointers[character],character,sortedFriendly,_enemyPointers, sortedEnemy);
                SetButtonEvent(_friendlyPointers[character], AbstractBattleCharacter.ActiveBattleCharacter, new List<AbstractBattleCharacter> {character}, ability);
            }

            foreach (var character in AbstractBattleCharacter.Enemies)
            {
                SetButtonNavigation(_enemyPointers[character], character, sortedEnemy,_friendlyPointers, sortedFriendly);
                SetButtonEvent(_enemyPointers[character], AbstractBattleCharacter.ActiveBattleCharacter, new List<AbstractBattleCharacter> { character }, ability);
            }

            SelectFirst(ability,sortedFriendly,sortedEnemy);
        }

        private void SelectFirst(AbstractAbility ability, List<AbstractBattleCharacter> sortedFriendly, List<AbstractBattleCharacter> sortedEnemy)
        {
            switch (ability.DefaultTarget)
            {
                case AbstractAbility.DefaultTargetType.Self:
                    _friendlyPointers[AbstractBattleCharacter.ActiveBattleCharacter].GetComponentInChildren<Button>().Select();
                    break;
                case AbstractAbility.DefaultTargetType.Allies:
                    _friendlyPointers[sortedFriendly[0]].GetComponentInChildren<Button>().Select();
                    break;
                case AbstractAbility.DefaultTargetType.Enemy:
                    _enemyPointers[sortedEnemy[0]].GetComponentInChildren<Button>().Select();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private AbstractBattleCharacter ToRight(AbstractBattleCharacter thisBattleCharacter, IEnumerable<AbstractBattleCharacter> sortedOtherBattleCharacters)
        {
            foreach (var character in sortedOtherBattleCharacters)
            {
                if (character.transform.position.x > thisBattleCharacter.transform.position.x)
                {
                    return character;
                }
            }

            return thisBattleCharacter;
        }

        private AbstractBattleCharacter ToLeft(AbstractBattleCharacter thisBattleCharacter, IEnumerable<AbstractBattleCharacter> sortedOtherBattleCharacters)
        {
            foreach (var character in sortedOtherBattleCharacters.Reverse())
            {
                if (character.transform.position.x < thisBattleCharacter.transform.position.x)
                {
                    return character;
                }
            }

            return thisBattleCharacter;
        }

        /// <summary>
        /// Configures buttons for navigation
        /// </summary>
        /// <param name="pointer">The pointer to configure.</param>
        /// <param name="character">Character the pointer belongs to</param>
        /// <param name="sortedCharacterList">Sorted list of friendly or enemy characters (same group which CHARACTER belongs to).</param>
        /// <param name="oppositePointers">Pointers for other group (i.e. if CHARACTER is friendly, then enemy pointer list).</param>
        /// <param name="oppositeSortedCharacters">Sorted list for other group (i.e. if CHARACTER is friendly, then enemy characters list).</param>
        private void SetButtonNavigation(GameObject pointer, AbstractBattleCharacter character, List<AbstractBattleCharacter> sortedCharacterList, Dictionary<AbstractBattleCharacter, GameObject> oppositePointers, List<AbstractBattleCharacter> oppositeSortedCharacters)
        {
            var button = pointer.GetComponentInChildren<Button>();
            Navigation navigation = new Navigation {mode = Navigation.Mode.Explicit};
            navigation.selectOnUp = navigation.selectOnDown = navigation.selectOnUp = navigation.selectOnDown = oppositePointers[oppositeSortedCharacters[0]].GetComponentInChildren<Button>();
            // oppositePointers[sortedCharacterList[0]].GetComponentInChildren<Button>();
            navigation.selectOnLeft = ToLeft(character, sortedCharacterList).GetComponentInChildren<Button>();
            navigation.selectOnRight = ToRight(character, sortedCharacterList).GetComponentInChildren<Button>();
            button.navigation = navigation;
        }

        // For now assume single-target abilities only
        private void SetButtonEvent(GameObject pointer, AbstractBattleCharacter caster, List<AbstractBattleCharacter> targets, AbstractAbility ability)
        {
            pointer.GetComponentInChildren<Button>().onClick.AddListener(delegate() {OnActionTargetSelected(caster,targets,ability);});
        }
    }
}
