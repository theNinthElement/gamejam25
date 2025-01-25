using UnityEngine;

namespace Collectibles_Obstacles
{
    public class CollectibleMood : ACollectible
    {
        [Tooltip("Number added to the mood")]
        [SerializeField][Range(-1, 1)] float moodBonus = 0f;

        protected override void Collect()
        {
            GameStats.instance.ChangeMood(moodBonus);
        }
    }
}
