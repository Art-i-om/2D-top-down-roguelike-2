using System;
using System.Collections.Generic;
using System.Linq;
using Edgar.Unity;
using Edgar.Unity.Examples.CurrentRoomDetection;
using GraphSystem;
using UnityEngine;
using Room = GraphSystem.Room;

namespace PostProcessing
{
    public class RoomManager : MonoBehaviour
    {
        public GameObject[] EnemyPrefabs;

        public List<EnemyDefault> RemainingEnemies;

        public bool EnemiesSpawned;

        public Collider2D FloorCollider;

        private static System.Random Random => GameManager.Instance.Random;

        public RoomInstanceGrid2D RoomInstance;

        private Room room;
        
        public List<GameObject> Doors = new List<GameObject>();

        public bool Cleared;

        public bool Visited;
    
        public void Start()
        {
            RoomInstance = GetComponent<RoomInfoGrid2D>()?.RoomInstance;
            room = RoomInstance?.Room as Room;
        }
        
        public void OnRoomEnter(GameObject player)
        {
            Debug.Log($"Room enter. Room name: {RoomInstance.Room.GetDisplayName()}, Room template: {RoomInstance.RoomTemplatePrefab.name}");
            GameManager.Instance.OnRoomEnter(RoomInstance);

            if (!Visited && RoomInstance != null)
            {
                Visited = true;
                UnlockDoors();
            }
            
            if (ShouldSpawnEnemies())
            {
                // Close all neighboring doors
                CloseDoors();

                // Spawn enemies
                SpawnEnemies();
            }
        }
        
        public void OnRoomLeave(GameObject player)
        {
            Debug.Log($"Room leave {RoomInstance.Room.GetDisplayName()}");
            
            if (GameManager.Instance)
            {
                GameManager.Instance.OnRoomLeave(RoomInstance);
            }
        }

        private void SpawnEnemies()
        {
            EnemiesSpawned = true;

            var enemies = new List<EnemyDefault>();
            var totalEnemiesCount = Random.Next(4, 8);
            Debug.Log("totalEnemiesCount: " + totalEnemiesCount);

            while (enemies.Count < totalEnemiesCount)
            {
                var position = RandomPointInBounds(FloorCollider.bounds, 1f);

                if (!IsPointWithinCollider(FloorCollider, position))
                {
                    continue;
                }

                if (Physics2D.OverlapCircleAll(position, 0.5f).Any(x => !x.isTrigger))
                {
                    continue;
                }

                var enemyPrefab = EnemyPrefabs[Random.Next(0, EnemyPrefabs.Length)];

                var enemy = Instantiate(enemyPrefab, RoomInstance.RoomTemplateInstance.transform, true);
                enemy.transform.position = position;

                var Enemy = enemy.AddComponent<EnemyDefault>();
                Enemy.RoomManager = this;
                
                enemies.Add(Enemy);
            }

            RemainingEnemies = enemies;
        }

        private static bool IsPointWithinCollider(Collider2D collider, Vector2 point)
        {
            return collider.OverlapPoint(point);
        }

        private static Vector3 RandomPointInBounds(Bounds bounds, float margin = 0)
        {
            return new Vector3(
                RandomRange(bounds.min.x + margin, bounds.max.x - margin),
                RandomRange(bounds.min.y + margin, bounds.max.y - margin),
                RandomRange(bounds.min.z + margin, bounds.max.z - margin));
        }

        private static float RandomRange(float min, float max)
        {
            return (float)(Random.NextDouble() * (max - min) + min);
        }

        private void CloseDoors()
        {
            foreach (var door in Doors)
            {
                if (door.GetComponent<Door>().State == Door.DoorState.EnemyLocked)
                {
                    door.SetActive(true);
                }
            }
        }
        
        private void OpenDoors()
        {
            foreach (var door in Doors)
            {
                if (door.GetComponent<Door>().State == Door.DoorState.EnemyLocked)
                {
                    door.SetActive(false);
                }
            }
        }
        
        private void UnlockDoors()
        {
            if (room.type == RoomType.Reward || room.type == RoomType.Shop)
            {
                foreach (var door in Doors)
                {
                    if (door.GetComponent<Door>().State == Door.DoorState.Locked)
                    {
                        door.GetComponent<Door>().State = Door.DoorState.Unlocked;
                    }
                }
            }
        }

        private bool ShouldSpawnEnemies()
        {
            return Cleared == false && EnemiesSpawned == false && (room.type == RoomType.Normal || room.type == RoomType.Hub || room.type == RoomType.Boss);
        }
        
        public void OnEnemyKilled(EnemyDefault enemy)
        {
            Destroy(enemy.gameObject);
            RemainingEnemies.Remove(enemy);
            
            // Open doors if there are no enemies left in the room
            if (RemainingEnemies.Count == 0)
            {
                OpenDoors(); 
            }
        }
    }
}

