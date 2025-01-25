using UnityEngine;
using Random = UnityEngine.Random;

namespace Generic
{
    public class ActivateRandomGameObject : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gameObjects;
        
        public void ActivateRandom()
        {
            if (_gameObjects.Length == 0) return;
            var index = Random.Range(0, _gameObjects.Length);
            foreach (var gO in _gameObjects)
            {
                gO.SetActive(false);
            }
            _gameObjects[index].SetActive(true);
        }
    }
}