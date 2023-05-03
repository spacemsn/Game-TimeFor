using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItem", menuName = "Inventory/Items/Food")]
public class foodItem : Item
{
    [Header("�������������� ������������ ��������")]
    [Header("��������� �������")]
    public bool isConsumeable;

    public int changeHealth;
    public int changeHunger;
    public int changeThirst;
}
