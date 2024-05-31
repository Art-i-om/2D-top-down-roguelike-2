using UnityEngine;
using Edgar.Unity;
using Room = GraphSystem.Room;
using RoomType = GraphSystem.RoomType;
using System;
using System.Linq;
using GraphSystem;
using UnityEngine.Tilemaps;

namespace PostProcessing
{
    [CreateAssetMenu(menuName = "MyMenus/PostProcessing", fileName = "PostProcessing")]
    
    public class PostProcessingTask : DungeonGeneratorPostProcessingGrid2D
    {
        public GameObject[] Enemies;
        
        private void SetSpawnPosition(DungeonGeneratorLevelGrid2D level)
        {
            var entranceRoomInstance = level
                .RoomInstances
                .FirstOrDefault(x => ((Room) x.Room).type == RoomType.Entrance);

            if (entranceRoomInstance == null)
            {
                throw new InvalidOperationException("Could not find Entrance room");
            }

            var roomTemplateInstance = entranceRoomInstance.RoomTemplateInstance;

            var spawnPosition = roomTemplateInstance.transform.Find("SpawnPosition");

            var player = GameObject.FindWithTag("Player");
            player.transform.position = spawnPosition.position;
        }

        private void AddFloorCollider(GameObject floor)
        {
            var tilemapCollider2D = floor.AddComponent<TilemapCollider2D>();
            tilemapCollider2D.usedByComposite = true;

            var compositeCollider2d = floor.AddComponent<CompositeCollider2D>();
            compositeCollider2d.geometryType = CompositeCollider2D.GeometryType.Polygons;
            compositeCollider2d.isTrigger = true;
            compositeCollider2d.generationType = CompositeCollider2D.GenerationType.Manual;

            floor.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        
        public override void Run(DungeonGeneratorLevelGrid2D level)
        {
            SetSpawnPosition(level);
            
            if (GameManager.Instance != null)
            {
                // Set the Random instance of the GameManager to be the same instance as we use in the generator
                GameManager.Instance.Random = Random;
            }

            foreach (var roomInstance in level.RoomInstances)
            {
                var roomTemplateInstance = roomInstance.RoomTemplateInstance;
                var room = (Room)roomInstance.Room;

                // Find floor tilemap layer
                var tilemaps = RoomTemplateUtilsGrid2D.GetTilemaps(roomTemplateInstance);
                var floor = tilemaps.Single(x => x.name == "Floor").gameObject;

                // Add floor collider
                AddFloorCollider(floor);
                
                floor.AddComponent<TriggerHandler>();
                var roomManager = roomInstance.RoomTemplateInstance.AddComponent<RoomManager>();

                if (room.type != RoomType.Corridor)
                {
                    roomManager.EnemyPrefabs = Enemies;
                    roomManager.FloorCollider = floor.GetComponent<CompositeCollider2D>();

                    foreach (var door in roomInstance.Doors)
                    {
                        var corridorRoom = door.ConnectedRoomInstance;
                        var corridorGameObject = corridorRoom.RoomTemplateInstance;
                        var doorsGameObject = corridorGameObject.transform.Find("Door")?.gameObject;
                        var connection = (Connections)corridorRoom.Connection;

                        if (doorsGameObject != null)
                        {
                            if (connection.IsLocked)
                            {
                                doorsGameObject.GetComponent<Door>().State = Door.DoorState.Locked;
                            }
                            else
                            {
                                doorsGameObject.GetComponent<Door>().State = Door.DoorState.EnemyLocked;
                                doorsGameObject.SetActive(false);
                            }

                            roomManager.Doors.Add(doorsGameObject);
                        }
                    }
                }
            }
        }
    }
}

