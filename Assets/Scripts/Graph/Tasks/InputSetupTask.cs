using System.Linq;
using Edgar.Unity;
using UnityEngine;

namespace GraphSystem
{
    [CreateAssetMenu(menuName = "MyMenus/Input setup", fileName = "Input Setup")]
    public class InputSetupTask : DungeonGeneratorInputBaseGrid2D
    {
        public LevelGraph LevelGraph;

        public RoomTemplatesConfig RoomTemplates;
    
        protected override LevelDescriptionGrid2D GetLevelDescription()
        {
            var levelDescription = new LevelDescriptionGrid2D();

            foreach (var room in LevelGraph.Rooms.Cast<Room>())
            {
                levelDescription.AddRoom(room, RoomTemplates.GetRoomTemplates(room).ToList());
            }

            foreach (var connection in LevelGraph.Connections.Cast<Connections>())
            {
                var corridorRoom = ScriptableObject.CreateInstance<Room>();
                corridorRoom.type = RoomType.Corridor;
                levelDescription.AddCorridorConnection(connection, corridorRoom, RoomTemplates.CorridorRoomTemplates.ToList());
            }

            return levelDescription;
        }
    }
}

