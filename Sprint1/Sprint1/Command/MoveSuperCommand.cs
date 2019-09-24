using Sprint1.MarioClasses;

namespace Sprint1
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
