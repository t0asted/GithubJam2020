public class S_UI_MachineGatherer : S_InGameMenuBase
{
    private S_MachineGatherer MachineGatherer = null;

    new void Start()
    {
        base.Start();
        if (ref_Machine is S_MachineGatherer)
        {
            MachineGatherer = ref_Machine as S_MachineGatherer;
        }
    }
}
