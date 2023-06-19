using UnityEngine;

[CreateAssetMenu(fileName = "Cell", menuName = "CellElemental")]
public class CellObject : Item
{
    [Header("��� ������")]
    public ElementObject element;

    [Header("��� ������")]
    public CellObject nextCell;

    [Header("��� ������")]
    public int levelCell;

    public enum CellType { ������, ������, ������, }
    [Header("����� ������")]
    public CellType cellType;

    [Header("��� ������")]
    public bool isOpenCell;

    [Header("��� ������")]
    public GameObject totemPrefab;

    [Header("��� ������")]
    public float damageTotem;

    [Header("��� ������")]
    public float timeTotem;

    public void Upgrade()
    {
        if(cellType == CellType.������)
        {
            UpgradeDamage();
        }
        else if(cellType == CellType.������)
        {
            UpgradePercent();
        }
        else if(cellType == CellType.������)
        {
            UpgradeTotem();
        }
    }

    public void UpgradeDamage()
    {
        levelCell++;
        element.baseDamage += 5;

        if(levelCell >= 3)
        {
            nextCell.isOpenCell = true; 
        }
    }

    public void UpgradePercent()
    {
        levelCell++;
        element.basePersent += 0.05f;

        if (levelCell >= 3)
        {
            nextCell.isOpenCell = true;
        }
    }

    public void UpgradeTotem()
    {
        levelCell++;
        damageTotem += 5;
        timeTotem += 1;
    }
}
