using System;
using SpaceFusion.SF_RecrutementCenter.Scripts.Scriptables;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceFusion.SF_RecrutementCenter.Scripts {
    /// <summary>
    /// NOTE: CharacterSelector.Set() method which is called for each Button at the start, will ensure that
    /// OnCharacterChange event is triggered for the activated character. which will allow this class to keep track of the currently selected character
    /// </summary>
    public class ActivateCharacter : MonoBehaviour
    {
        
        public static Action OnCharacterActivation;
    
        public GameObject characterSwitchForm;
        public Image activeCharFrame;
        public Image activeCharIcon;
        public Image currentCharFrame;
        public Image currentCharIcon;
        /// <summary>
        /// animated arrows for the CharacterChangePanel 
        /// </summary>
        [Header("To reset the transparency for animator ")]
        public Image[] arrows;

        private Character currentCharacter;
        private Character activeCharacter;

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
            currentCharacter = character;
        }

        /// <summary>
        /// if the User clicks on the selected character image in the displayed info, the CharacterChangePanel will be opened.
        /// The Panel allows the user to switch between the actvated and the selected character!
        ///
        /// If The selected character is also the activated(currently used by the player) nothing will happen
        /// </summary>
        public void ConfirmCharacterChangePanel()
        {
            // if character already selected then nothing has to be done
            if (currentCharacter.IsActivated()) return;
            activeCharacter = CharacterList.instance.GetActive();
            // if no active character available, then its the first assignment
            // just assign the current character
            if (activeCharacter == null)
            {
                Accept();
                return;
            }

            foreach (var arrow in arrows)
            {
                arrow.color = new Color(1, 1, 1, 0.3f);
            }

            activeCharFrame.color = activeCharacter.GetStrengthColor();
            activeCharIcon.sprite = activeCharacter.image;

            currentCharFrame.color = currentCharacter.GetStrengthColor();
            currentCharIcon.sprite = currentCharacter.image;

            characterSwitchForm.SetActive(true);
        }

        /// <summary>
        /// Accept the new character and triggers OnCharacterActivation with the new character as parameter
        /// </summary>
        public void Accept()
        {
            if (activeCharacter != null)
            {
                activeCharacter.SetActivationStatus(false);
            }

            currentCharacter.SetActivationStatus(true);
            OnCharacterActivation.Invoke();
            characterSwitchForm.SetActive(false);
        }
    }
}