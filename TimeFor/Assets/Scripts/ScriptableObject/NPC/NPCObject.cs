using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "Object/ItemObject/NPC")]
public class NPCObject : ItemObject
{
    [Header("������� �����")]
    [TextArea(3, 10)]
    public string[] lines;
}
