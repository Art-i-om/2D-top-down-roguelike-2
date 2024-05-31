using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Edgar.Unity;
using Edgar.Unity.Examples;
using UnityEngine;
using Random = System.Random;

namespace PostProcessing
{
    public class GameManager : GameManagerBase<GameManager>
    {
        private RoomInstanceGrid2D currentRoom;
    
        private RoomInstanceGrid2D nextCurrentRoom;

        public Random Random;

        public override void LoadNextLevel()
        {
            
        }
        
        private IEnumerator GeneratorCoroutine(DungeonGeneratorGrid2D generator)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            yield return null;

            generator.Generate();

            yield return null;

            stopwatch.Stop();

            SetLevelInfo($"Generated in {stopwatch.ElapsedMilliseconds / 1000d:F}s");
            HideLoadingScreen();
        }
        
        public void OnRoomEnter(RoomInstanceGrid2D roomInstance)
        {
            nextCurrentRoom = roomInstance;

            if (currentRoom == null)
            {
                currentRoom = nextCurrentRoom;
                nextCurrentRoom = null;
                // UpdateCurrentRoomInfo();
            }
        }
        
        public void OnRoomLeave(RoomInstanceGrid2D roomInstance)
        {
            currentRoom = nextCurrentRoom;
            nextCurrentRoom = null;
            // UpdateCurrentRoomInfo();
        }
        
        // private void UpdateCurrentRoomInfo()
        // {
        //     var canvas = GetCanvas();
        //     
        //     if (!canvas)
        //     {
        //         return;
        //     }
        //     
        //     var currentRoomInfo = canvas.transform.Find("CurrentRoomInfo").GetComponent<Text>();
        //     currentRoomInfo.text = $"Room name: {currentRoom?.Room.GetDisplayName()}, Room template: {currentRoom?.RoomTemplatePrefab.name}";
        // }
    }
}

