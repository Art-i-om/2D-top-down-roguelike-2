using System;
using UnityEngine;

namespace GraphSystem
{
    [Serializable]
    public class RoomTemplatesConfig
    {
        public GameObject[] NormalRoomTemplates;
    
        public GameObject[] ShopRoomTemplates;
    
        public GameObject[] EntranceRoomTemplates;
    
        public GameObject[] HubRoomTemplates;
    
        public GameObject[] BossRoomTemplates;
        
        public GameObject[] RewardRoomTemplates;

        public GameObject[] CorridorRoomTemplates;

        public GameObject[] GetRoomTemplates(Room room)
        {
            switch (room.type)
            {
                case RoomType.Entrance:
                    return EntranceRoomTemplates;
                
                case RoomType.Shop:
                    return ShopRoomTemplates;
                
                case RoomType.Hub:
                    return HubRoomTemplates;
                
                case RoomType.Boss:
                    return BossRoomTemplates;
                
                case RoomType.Reward:
                    return RewardRoomTemplates;

                case RoomType.Undefined:
                    return NormalRoomTemplates;
                
                case RoomType.BossFoyers:
                    return NormalRoomTemplates;
                
                case RoomType.Exit:
                    return NormalRoomTemplates;
                
                case RoomType.Corridor:
                    return NormalRoomTemplates;
                
                case RoomType.Connector:
                    return NormalRoomTemplates;
                    
                case RoomType.Secret:
                    return NormalRoomTemplates;
                
                default:
                    return NormalRoomTemplates;
            }
        }

    }

}
