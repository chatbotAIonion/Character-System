using UnityEngine;

namespace SpaceFusion.common.Scripts {
    public class GlobalStats : MonoBehaviour {

        private StatIncreasement stats;

        public static StatIncreasement GetBonusStatsInPercent() {
            var stats = new StatIncreasement(0);
            // var character = CharacterList.instance.GetActive();
            // if (character != null) {
            //     stats.Combine(character.GetStatIncreasement());
            // }

            return stats;
        }

    }

    /// <summary>
    /// % multiplicator for every available stats
    /// 1 = no multiplicator
    /// 1,5 = additional 50% on standard stats!
    /// </summary>
    public class StatIncreasement {

        // values increasement 0 %
        public float damage = 0f;
        public float hp = 0f;
        public float speed = 0f;
        public float fireRate = 0f;
        public float criticalProbability = 0f;
        public float criticalDamage = 0f;

        public float coins = 0f;
        public float darkMatter = 0f;
        public float exp = 0f;

        public StatIncreasement(float defaultValInPercent) {
            damage = defaultValInPercent;
            hp = defaultValInPercent;
            speed = defaultValInPercent;
            fireRate = defaultValInPercent;
            coins = defaultValInPercent;
            darkMatter = defaultValInPercent;
            criticalProbability = defaultValInPercent;
            criticalDamage = defaultValInPercent;
            exp = defaultValInPercent;
        }

        public void addIncreasement(AvailableStats stat, float increasementInPercent) {
            switch (stat) {
                case AvailableStats.Damage:
                    damage += increasementInPercent;
                    return;
                case AvailableStats.Speed:
                    speed += increasementInPercent;
                    return;
                case AvailableStats.Health:
                    hp += increasementInPercent;
                    return;
                case AvailableStats.FireRate:
                    fireRate += increasementInPercent;
                    return;
                case AvailableStats.criticalDamage:
                    criticalDamage += increasementInPercent;
                    return;
                case AvailableStats.criticalProbability:
                    criticalProbability += increasementInPercent;
                    return;
                case AvailableStats.Coins:
                    coins += increasementInPercent;
                    return;
                case AvailableStats.DarkMatter:
                    darkMatter += increasementInPercent;
                    return;
                case AvailableStats.Experience:
                    exp += increasementInPercent;
                    return;
                default: return;
            }
        }

        public void Combine(StatIncreasement values) {
            damage += values.damage;
            hp += values.hp;
            speed += values.speed;
            fireRate += values.fireRate;
            coins += values.coins;
            darkMatter += values.darkMatter;
            criticalProbability += values.criticalProbability;
            criticalDamage += values.criticalDamage;
            exp += values.exp;
        }

        public StatIncreasement Normalize() {
            var tmp = new StatIncreasement(0);
            tmp.damage = damage / 100;
            tmp.hp = hp / 100;
            tmp.speed = speed / 100;
            tmp.fireRate = fireRate / 100;
            tmp.coins = coins / 100;
            tmp.darkMatter = darkMatter / 100;
            tmp.criticalProbability = criticalProbability / 100;
            tmp.criticalDamage = criticalDamage / 100;
            tmp.exp += exp / 100;
            return tmp;
        }

        public override string ToString() {
            return "damage: " + damage + "\n" +
                   "hp: " + hp + "\n" +
                   "speed: " + speed + "\n" +
                   "fireRate: " + fireRate + "\n" +
                   "coins: " + coins + "\n" +
                   "darkMatter: " + darkMatter + "\n" +
                   "criticalProbability: " + criticalProbability + "\n" +
                   "criticalDamage: " + criticalDamage + "\n" +
                   "exp: " + exp + "\n";
        }

    }

    public enum AvailableStats {

        Nothing,
        Damage,
        Health,
        Speed,
        FireRate,
        Coins,
        DarkMatter,
        criticalProbability, // probability that a critical hit is done
        criticalDamage, // multiplier for critical damage
        Experience,

    }
}