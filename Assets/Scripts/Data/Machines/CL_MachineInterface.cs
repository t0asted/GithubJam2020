public interface CL_MachineInterface
{
    CL_ResourcesScriptable CostToBuild { get; }
    CL_ResourcesScriptable ResourceCanCollect { get; }
    CL_Resources ResourcesCollected { get; }
    int ProcessSpeed { get; }
    int AmountCanProcess { get; }

    void Process();
    string ResourceContent();
}
