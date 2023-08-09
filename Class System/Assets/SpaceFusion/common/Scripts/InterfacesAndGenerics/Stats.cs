using UnityEngine;

namespace SpaceFusion.common.Scripts.InterfacesAndGenerics {
    public class Stats
    {
        public bool isActivated;
        public bool isUnlocked;
    
    
        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}