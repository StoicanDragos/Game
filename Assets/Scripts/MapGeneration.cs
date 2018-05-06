﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

    Vector2 worldSize = new Vector2(4, 4);

    Room[,] rooms;

    List<Vector2> takenPositions = new List<Vector2>();

    int gridSizeX, gridSizeY, numberOfRooms;

    private GameObject roomObj;

    int roomType;


    void Start ()
    {
        numberOfRooms = Random.Range(15, 20);
        if (numberOfRooms >= (worldSize.x*2) * (worldSize.y *2))
        {
            numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }
        gridSizeX = Mathf.RoundToInt(worldSize.x);
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        CreateRooms();
        SetRoomDoors();
        DrawMap();
    }

    void CreateRooms ()
    {
        ///setup
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, 0);
        takenPositions.Insert(0, Vector2.zero);
        Vector2 checkPos = Vector2.zero;


        ///For influencing numbers
        float randomCompare = -.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;

        ///add rooms
        for (int i=0;i<numberOfRooms-1;i++)
        {
            float randomPerc = ((float)i) / (((float)numberOfRooms - 1));
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
            /// search for new pos
            checkPos = NewPosition();
            if(NumberOfNeighbors(checkPos,takenPositions) > 1 && Random.value > randomCompare)
            {
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                } while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
                if (iterations >= 100)
                    print("error: could not create with fewer neighbors than: " + NumberOfNeighbors(checkPos, takenPositions));
            }
            roomType = Random.Range(1, 4);
            rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, roomType);
            takenPositions.Insert(takenPositions.Count, checkPos);
        }
        

    }

    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool upDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            ///Go up/down
            if (upDown)
            {
                ///up
                if (positive)
                    y += 1;
                ///down
                else
                    y -= 1;
            }
            /// Go left/right
            else
            {
                ///left
                if (positive)
                    x += 1;
                ///right
                else
                    x -= 1;
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        return checkingPos;
    }

    Vector2 SelectiveNewPosition()
    {
        int index = 0, inc = 0;
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            inc = 0;
            do
            {
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                inc++;
            } while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);
            
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool upDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            ///Go up/down
            if (upDown)
            {
                ///down
                if (positive)
                    y += 1;
                ///up
                else
                    y -= 1;
            }
            /// Go left/right
            else
            {
                ///right
                if (positive)
                    x += 1;
                ///left
                else
                    x -= 1;
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        if (inc >= 100)
            print("error: could not create with only one heighbor");
        return checkingPos;
    }

    
    int NumberOfNeighbors (Vector2 checkingPos, List<Vector2> usedPositions)
    {
        int ret = 0;
        if (usedPositions.Contains(checkingPos + Vector2.right))
            ret++;
        if (usedPositions.Contains(checkingPos + Vector2.left))
            ret++;
        if (usedPositions.Contains(checkingPos + Vector2.up))
            ret++;
        if (usedPositions.Contains(checkingPos + Vector2.down))
            ret++;
        return ret;

    }


    void SetRoomDoors()
    {
        for (int x = 0; x < ((gridSizeX * 2)); x++)
            for (int y = 0; y < ((gridSizeY * 2)); y++)
            {
                if (rooms[x,y] == null)
                {
                    continue;
                }
                if (y - 1 < 0)
                    rooms[x, y].doorTop = false;
                else
                    rooms[x, y].doorTop = (rooms[x, y - 1] != null);

                if (y + 1 >= gridSizeY * 2)
                    rooms[x, y].doorBot = false;
                else
                    rooms[x, y].doorBot = (rooms[x, y + 1] != null);
                if (x - 1 < 0)
                    rooms[x, y].doorLeft = false;
                else
                    rooms[x, y].doorLeft = (rooms[x -1, y] != null);
                if (x + 1 >= gridSizeX * 2)
                    rooms[x, y].doorRight = false;
                else
                    rooms[x, y].doorRight = (rooms[x + 1, y] != null);

            }
    }

    void DrawMap()
    {
        foreach(Room room in rooms)
        {
            if (room == null)
                continue;
            Vector2 drawPos = room.gridPos;
            drawPos.x *= 8;
            drawPos.y *= 4;
            GameObject roomPrefab = Resources.Load(@"RoomPrefabs\Room" + room.type) as GameObject;
            Instantiate(roomPrefab, drawPos, Quaternion.identity);
            
        }
    }
}
