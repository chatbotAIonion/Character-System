using System;
using SpaceFusion.SF_RecrutementCenter.Scripts.Scriptables;
using UnityEngine;

namespace SpaceFusion.SF_RecrutementCenter.Scripts {
    /// <summary>
    /// Script that handles the character unlock.
    /// if the player has saved enough character fragments and klicks on unlock,
    /// the payment of the needed fragments is processes, and the character will be marked as unlocked!
    /// </summary>
    public class UnlockCharacter : MonoBehaviour
    {
        private Character character;
        public GameObject buyPanel;
        public static Action<Character> OnCharUnlock;

        private void OnEnable()
        {
            CharacterSelector.OnCharacterChange += ChangeCharacter;
        }

        private void OnDisable()
        {
            CharacterSelector.OnCharacterChange -= ChangeCharacter;
        }

        /// <summary>
        /// keeps track of the currently displayed character
        /// </summary>
        /// <param name="character"></param>
        private void ChangeCharacter(Character character)
        {
            this.character = character;
        }

        /// <summary>
        /// if the user has enough fragments to buy the currently displayed character the lock will be removed
        /// so the user can activate the character if he wants!
        /// </summary>
        public void Unlock()
        {
            var unlocked = character.PayFragments(character.GetNeededFragmentsToUnlock());
            if (!unlocked) return;
            character.Unlock();
            buyPanel.SetActive(false);
            OnCharUnlock?.Invoke(character);
        }
    }
}