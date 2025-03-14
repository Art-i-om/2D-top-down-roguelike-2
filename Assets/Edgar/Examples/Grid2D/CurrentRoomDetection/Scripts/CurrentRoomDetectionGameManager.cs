﻿using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace Edgar.Unity.Examples.CurrentRoomDetection
{
    public class CurrentRoomDetectionGameManager : GameManagerBase<CurrentRoomDetectionGameManager>
    {
        // Current active room
        private RoomInstanceGrid2D currentRoom;

        // The room that will be active after the player leaves the current room
        private RoomInstanceGrid2D nextCurrentRoom;
        

        /// <summary>
        /// Coroutine that generates the level.
        /// We need to yield return before the generator starts because we want to show the loading screen
        /// and it cannot happen in the same frame.
        /// It is also sometimes useful to yield return before we hide the loading screen to make sure that
        /// all the scripts that were possibly created during the process are properly initialized.
        /// </summary>
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
                UpdateCurrentRoomInfo();
            }
        }

        public void OnRoomLeave(RoomInstanceGrid2D roomInstance)
        {
            currentRoom = nextCurrentRoom;
            nextCurrentRoom = null;
            UpdateCurrentRoomInfo();
        }

        private void UpdateCurrentRoomInfo()
        {
            var canvas = GetCanvas();
            
            if (!canvas)
            {
                return;
            }
            
            var currentRoomInfo = canvas.transform.Find("CurrentRoomInfo").GetComponent<Text>();
            currentRoomInfo.text = $"Room name: {currentRoom?.Room.GetDisplayName()}, Room template: {currentRoom?.RoomTemplatePrefab.name}";
        }

        public override void LoadNextLevel()
        {
            throw new System.NotImplementedException();
        }
    }
}