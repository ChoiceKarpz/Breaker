using System;
using System.Threading;
using SwinGameSDK;
using Newtonsoft.Json;
namespace MyGame
{
    public class GameMain
    {
		public static readonly int ScreenWidth = 800;
		public static readonly int ScreenHeight = 600;
        public static void Main()
        {
			//Open the game window
			SwinGame.OpenGraphicsWindow ("Breaker", ScreenWidth, ScreenHeight);
			//attempt to load colours from a JSON file
			PlayingField.LoadColors ();
			//make game objects
			PlayingField.GenerateWalls ();
			PlayingField.GenerateBricks ();
			//draw the field
			PlayingField.DrawField ();
			//pause the screen for 5 seconds so they have time to take in what is happening before the game starts
			Thread.Sleep (5000);




            //Run the game loop
			while(!SwinGame.WindowCloseRequested() && !SwinGame.KeyTyped(KeyCode.vk_ESCAPE))
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();

				//Clear the screen, process user imput, draw the screen, and check that the board doesn't need resetting

				PlayingField.DrawField ();
				PlayingField.ProcessInput ();
				PlayingField.ProcessMovement ();

				//check the health of each brick and wall, which then delete themselves if they do not have any health left
				PlayingField.CheckHealthOfField ();

				//check that the ball hasn't gone off of the screen
				if (PlayingField.CheckGameOver () == true)
					break;

				//check that the bricks don't need resetting
				if (PlayingField.NumberOfBricks <= 0) {
					PlayingField.ResetBricks ();
					PlayingField.myBall.ResetLocation ();
					PlayingField.myBall.IncreaseSpeed ();
					PlayingField.DisplayResetBricksScreen ();
					Thread.Sleep (3000);
					PlayingField.DrawField ();
					Thread.Sleep (2000);
				}
            }

			//call a method to display a game over screen, which includes the score the user got
			PlayingField.DisplayGameOver ();
			//pause for 5 seconds before the program closes so they have time to read what is on the screen
			Thread.Sleep (5000);
        }
    }
}