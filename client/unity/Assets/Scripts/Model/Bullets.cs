using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace BattleCity
{
    public class Bullets : AbstractModel
    {
        private List<BulletModel> BulletsList { get;set;}
        private List<int> BulletsId { get;set;}

        protected override void OnInit()
        {
            BulletsList = new();
            BulletsId = new List<int>();
        }

        public BulletModel GetBullet(int id)
        {
            BulletModel bullet = BulletsList.Find(Bullet => Bullet.Id == id);
            if (bullet!=null)
            {
                return bullet;
            }
            return null;           
        }

        public List<int> GetBulletIds()
        {
            return BulletsId;
        }

        public bool AddBulletModel(BulletModel bullet)
        {
            if (bullet == null || BulletsList.Exists(b => b.Id == bullet.Id))
            {
                // ����ӵ�ģ��Ϊnull��ID�Ѵ��ڣ��򷵻�false
                return false;
            }

            BulletsList.Add(bullet);
            BulletsId.Add(bullet.Id);
            return true; // �ɹ����
        }

        public bool DelBulletModel(int id)
        {
            BulletModel bullet = GetBullet(id);
            if (bullet != null)
            {
                BulletsList.Remove(bullet);
                BulletsId.Remove(bullet.Id);
                return true; // �ɹ�ɾ��
            }

            return false; // û���ҵ���ID���ӵ�ģ��
        }

    }
}

