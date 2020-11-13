public interface CL_Machine 
{
    CL_Resources CostToBuild { get; }
    int ProcessSpeed { get; }
    CL_Resources ResourceCanCollect { get; }
    CL_Resources ResourcesCollected { get; set; }
}


public class Miner : CL_Machine
{
    public CL_Resources CostToBuild { get { return CostToBuild; } }
    public int ProcessSpeed { get { return ProcessSpeed; } }
    public CL_Resources ResourceCanCollect { get { return ResourceCanCollect; } }
    public CL_Resources ResourcesCollected { get { return ResourcesCollected; } set { } }
}