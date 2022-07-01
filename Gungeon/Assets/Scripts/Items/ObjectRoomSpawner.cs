using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;
    [SerializeField] GameObject itemObject;
    public GridController grid;
    void Start() {

    }

    public void InitialiseObjectSpawning(){

        int enemiesNumber = Random.Range(1,6);
        int itemsNumber = Random.Range(0,2);
        SpawnObjects(enemyObject, enemiesNumber);
        SpawnObjects(itemObject, itemsNumber);

    }

    void SpawnObjects(GameObject gameObject, int items){
        
        for(int i = 0; i < items; i++){
            if (grid.availablePoints.Count == 0) {
                break;
            }
            int randomPos = Random.Range(0, grid.availablePoints.Count - 1);
            GameObject go = Instantiate(gameObject, grid.availablePoints[randomPos], Quaternion.identity, transform) as GameObject;
            grid.availablePoints.RemoveAt(randomPos);
        }
    }
}
