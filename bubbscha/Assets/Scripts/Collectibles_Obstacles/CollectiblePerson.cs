using UnityEngine;

namespace Collectibles_Obstacles
{
    public class CollectiblePerson : ACollectible
    {
        [Tooltip("Number of people picked up")]
        [SerializeField] int people=1;
        
        protected override void Collect()
        {
            GameStats.instance.ChangePeople(people);
        }
    }
}
