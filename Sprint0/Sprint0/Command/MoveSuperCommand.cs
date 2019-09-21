using Sprint0.MarioClasses;

namespace Sprint0
{
    public class MoveSuperCommand : ICommand
    {
        Mario mario;
        public MoveSuperCommand(Mario mario)
        {
            this.mario = mario;
        }

        public void Execute()
        {
            mario.MoveSuper();
        }
    }
}
