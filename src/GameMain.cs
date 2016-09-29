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
			
			//Open the game window
			SwinGame.OpenGraphicsWindow ("Breaker", ScreenWidth, ScreenHeight);
			PlayingField myField = new PlayingField ();
            
            //Run the game loop
            while(false == SwinGame.WindowCloseRequested())
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();

				//Clear the screen and draw the framerate
				SwinGame.ClearScreen (Color.Black);
				myField.DrawField ();
				myField.ProcessInput ();
				myField.ProcessMovement ();

                SwinGame.DrawFramerate(0,0);
                
                //Draw onto the screen
                SwinGame.RefreshScreen(60);
            }
        }
    }
}