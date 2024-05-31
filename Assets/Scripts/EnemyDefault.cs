using PostProcessing;
using UnityEngine;

namespace PostProcessing
{
    public class EnemyDefault : MonoBehaviour
    {
        private Transform player;

        public RoomManager RoomManager;
    
        void Start()
        {
            player = FindObjectOfType<Player>().transform;
            RoomManager = transform.parent.gameObject.GetComponent<RoomManager>();
        }

        public void DamageEnemy()
        {
            RoomManager.OnEnemyKilled(this);
        }
    }
}



