using System;
using SwinGameSDK;

namespace MyGame
{
    public class GameMain
    {
		public static readonly int ScreenWidth = 800;
		public static readonly int ScreenHeight = 600;
        public static void Main()
        {
			//get game ready
			PlayingField.GenerateWalls ();
			Console.WriteLine (PlayingField.Walls.Count);
			
			//Open the game window
			SwinGame.OpenGraphicsWindow ("Breaker", ScreenWidth, ScreenHeight);

            //Run the game loop
			while(!SwinGame.WindowCloseRequested() && !SwinGame.KeyTyped(KeyCode.vk_ESCAPE))
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();

				//Clear the screen and draw the framerate
				SwinGame.ClearScreen (Color.Black);
				PlayingField.DrawField ();
				PlayingField.ProcessInput ();
				PlayingField.ProcessMovement ();
				PlayingField.CheckHealthOfField ();
                
                //Draw onto the screen
                SwinGame.RefreshScreen(60);
            }
        }
    }
}