using UnityEngine;

namespace SpaceFusion.common.Scripts {
    /**
     *  NEVER CHANGE THESE VALUES !!!!
     *  IF CHANGED THEN THE PROGRESS FROM PLAYERS WILL BE LOST!!!!!
     */
    public class PlayerPrefsReferences : MonoBehaviour {

        private static PlayerPrefsReferences references;

        private void Awake() {
            if (references == null) {
                DontDestroyOnLoad(gameObject);
                references = this;
            } else if (references != this) {
                Destroy(gameObject);
            }
        }

        public const string premiumCurrency = "darkmatter";
        public const string coins = "coins";
        public const string highscore = "highscore";

        public const string damage = "damage";
        public const string health = "health";
        public const string fireRate = "fireRate";
        public const string speed = "speed";

        /// <summary>
        /// current level of the player
        /// </summary>
        public const string currentPlayerLevel = "currentPlayerlevel";

        /// <summary>
        /// current exp of the player
        /// </summary>
        public const string currentPlayerExperience = "currentPlayerExperience";

        /// <summary>
        /// tracks the "progress" of the game if level and xp are the same!
        /// every buy, sell and reward process that does not give xp will increase the progress by 1!
        /// so we can also track that user has bought some ship or got some coins as reward and save it to the cloud!
        /// </summary>
        public const string gameProgress = "gameProgress";


        /// <summary>
        /// returns the highest level where the player successfully killed a boss
        /// </summary>
        public const string highestBossKilled = "BossKilled";

        /// <summary>
        /// used by MissionProgressTracker
        /// returns a ProgressTracker class where the unlocked stars for each level are saved!
        /// Values: 1= Normal | 2 = Hard | 3 = Nightmare
        /// </summary>
        public const string missionTracker = "missionTracker";

        /// <summary>
        /// to save tracked tutorials, so we know which tutorial should still be displayed!
        /// </summary>
        public const string tutorialTracker = "finishedTutorials";

        /// <summary>
        /// returns the currently selected mission difficulty!
        /// Values: 1= Normal | 2 = Hard | 3 = Nightmare
        /// </summary>
        public const string selectedDifficultyKey = "selectedDifficulty";

        /// <summary>
        /// returns the last selected level in campaign mode
        /// </summary>
        public const string selectedLevel = "selectedLvl";

        /// <summary>
        /// returns highest level the user has passed
        /// </summary>
        public const string highestCampaignLevel = "KampagneLevel";

        /// <summary>
        /// Value 1 = directly opens levels by loading KampagneMenu
        /// </summary>
        public const string openLevel = "openLevelHolder";


        /// <summary>
        /// FIXME:
        /// need to figure out where this is used exactly --> see LevelButton.cs/startMission()
        /// </summary>
        public const string currentLevel = "currentLevel";


        /// <summary>
        /// a string that denotes the last time the user spinned the lucky-wheel
        /// </summary>
        public const string lastLuckyWheelSpin = "LastSpin";

        /// <summary>
        /// a string that denotes the  
        /// last time the user spinned the lucky-wheel from watching an advertisement
        /// </summary>
        public const string lastLuckyWheelAdvertisementSpin = "LastAdSpin";

        /// <summary>
        /// returns the amount of ad spins that can be made 
        /// </summary>
        public const string numberOfLeftAdSpins = "NumberOfAdSpinnsLeft";


        /// <summary>
        /// used for saving the used language
        /// </summary>
        public const string localizedLanguage = "localize";


        /// <summary>
        /// Value 0 = followfinger ship control
        /// Value 1 = touchpad
        /// </summary>
        public const string playerControlInfo = "ctrl";

        /// <summary>
        /// gives information if a teleport add should be shown at start of endless mode.
        /// 
        /// if true: player can watch add and teleport directly to the highest defeated boss
        /// </summary>
        public const string teleportAdSettings = "teleport";


        public const string masterVolume = "master";
        public const string musicVolume = "music";
        public const string sfxVolume = "sfx";

        /// <summary>
        /// saves the position (left/right) of the skill buttons, including character skills, ship weapon skill and power ups
        /// </summary>
        public const string skillPanelPosition = "SkillPanelPosition";

    }
}