using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    private List<Vector2Int> dungeonRooms;
    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();
    private static readonly List<Vector2Int> directions = new List<Vector2Int>{
        Vector2Int.up,
        Vector2Int.left,
        Vector2Int.down,
        Vector2Int.right
    };
    private int cont = 0;
    
    private void Start(){
        dungeonRooms = GenerateDungeon();
        SpawnRooms(dungeonRooms);
    }

    public static List<Vector2Int> GenerateDungeon(){
        if (GameController.CurrentState == CreationStates.BySeed)
        {
            Random.InitState(GameController.Seed);
        }
        int rooms = Random.Range(10,20);
        positionsVisited = PositionsSelector(rooms);
        return positionsVisited;
    }

    private static List<Vector2Int> PositionsSelector(int rooms)
    {
        List<Vector2Int> usedPositions = new List<Vector2Int>();
        List<Vector2Int> selectablePositions = new List<Vector2Int>{
            Vector2Int.zero
        };
        List<Vector2Int> allCheckedPositions = new List<Vector2Int>{
            Vector2Int.zero
        };

        for(int i = 0; i < rooms+1; i++){
            int randomLimit = selectablePositions.Count;
            int selectedPositionIndex = Random.Range(0,randomLimit);
            Vector2Int selectedPosition = selectablePositions[selectedPositionIndex];
            selectablePositions.RemoveAt(selectedPositionIndex);
            usedPositions.Add(selectedPosition);
            List<Vector2Int> adyacentPositions = GetAdyacentPositions(selectedPosition);
            List<Vector2Int> notCheckedAdyacentPositions = ListDifference(allCheckedPositions, adyacentPositions);
            allCheckedPositions.AddRange(notCheckedAdyacentPositions);
            selectablePositions.AddRange(notCheckedAdyacentPositions);
            
        }

        return  usedPositions;
    }

    private static List<Vector2Int> GetAdyacentPositions(Vector2Int selectedPosition)
    {
        List<Vector2Int> adyacentPositions = new List<Vector2Int>();
        foreach(Vector2Int direction in directions)
        {
            Vector2Int adyacentPosition = selectedPosition + direction;
            adyacentPositions.Add(adyacentPosition);
        }
        return adyacentPositions;
    }

    private static List<Vector2Int> ListDifference(List<Vector2Int> listToUpdate, List<Vector2Int> newData)
    {
        List<Vector2Int> listDifference = new List<Vector2Int>();
        foreach(Vector2Int data in newData)
        {
            if (!listToUpdate.Contains(data)) {
                listDifference.Add(data);
            }
        }
        return listDifference;
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms){
        RoomController.instance.LoadRoom("Start", 0, 0);
        foreach(Vector2Int roomLocation in rooms){
            cont = cont + 1;
            RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoomName(cont%2), roomLocation.x, roomLocation.y);
        }
    }

}
