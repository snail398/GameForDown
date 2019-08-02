using UnityEngine;

namespace Assets.Scripts.Coins
{
    public class CoinCountIncreacer : MonoBehaviour
    {
        [SerializeField] private IntVariable playerCoinCount;
        
        void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag.Equals("Player"))
            {
                playerCoinCount.Value++;
                gameObject.GetComponent<PoolObject>().ReturnToPool();
            }
        }
    }
}
