using static Common.Fsm<Crops.Crop>;

namespace Crops.States
{
    public class CropInit : AState
    {
        public override void Enter()
        {
            Context.Updater.AddListener(Context);
            Context.OnDestroyHandler += Context.Destroy;
        }
    }
}