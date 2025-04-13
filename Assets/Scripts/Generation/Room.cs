using System;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public bool IsCleared
    {
        get { return roomEntities.Count == 0; }
    }
    public Room(int depth, Room parentRoom, bool bossRoom = false)
    {
        roomEntities = new List<EC_Entity>();
        this.depth = depth;
        this.parentRoom = parentRoom;
        children = new List<Room>();
        type = bossRoom ? RoomType.boss : RoomType.normal;
        entered = false;
    }

    public List<EC_Entity> roomEntities;
    public Room parentRoom;
    public List<Room> children;
    public RoomType type;
    
    // Variables
    public int depth;
    bool entered;

    public bool Clear { get { return CheckClear(); } }
    /// <summary>
    /// Returns true if no hostile entities exist in room, returns false if any hostile entities exist in room
    /// </summary>
    bool CheckClear()
    {
        // Check for hostiles
        for (int i = 0; i < roomEntities.Count; i++)
        {
            if (roomEntities[i].GetComponent<EC_Damage>())
                return false;
        }

        // Spawn portal?
        if (type == RoomType.boss)
        {
            DungeonManager.instance.SpawnPortal();
        }

        return true;
    }

    public enum RoomType
    {
        normal,
        boss
    }

    /// <summary>
    /// Handles room enter logic
    /// </summary>
    public void EnterRoom()
    {
        for (int i = 0; i < roomEntities.Count; i++)
        {
            roomEntities[i].IsEnabled(true);
        }

        if (!entered)
        {
            entered = true;

            ArtifactManager.instance.TriggerEnterRoom();
            if (type == RoomType.boss)
                ArtifactManager.instance.TriggerEnterBossRoom();
        }
    }

    /// <summary>
    /// Handles room exit logic
    /// </summary>
    public void ExitRoom()
    {
        for (int i = 0; i < roomEntities.Count; i++)
        {
            roomEntities[i].IsEnabled(false);
        }
    }

    /// <summary>
    /// Lock or unlock all doors in room
    /// </summary>
    public void SetAllDoors(bool _locked)
    {
        // Get all doors in room
        List<EC_Door> _doors = new List<EC_Door>();
        for (int i = 0; i < roomEntities.Count;i++)
        {
            if (roomEntities[i].GetComponent<EC_Door>())
                _doors.Add(roomEntities[i].GetComponent<EC_Door>());
        }
        // Set all doors
        for (int i = 0; i < _doors.Count;i++)
            _doors[i].SetLocked(_locked);
    }

    /// <summary>
    /// Returns a list of EC_Damage (hostile entities) in room
    /// </summary>
    public List<EC_Damage> GetHostiles()
    {
        List<EC_Damage> _hostiles = new List<EC_Damage>();
        for (int i = 0; i < roomEntities.Count; i++)
        {
            if (roomEntities[i].GetComponent<EC_Damage>())
                _hostiles.Add(roomEntities[i].GetComponent<EC_Damage>());
        }
        return _hostiles;
    }

    internal Vector3 GetSpawnPoint()
    {
        throw new NotImplementedException();
    }
}
