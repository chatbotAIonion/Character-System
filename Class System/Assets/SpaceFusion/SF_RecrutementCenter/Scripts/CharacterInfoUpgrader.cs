using SpaceFusion.SF_RecrutementCenter.Scripts.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceFusion.SF_RecrutementCenter.Scripts {
    /// <summary>
    /// NOTE: every time OnCharacterChange event is triggered, we need to update the Information panel
    /// </summary>
    public class CharacterInfoUpgrader : MonoBehaviour
    {
        public TextMeshProUGUI name;
        public TextMeshProUGUI race;
        public TextMeshProUGUI levelInfo;
        public TextMeshProUGUI currentXp;
        public TextMeshProUGUI neededXp;
        public RectTransform expBar;
        public TextMeshProUGUI characterDescription;
        public TextMeshProUGUI skillInfo;
        public Image characterIcon;
        public Image characterFrame;
        public Image skillIcon;
        public Image rank;

        [Header("animation/image that shows selected character!")]
        public GameObject activatedAnimation;

        [Header("Passive Skills")] public TextMeshProUGUI passive1;

        [Header("Unlock Panel")] public GameObject unlockButton;
        public Image characterFragment;
        public TextMeshProUGUI fragmentDisplayer;
        public GameObject unlockPseudoButton;

        [Header("first entry = lowest rank, last entry = highest rank")]
        public Sprite[] rankSprites;

        private void OnEnable()
        {
            CharacterSelector.OnCharacterChange += UpdateCharacterInfo;
            // we need this part to activate the animation when the user switches character
            // in this case OnCharacterChange is triggered first (char still not activated)
            // and then OnCharacterActivation if the user chooses the character!
            ActivateCharacter.OnCharacterActivation += AdaptSelectedDisplayInfo;
        }

        private void OnDisable()
        {
            CharacterSelector.OnCharacterChange -= UpdateCharacterInfo;
            ActivateCharacter.OnCharacterActivation -= AdaptSelectedDisplayInfo;
        }

        private Character character;

        private void UpdateCharacterInfo(Character character)
        {
            this.character = character;
            character.LoadCurrentStats();
            name.text = character.name;
            race.text = character.characterClass.name;
            levelInfo.text = character.GetCurrentLevel() + " / " + character.GetMaxLevel();
            currentXp.text = character.GetCurrentXp().ToString();
            neededXp.text = character.GetNeededXp().ToString();
            characterDescription.text = character.description;
            skillInfo.text = character.skill.name + ":\n" + character.skill.description;
            characterIcon.sprite = character.image;
            characterFrame.color = character.GetStrengthColor();
            rank.sprite = rankSprites[character.GetRank()];
            rank.color = character.GetStrengthColor();
            skillIcon.sprite = character.skill.skillIcon;
            // if unlocked then disable, otherwise enable unlockButton
            unlockButton.SetActive(!character.IsUnlocked());
            characterFragment.sprite = character.image;
            fragmentDisplayer.text = character.getAvailableFragments() + "/" + character.GetNeededFragmentsToUnlock();
            unlockPseudoButton.SetActive(character.getAvailableFragments() >= character.GetNeededFragmentsToUnlock());

            // passive skills;
            passive1.text = character.GetPassiveSkill();

            // show animation that character is currently selected/activated by the user
            activatedAnimation.SetActive(character.IsActivated());

            // set Exp bar properly:
            float exp = character.GetCurrentXp() / character.GetNeededXp();
            expBar.localScale = new Vector3(exp, 1, 1);
        }

        /// <summary>
        /// Enables/disables the ActivatedCharacter animation
        /// </summary>
        private void AdaptSelectedDisplayInfo()
        {
            activatedAnimation.SetActive(character.IsActivated());
        }
    }
}