using Assets.Scripts.Core.Actions;
using Assets.Scripts.Skills.Woodcutting;
using UnityEngine;

public sealed class WoodcuttingTester : MonoBehaviour
{
    [SerializeField] private bool startNormalTreeOnPlay = true;

    private readonly WoodcuttingAction normalTree = new WoodcuttingAction("Normal Tree", 3f, 10);
    private readonly WoodcuttingAction oakTree = new WoodcuttingAction("Oak Tree", 4f, 15);

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
}
