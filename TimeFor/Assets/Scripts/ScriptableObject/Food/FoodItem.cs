using UnityEngine;

[CreateAssetMenu(fileName = "FoodItem", menuName = "Inventory/Items/Food")]
public class foodItem : Item
{
    [Header("�������������� ���")]

    [Header("��������� �������")]
    public bool isConsumeable;

    public int changeHealth;
    public int changeStamina;
}


