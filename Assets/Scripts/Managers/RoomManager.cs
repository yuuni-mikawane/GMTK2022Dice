using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;

public class RoomManager : SingletonBind<RoomManager>
{
    public List<Room> rooms;
    public Room currentRoom;
}
