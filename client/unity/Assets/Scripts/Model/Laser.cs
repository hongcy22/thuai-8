using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleCity
{
    public class Laser : ICanGetModel
    {
        public RecordInfo _recordInfo;
        public int currentTick;
        public GameObject LaserObject;

        public Vector3 startPos { get; set; }
        public Vector3 endPos { get; set; }

        public Laser(Position start, Position end)
        {
            _recordInfo = this.GetModel<RecordInfo>();
            currentTick = _recordInfo.BattleTick;
            GameObject wallController = GameObject.Find("WallController");

            // ���ز�ʵ��������Ԥ����
            GameObject LaserPrefab = Resources.Load<GameObject>("Effects/Laser");
            startPos = new Vector3(
                (float)(start.X + Constants.GENERAL_XBIAS),
                (float)(start.Y + Constants.Y_BIAS + 0.05f),
                (float)(start.Z + Constants.GENERAL_ZBIAS)
            );
            endPos = new Vector3(
                (float)(end.X + Constants.GENERAL_XBIAS),
                (float)(end.Y + Constants.Y_BIAS + 0.05f),
                (float)(end.Z + Constants.GENERAL_ZBIAS)
            );

            LaserObject = Object.Instantiate(LaserPrefab, wallController.transform);

            // ���ü������
            // ����������ת��Ϊ������ı�������
            var hovlLaser = LaserObject.GetComponent<Hovl_Laser>();
            if (hovlLaser != null)
            {
                hovlLaser.CustomStartPosition = startPos;
                hovlLaser.CustomEndPosition = endPos;
            }
            else
            {
                Debug.LogError("Hovl_Laser component missing on LaserPrefab!");
            }

            // ��Ӽ����������ڿ�����
            LaserObject.AddComponent<LaserLifecycleController>().Init(this);

            // ע��ս�������¼�
            TypeEventSystem.Global.Register<BattleEndEvent>(e => DestroyLaser());
        }

        public void DestroyLaser()
        {
            if (LaserObject != null)
            {
                Object.Destroy(LaserObject);
                LaserObject = null;
            }
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }
    }

    // �����������ڿ������
    public class LaserLifecycleController : MonoBehaviour
    {
        private Laser mOwner;

        public void Init(Laser owner)
        {
            mOwner = owner;
            StartCoroutine(CheckExpiration());
        }

        private IEnumerator CheckExpiration()
        {
            // ÿ֡����Ƿ�ʱ
            while (mOwner._recordInfo.BattleTick <= mOwner.currentTick + 8)
            {
                yield return null;
            }
            mOwner.DestroyLaser();
        }
    }
}