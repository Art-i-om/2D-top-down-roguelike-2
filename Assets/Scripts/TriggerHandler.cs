using System.Collections;
using System.Collections.Generic;
using Edgar.Unity.Examples.CurrentRoomDetection;
using UnityEngine;

namespace PostProcessing
{
    public class TriggerHandler : MonoBehaviour
    {
        private RoomManager roomManager;

        public void Start()
        {
            roomManager = transform.parent.parent.gameObject.GetComponent<RoomManager>();
        }

        public void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.CompareTag("Player"))
            {
                roomManager?.OnRoomEnter(otherCollider.gameObject);
            }
        }

        public void OnTriggerExit2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.CompareTag("Player"))
            {
                roomManager?.OnRoomLeave(otherCollider.gameObject);
            }
        }
    }
}

