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
			PlayingField.DrawField ();
			Thread.Sleep (3000);




            //Run the game loop
			while(!SwinGame.WindowCloseRequested() && !SwinGame.KeyTyped(KeyCode.vk_ESCAPE))
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();

				//Clear the screen and draw the framerate

				PlayingField.DrawField ();
				PlayingField.ProcessInput ();
				PlayingField.ProcessMovement ();
				PlayingField.CheckHealthOfField ();

				if (PlayingField.CheckGameOver () == true)
					break;

				if (PlayingField.NumberOfBricks <= 0) {
					PlayingField.ResetBricks ();
					PlayingField.myBall.ResetLocation ();
					PlayingField.DisplayResetBricksScreen ();
					Thread.Sleep (3000);
					PlayingField.DrawField ();
					Thread.Sleep (2000);
				}
            }

			PlayingField.DisplayGameOver ();
			Thread.Sleep (5000);
        }
    }
}