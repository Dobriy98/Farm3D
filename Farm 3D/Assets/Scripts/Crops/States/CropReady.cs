using Common;
using UnityEngine;
using static Common.Fsm<Crops.Crop>;

namespace Crops.States
{
    public class CropReady : AState, ISignalHandler<string>
    {
        public override void Enter()
        {
            int expCount = Mathf.FloorToInt(Context.RipeningTime);
            Context.ExperienceCounter.AddExp(expCount);
        }

        public void Signal(string signal)
        {
            switch (signal)
            {
                case "Collect":
                    Context.OnDestroyHandler?.Invoke();
                    Context.CropCounter.AddTo(Context.CropModel.cropType, Context.CropModel.pointsToAdd);
                    Object.Destroy(Context.CurrentCropView.gameObject);
                    break;
            }
        }
    }
}