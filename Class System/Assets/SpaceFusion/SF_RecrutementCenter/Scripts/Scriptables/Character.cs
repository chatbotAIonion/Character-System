using System;
using SpaceFusion.common.Scripts;
using SpaceFusion.common.Scripts.InterfacesAndGenerics;
using UnityEngine;

namespace SpaceFusion.SF_RecrutementCenter.Scripts.Scriptables {
    [CreateAssetMenu(fileName = "newCharacter", menuName = "Characters/Character")]
    public class Character : GenericPlayableObject<CharacterStats>
    {
        public CharacterClass characterClass;
        public Skill skill;
        public string description;
    
        [Header("currently only 1 passive skill supported")]
        public PassiveSkill[] passiveSkills;
    
        /// <summary>
        /// Sets the correct PlayerPrefs key to load and save characters
        /// </summary>
        /// <returns></returns>
        public override string GetKey()
        {
            return "character_"+name;
        }
    
        public void AddXp(int exp)
        {
            stats.currentXp += exp;
            if (stats.currentLevel >= GetMaxLevel())
            {
                return;
            }

            if (stats.currentXp >= stats.neededXp)
            {
                stats.currentXp -= stats.neededXp;
                stats.currentLevel++;
                stats.neededXp = stats.currentLevel * 1000;

                //TODO invoke character level up event....
                // and upgrade Character Stats
            }

            SaveCurrentStats();
        }

        public void AddFragments(int amount)
        {
            stats.fragments += amount;
            SaveCurrentStats();
        }
    
        public override void LoadCurrentStats()
        {
            stats = JsonUtility.FromJson<CharacterStats>(PlayerPrefs.GetString(GetKey(),
                new CharacterStats().ToString()));
        }

        /// <summary>
        /// Selects or deselects the character
        /// </summary>
        /// <param name="choice"> true if character should be selected, false to deselect the character</param>
        public void SetActivationStatus(bool choice)
        {
            stats.isActivated = choice;
            SaveCurrentStats();
        }

        public bool PayFragments(int amount)
        {
            if (stats.fragments < amount)
            {
                return false;
            }

            stats.fragments -= amount;
            SaveCurrentStats();
            return true;
        }

        #region Getters
    
        public int GetCurrentLevel()
        {
            return stats.currentLevel;
        }

        public int GetMaxLevel()
        {
            switch (rank)
            {
                case Rank.R1: return 10;
                case Rank.R2: return 20;
                case Rank.R3: return 30;
                case Rank.R4: return 40;
                default: return 10; // Novice
            }
        }

        public int GetCurrentXp()
        {
            return stats.currentXp;
        }

        public int GetNeededXp()
        {
            return stats.neededXp;
        }
    
        public int GetNeededFragmentsToUnlock()
        {
            return 10;
        }

        public int getAvailableFragments()
        {
            return stats.fragments;
        }
    

        /// <summary>
        /// NOTE: current stats will be loaded first, via PlayerPrefs
        /// </summary>
        public string GetPassiveSkill()
        {
            LoadCurrentStats();
            if (passiveSkills.Length > 0)
            {
                var passiveSkill = passiveSkills[0];
                var bonus = passiveSkill.startValue +
                            (passiveSkill.increasementPerLevel * (float) (stats.currentLevel - 1));
                return passiveSkill.stat + " : " + bonus + " %";
            }

            return "not defined";

        }
    
        public StatIncreasement GetStatIncreasement()
        {
            var increasement = new StatIncreasement(0);
            foreach (var val in passiveSkills)
            {
                increasement.addIncreasement(val.stat,
                    (val.startValue + (val.increasementPerLevel * (GetCurrentLevel() - 1))));
            }
            return increasement;
        }
        #endregion Getters
    
        /// <summary>
        /// CAUTION: this method will overwrite all the character progress with the default lvl1 stats
        /// </summary>
        public void DebugResetStats()
        {
            PlayerPrefs.SetString(GetKey(), new CharacterStats().ToString());
        }
    }

    public class CharacterStats: Stats
    {
        public int currentLevel = 1;
        public int currentXp = 0;
        public int neededXp = 500;
        public int fragments = 0;
    }

    [Serializable]
    public class PassiveSkill
    {
        public AvailableStats stat;

        [Header("Increasement in % --> 5 = 5%")]
        public float startValue;

        public float increasementPerLevel;
    }
}