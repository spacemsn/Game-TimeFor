using System;
using Unity;
using UnityEngine;
using System.Collections;

public enum ArtifactType { Defaunt, Ring, Amulet, Headdress, }
[CreateAssetMenu(fileName = "Artifact", menuName = "Artifacts")]

public class ArtifactsObject : Item
{
    [Header("��� ���������")]
    public ArtifactType artifact;

    [Header("���� ���������� �������������")]
    [Header("����� �����")]
    public float healthIncrease;
    public float staminaIncrease;
    [Header("����� �������")]
    public float damageIncrease;
    [Header("����� �����")]
    public float damagePercentIncrease;
}