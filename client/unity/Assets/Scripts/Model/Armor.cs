using System;
using UnityEngine;


namespace BattleCity
{
    public class Armor
    {
        public enum KNIFE
        {
            NOT_OWNED,
            AVAILABLE,
            ACTIVE,
            BROKEN
        }
        public bool CanReflect { get; set; }
        public int ArmorValue { get; set; }
        public int Health { get; set; }
        public bool GravityField { get; set; }
        public KNIFE Knife { get; set; }
        public float DodgeRate { get; set; }

        public GameObject GravityInstance = null;

        public GameObject Knife_AC_Instance = null;

        public GameObject Knife_AV_Instance = null;

        public GameObject Reflect_AV_Instance = null;


        public Armor(bool canReflect = false, int armorValue = 0, int health = 0, bool gravityField = false, string knife = "NOT_OWNED", float dodgeRate = 0)
        {
            CanReflect = canReflect;
            ArmorValue = armorValue;
            Health = health;
            GravityField = gravityField;
            try
            {
                SetKnife(knife);
            }
            catch
            {
                Console.WriteLine($"The value of Knife is wrong, and cannot be set!");
            }
            DodgeRate = dodgeRate;
        }

        public void UpdateArmor(bool canReflect, int armorValue, int health, bool gravityField, string knife, float dodgeRate, GameObject player)
        {
            CanReflect = canReflect;
            if (CanReflect == true)
            {
                Reflect_AV(player);
            }
            else
            {
                GameObject.Destroy(Reflect_AV_Instance);
                Reflect_AV_Instance = null;
            }
            ArmorValue = armorValue;
            Health = health;
            GravityField = gravityField;
            if (GravityField == true)
            {
                GravityFieldEffect(player);
            }
            else
            {
                CancelGravityFieldEffect(player);
            }
            try
            {
                SetKnife(knife);
                if (knife == "AVAILABLE")
                {
                    Knife_AV(player);
                }
                else if (knife == "ACTIVE")
                {
                    Knife_AC(player);
                }
            }
            catch
            {
                Console.WriteLine($"The value of Knife is wrong, and cannot be set!");
            }
            DodgeRate = dodgeRate;
        }

        public void GravityFieldEffect(GameObject player)
        {
            if (GravityInstance != null)
                return;
            // ������ЧԤ�Ƽ�
            GameObject effectPrefab = Resources.Load<GameObject>($"Effects/GRAVITY");

            if (effectPrefab != null)
            {
                // ʵ������Ч����������� player's TankObject ��
                Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);
                GravityInstance = GameObject.Instantiate(effectPrefab, player.transform.position, rotation, player.transform);

                // ��ѡ��������Чʵ�����������ڣ�������Ч��3�������
                //GameObject.Destroy(effectInstance, 3f);
            }
            else
            {
                Debug.LogWarning($"��Ч GRAVITY δ�ҵ�!");
            }
        }

        public void CancelGravityFieldEffect(GameObject player)
        {
            if (GravityInstance == null)
                return;
            GameObject.Destroy(GravityInstance);
        }

        public void UpdateArmor(Armor armor, GameObject player)
        {
            UpdateArmor(armor.CanReflect,armor.ArmorValue,armor.Health,armor.GravityField,armor.Knife.ToString(),armor.DodgeRate, player);
        }

        public bool SetKnife(string knifeString)
        {
            if (Enum.TryParse(knifeString, out KNIFE knife))
            {
                Knife = knife;
                return true;
            }
            else
            {
                Knife = KNIFE.NOT_OWNED; // �������ʧ�ܣ�����ΪĬ��ֵ
                return false;
            }
        }

        public void Knife_AC(GameObject player)
        {
            if (Knife_AC_Instance != null)
                return;
            // ������ЧԤ�Ƽ�
            GameObject Knife_AC_Prefab = Resources.Load<GameObject>($"Effects/KNIFE_AC");

            if (Knife_AC_Prefab != null)
            {
                // ʵ������Ч����������� player's TankObject ��
                // Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);
                Knife_AC_Instance = GameObject.Instantiate(Knife_AC_Prefab, player.transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity, player.transform);

                if (Knife_AV_Instance != null)
                    GameObject.Destroy(Knife_AV_Instance);
                GameObject.Destroy(Knife_AC_Instance, 3f);
            }
            else
            {
                Debug.LogWarning($"��Ч KNIFE_AC δ�ҵ�!");
            }
        }

        public void Knife_AV(GameObject player)
        {
            if (Knife_AV_Instance != null)
                return;
            // ������ЧԤ�Ƽ�
            GameObject Knife_AV_Prefab = Resources.Load<GameObject>($"Effects/KNIFE_AV");

            if (Knife_AV_Prefab != null)
            {
                // ʵ������Ч����������� player's TankObject ��
                // Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);
                Knife_AV_Instance = GameObject.Instantiate(Knife_AV_Prefab, player.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity, player.transform);

            }
            else
            {
                Debug.LogWarning($"��Ч KNIFE_AC δ�ҵ�!");
            }
        }

        public void Reflect_AV(GameObject player)
        {
            if (Reflect_AV_Instance != null)
                return;
            // ������ЧԤ�Ƽ�
            GameObject Reflect_AV_Prefab = Resources.Load<GameObject>($"Effects/REFLECT_AV");

            if (Reflect_AV_Prefab != null)
            {
                // ʵ������Ч����������� player's TankObject ��
                Reflect_AV_Instance = GameObject.Instantiate(Reflect_AV_Prefab, player.transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity, player.transform);
               
            }
            else
            {
                Debug.LogWarning($"��Ч REFLECT_AV δ�ҵ�!");
            }
        }
    }
}