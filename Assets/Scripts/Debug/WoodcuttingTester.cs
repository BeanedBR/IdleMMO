using System.Collections.Generic;
using Assets.Scripts.Core.Actions;
using Assets.Scripts.Skills.Woodcutting;
using Assets.Data.Items;
using UnityEngine;
using Unity.VisualScripting;
using System;
using Assets.Scripts.Skills;

public sealed class WoodcuttingTester : MonoBehaviour
{
    [SerializeField] private bool startNormalTreeOnPlay = true;

    [Header("Item assets")]
    [SerializeField] private ItemData normalLogs;
    [SerializeField] private ItemData oakLogs;

    [SerializeField] private int LogsToRemove = 3;
    [SerializeField] private ItemData LogTypeToRemove;

    private WoodcuttingAction normalTree;
    private WoodcuttingAction oakTree;

    private void Awake()
    {
        normalTree = new WoodcuttingAction("Normal Tree", 3f, 10, normalLogs);
        oakTree = new WoodcuttingAction("Oak Tree", 4f, 15, oakLogs);
    }

    private void Start()
    {
        if (startNormalTreeOnPlay) StartNormalTree();
    }

    [ContextMenu("Start Normal Tree")]
    private void StartNormalTree() => ActionManager.Instance.StartAction(normalTree);

    [ContextMenu("Start Oak Tree")]
    private void StartOakTree() => ActionManager.Instance.StartAction(oakTree);

    [ContextMenu("Stop Action")]
    private void StopAction() => ActionManager.Instance.StopAction();

    [ContextMenu("Print Bank")]
    private void PrintBank()
    {
        if (Bank.Instance.Items.Count == 0)
        {
            Debug.Log("Bank is empty");
            return;
        }

        foreach (KeyValuePair<ItemData, int> entry in Bank.Instance.Items)
        {
            Debug.Log($"Bank: {entry.Key.DisplayName} x{entry.Value}");
        }
    }

    [ContextMenu("Print Skills")]
    private void PrintSkills()
    {
        foreach (Skill skill in SkillManager.Instance.All)
        {
            int nextLevelXp = ExperienceTable.GetXpForLevel(skill.Level + 1);

            Debug.Log($"{skill.Type}: level {skill.Level}, {skill.Xp} XP " +
                    $"(next level at {nextLevelXp} XP)");
        }
    }

    [ContextMenu("Add 100,000 Woodcutting XP")]
    private void AddLotsOfXP()
    {
        Skill woodcutting = SkillManager.Instance.Get(SkillType.Woodcutting);
        int levelsGained = woodcutting.AddXp(100000);
        Debug.Log($"+100,000 XP: gained {levelsGained} levels, Woodcutting is now level {woodcutting.Level}");

    }


    [ContextMenu("Remove x amount of Logs")]
    private void RemoveNormalLogs()
    {
        bool removed = Bank.Instance.TryRemove(LogTypeToRemove, LogsToRemove);
        Debug.Log(removed 
            ? $"Removed {LogsToRemove} {LogTypeToRemove.DisplayName}. {Bank.Instance.GetQuantity(LogTypeToRemove)} left" 
            : $"Not enough {LogTypeToRemove.DisplayName} Logs (have {Bank.Instance.GetQuantity(LogTypeToRemove)}), nothing removed");
    }
}
