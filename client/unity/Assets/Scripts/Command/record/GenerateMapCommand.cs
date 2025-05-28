using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using BattleCity;
using UnityEngine.UIElements;
using Newtonsoft.Json.Linq;

public class GenerateMapCommand : AbstractCommand
{
    private static readonly string[] floorPrefabNames =
        {
            "Floor00"
           /* "Floor01",
            "Floor02",
            "Floor03",
            "Floor04",
            "Floor05",
            "Floor06",
            "Floor07",
            "Floor08"*/
        };
    private Map map;
    public JObject mapData;

    public GenerateMapCommand(JObject MapData, Map map)
    {
        mapData = MapData;
    }
    

    protected override void OnExecute()
    {
        map = this.GetModel<Map>();
        Initial();
        GenerateFloor();
        foreach (Wall wall in map.CityWall)
        {
            wall.CreateWallObject();
        }
    }

    private void Initial()
    {
        if (mapData == null || mapData.Count == 0)
        {
            Debug.LogError("MapData is null or empty!");
            return;
        }

        JArray wallsArray = (JArray)mapData["walls"];
        if (wallsArray == null)
        {
            Debug.LogWarning("No walls data found in the record!");
        }

        foreach (var wall in wallsArray)
        {
            // ��ȡ wall ����������
            float x = wall["x"]?.Value<float>() ?? 0f;
            float y = wall["y"]?.Value<float>() ?? 0f;
            float angle = wall["angle"]?.Value<float>() ?? 0f;

            // Debug.Log($"Wall Position: x={x}, y={y}, angle={angle}");

            // ���� Position ������ӵ� cityMap
            Position position = new Position(x, y, angle);
            map.AddWall(position);
        }

        int? mapSize = (int?)mapData["mapSize"];
        map.setSize(mapSize);
    }

    private void GenerateFloor()
    {
        GameObject wallController = GameObject.Find("WallController");
        for (int i = 0; i < map.MapSize; i++)
        {
            for (int j = 0; j < map.MapSize; j++)
            {
                Vector3 position = new Vector3((float)i * Constants.FLOOR_LEN + Constants.POS_BIAS, (float)(Constants.YPOS + Constants.Y_BIAS), (float)j * Constants.FLOOR_LEN + Constants.POS_BIAS);
                int randomIndex = Random.Range(0, floorPrefabNames.Length);
                // �����������ض�Ӧ��Ԥ����
                string prefabPath = "Prefabs/Floor/" + floorPrefabNames[randomIndex];
                GameObject floor = Resources.Load<GameObject>(prefabPath);

                if (floor != null)
                {
                    GameObject floorObject = Object.Instantiate(floor, wallController.transform);
                    // ���ñ����������ת
                    floorObject.transform.localPosition = position;
                    floorObject.transform.localRotation = Quaternion.identity;
                    // ��������
                    floorObject.transform.localScale *= Constants.ZOOM;

                    map.CityFloors.Add(floorObject);
                }
                else
                {
                    Debug.LogError("Failed to load prefab: " + prefabPath);
                }
                
            }
        }
    }
}
