namespace Sprint1
{
    public class QuitCommand : ICommand
    {
        Sprint1Main game;

        public QuitCommand(Sprint1Main myGame)
        {
            game = myGame;
        }

        public void Execute()
        {
            game.Exit();
        }
    }
}