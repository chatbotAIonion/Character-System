using System;
using SpaceFusion.SF_RecrutementCenter.Scripts.Scriptables;
using UnityEngine;

namespace SpaceFusion.SF_RecrutementCenter.Scripts {
    /// <summary>
    /// Script for the character button, which triggers the OnCharacterChange event if klicked
    /// </summary>
    public class CharacterSelector : MonoBehaviour
    {
        public static Action<Character> OnCharacterChange;
        /// <summary>
        /// lock image that prevents the user to select an unlocked character
        /// </summary>
        public GameObject charLock;
        /// <summary>
        /// animation effect to show the user which character is selected
        /// </summary>
        public GameObject selectedDisplayer;
    
        /// <summary>
        /// stores the associated character of this Button (this script is attached to CharacterInfoButton)
        /// </summary>
        private Character character;

        /// <summary>
        /// With OnCharUnlock and OnCharacterActivation events, we make sure that the lock button is destroyed, if the character is unlocked.
        /// Also we make sure that the animated light, which denotes the current selected character, is activated on the correct characterButton
        /// </summary>
        private void OnEnable()
        {
            UnlockCharacter.OnCharUnlock += DestroyLockButton;
            ActivateCharacter.OnCharacterActivation += AdaptSelectedDisplayInfo;
        }

        private void OnDestroy()
        {
            UnlockCharacter.OnCharUnlock -= DestroyLockButton;
            ActivateCharacter.OnCharacterActivation -= AdaptSelectedDisplayInfo;
        }

        /// <summary>
        ///  will be set by CharacterDisplayer for each created CharacterInfoButton
        /// </summary>
        /// <param name="c"></param>
        public void Set(Character c)
        {
            character = c;
            character.LoadCurrentStats();
            if (character.IsUnlocked())
            {
                charLock.SetActive(false);
            }

            Debug.Log("Selected: " + character);
            if (!character.IsActivated()) return;
            selectedDisplayer.SetActive(true);
            // also notify that this character is selected
            // this will ensure that on start the selected character will be displayed in the info panel
            CharacterSelected();
        }

        /**
     * Every time a user clicks on one of the CharacterInfoButtons an OnCharacterChange will be triggered
     */
        public void CharacterSelected()
        {
            OnCharacterChange?.Invoke(character);
        }

        /// <summary>
        /// disables the character lock if the player has bought the character!
        /// TODO: Duplicate: combine with same method from ShipInfo that uses Object as parameter!!
        /// </summary>
        private void DestroyLockButton(Character selected)
        {
            if (character == null) return;
            if (character.name != selected.name) return;
            charLock.SetActive(false);
        }

        /// <summary>
        /// adapts the animation of the selected/activated object
        /// if activated then animate, otherwise turn of animation 
        /// </summary>
        private void AdaptSelectedDisplayInfo()
        {
            selectedDisplayer.SetActive(character.IsActivated());
        }
    }
}