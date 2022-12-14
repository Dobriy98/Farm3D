using static Common.Fsm<Character.MainCharacter>;

namespace Character.States
{
    public class CharacterInit: AState
    {
        public override void Enter()
        {
            Context.Updater.AddListener(Context);
            Context.CharacterModel.characterCameraState.target = Context.CharacterView.transform;

            Context.CharacterView.OnDestroyHandler += Context.Destroy;
            
            Context.CharacterView.SetInitPosition(Context.CharacterModel.initPosition);
            Context.CharacterView.SetMovementSpeed(Context.CharacterModel.walkSpeed);
        }
    }
}