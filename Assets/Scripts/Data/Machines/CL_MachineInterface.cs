using System.Collections.Generic;

public interface CL_MachineInterface
{
    List<SO_Item> CostToBuild { get; }
    List<SO_Item> ResourceCanCollect { get; }
    CL_Storage ResourcesCollected { get; }
    int ProcessSpeed { get; }
    int AmountCanProcess { get; }

    void Process();
    string ResourceContent();
}
