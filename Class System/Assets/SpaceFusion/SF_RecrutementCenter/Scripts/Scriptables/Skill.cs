using UnityEngine;

namespace SpaceFusion.SF_RecrutementCenter.Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "newSkill", menuName = "Characters/Skill")]

    public class Skill : ScriptableObject
    {

        public Sprite skillIcon;
        public GameObject skill;

        public string description;
        [Range(30,180)]
        public int cooldown;

        [Header("Dont Spawn on player Position")]
        public bool ignorePlayerPos;
    }
}
