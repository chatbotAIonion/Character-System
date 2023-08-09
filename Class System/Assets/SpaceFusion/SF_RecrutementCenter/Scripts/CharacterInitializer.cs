using System.Collections.Generic;
using SpaceFusion.SF_RecrutementCenter.Scripts.Scriptables;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceFusion.SF_RecrutementCenter.Scripts {
    /// <summary>
    /// Initial script that creates CharacterInfoButtons for all available characters!
    /// 
    /// On Start: displays all characters that are stored in the characterList!
    /// whenever a CharacterClass filter method is called:
    /// clears the characters panel and displays only the characters that match the filter!
    /// </summary>
    public class CharacterInitializer : MonoBehaviour
    {
        public GameObject infoButton;

        // tracks the currently displayed objects
        private List<GameObject> displayedObjects = new List<GameObject>();

        /// <summary>
        /// Initially add all characters to the character panel
        /// </summary>
        private void Start()
        {
            AddAll();
        }

        /// <summary>
        /// Instantiates a "CharacterInfoButton" for a specific character
        /// Button should contain an Gameobject with the name "Image" where the character image is stored
        /// </summary>
        /// <param name="character"></param>
        private void AddToPanel(Character character)
        {
            var obj = Instantiate(infoButton, transform);
            displayedObjects.Add(obj);
            obj.GetComponent<CharacterSelector>().Set(character);
            obj.transform.Find("Image").GetComponent<Image>().sprite = character.image;
            obj.GetComponent<Image>().color = character.GetStrengthColor();
        }

        /// <summary>
        /// Clears all tracked characters from the list
        /// </summary>
        private void ClearPanel()
        {
            foreach (var displayedChar in displayedObjects)
            {
                Destroy(displayedChar);
            }

            displayedObjects.Clear();
        }

        /// <summary>
        /// disables the character filter and displays all available characters!
        /// </summary>
        public void AddAll()
        {
            ClearPanel();
            foreach (var character in CharacterList.instance.list)
            {
                AddToPanel(character);
            }
        }

        /// <summary>
        /// clears the characters panel and only displays characters that matches the filtered CharacterClass
        /// </summary>
        /// <param name="characterClass"></param>
        public void FilterByCharacterClass(string characterClass)
        {
            ClearPanel();
            foreach (var character in CharacterList.instance.GetByClass(characterClass))
            {
                AddToPanel(character);
            }
            //after finished display the first character in the new list(update the InfoPanel)
            displayedObjects[0].GetComponent<Button>().onClick.Invoke();
        }
    }
}