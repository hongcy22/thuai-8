using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace BattleCity
{
    public class Map : AbstractModel
    {
        public int MapSize { get; set; }
        public List<Wall> CityWall { get; set; }
        public List<Wall> CityFence { get; set; }

        public List<Trap> Traps { get; set; }
        public List<GameObject> CityFloors { get; set; }
        protected override void OnInit()
        {
            CityWall = new List<Wall>();
            CityFence = new List<Wall>();
            CityFloors = new List<GameObject>();
            Traps = new List<Trap>();
        }

        public void setSize(int? mapSize)
        {
            if (mapSize != null)
            {
                MapSize = (int)mapSize;
            }
            else
            {
                MapSize = Constants.MAP_SIZE;
            }
        }
        
        //���ڳ�ʼ������ͼʱ����wall
        public void AddWall(Position wallPos)
        {
            Wall wall = new(wallPos);
            CityWall.Add(wall);
        }

        public void AddWall(double x, double y,double angle) 
        {
            Position position = new(x, y, angle);
            AddWall(position);
        }

        //���ں�������wall
        public void UpdateWall(Position wallPos)
        {
            Wall wall = new(wallPos);
            CityWall.Add(wall);
            wall.CreateWallObject();
        }
        public void UpdateWall(double x, double y, double angle)
        {
            Position position = new(x, y, angle);
            UpdateWall(position);
        }
        //���ں�������fench
        public void UpdateFence(Position wallPos)
        {
            Wall wall = new(wallPos);
            CityFence.Add(wall);
            wall.CreateFenceObject();
        }
        public void UpdateFence(double x, double y, double angle)
        {
            Position position = new(x, y, angle);
            UpdateFence(position);
        }

        //�����Ƴ�ǽ��
        public void RemoveWall(Position wallPos) 
        {
            Wall wall = new(wallPos);
            Wall foundWall = CityWall.Find(w => w.wallPos.Equals(wall.wallPos));
            if (foundWall != null)
            {
                foundWall.RemoveWall(); // ���� RemoveWall ������ Wall ����
                CityWall.Remove(foundWall); // �� CityWall ���Ƴ��ҵ���ǽ
            }
            else
            {
                Debug.LogError("The wall is not found!");
            }
        }
        public void RemoveWall(double x, double y,double angle) 
        {
            Position position = new(x, y, angle);
            RemoveWall(position);
        }
        public void RemoveWall(Wall wall)
        {
            RemoveWall(wall.wallPos);
        }

        //�����Ƴ�fence
        public void RemoveFence(Position wallPos)
        {
            Wall wall = new(wallPos);
            CityFence.Remove(wall);
            Wall foundFence = CityWall.Find(w => w.wallPos.Equals(wall.wallPos));
            if (foundFence != null)
            {
                foundFence.RemoveWall(); 
                CityFence.Remove(foundFence);
            }
            else
            {
                Debug.LogError("The fence is not found!");
            }
        }
        public void RemoveFence(double x, double y, double angle)
        {
            Position position = new(x, y, angle);
            RemoveFence(position);
        }

        public void RemoveFence(Wall wall)
        {
            RemoveFence(wall.wallPos);
        }

        public void AddTrap(Position trapPos, bool isActive = false)
        {
            Trap trap = new(trapPos,isActive);
            Traps.Add(trap);
        }

        public void UpdateTrap(Trap trap, bool isActive)
        {
            if (trap == null)
            {
                trap.isActive = isActive;
            }
        }
        public void RemoveTrap(Position position)
        {
            Trap trap = Traps.Find(w => w.trapPos == position);
            if (trap != null)
            {
                Traps.Remove(trap);
                trap.RemoveTrap();
            }
            else
            {
                Debug.LogError("The Trap is not found!");
            }
        }

        public void RemoveTrap(Trap trap)
        {
            RemoveTrap(trap.trapPos);
        }

    }
}
