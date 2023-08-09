using SpaceFusion.SF_RecrutementCenter.Scripts.Scriptables;
using UnityEngine;

namespace SpaceFusion.SF_RecrutementCenter.Scripts {
    public class DebugCharacter : MonoBehaviour
    {
        private Character character;

        private void OnEnable()
        {
            CharacterSelector.OnCharacterChange += ChangeCharacter;
        }

        private void OnDisable()
        {
            CharacterSelector.OnCharacterChange -= ChangeCharacter;
        }


        private void ChangeCharacter(Character character)
        {
            this.character = character;
        }


        public void DebugAddFragments()
        {
            character.AddFragments(10);
            CharacterSelector.OnCharacterChange.Invoke(character);
        }

        /// <summary>
        /// NOTE: if you want to see the character lock again on the Scrollrect buttons, please restart the scene!
        /// </summary>
        public void DebugReset()
        {
            character.DebugResetStats();
            CharacterSelector.OnCharacterChange.Invoke(character);
        }


        public void DebugLevelUp()
        {
            if (character.IsUnlocked())
            {
                character.AddXp(character.GetNeededXp());
                CharacterSelector.OnCharacterChange.Invoke(character);
            }
        }
    }
}