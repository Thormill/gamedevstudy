using System;
using System.IO;

namespace Snake
{
  internal class Game {
    public const int SCREEN_DELAY = 50;
    public const int HEIGHT = 40;
    public const int WIDTH = 120;
    private static void PrepareScreen() {
      Console.SetWindowSize( WIDTH, HEIGHT + 2 );
      Console.SetBufferSize( WIDTH, HEIGHT + 2 );
      Console.CursorVisible = false;
    }

    private static void DrawBottomBorder() {
      for(int i = 0; i < WIDTH; i++) {
        Console.SetCursorPosition( i, HEIGHT );
        Console.Write("V");
      }
    }

    private static void ShowScore(Player player, History history) {
      Console.SetCursorPosition( 0, HEIGHT + 1 );
      Console.Write(@"Current score: ");

      Console.ForegroundColor = ConsoleColor.Red;
      Console.Write(player.size);
      Console.ResetColor();

      Console.Write(@"; Current speed: {0}", (player.size >= SCREEN_DELAY ? SCREEN_DELAY : player.size) / 10);
      Console.Write(@"       Record is {0}", history.record);
    }

    private static ConsoleKeyInfo ReadUserInput() {
      ConsoleKeyInfo result;

      Console.SetCursorPosition( 0, 0 );
      result = Console.ReadKey();

      Console.SetCursorPosition( 0, 0 );
      Console.Write(" ");

      return result;
    }

    static void Main(){
      bool game_ongoing = true;
      History history = new History();
      ConsoleKeyInfo key;

      do {
        Player player = new Player();
        Food food = new Food();

        int speed = 0;

        PrepareScreen();
        DrawBottomBorder();

        while(true) {
          ShowScore(player, history);

          food.Draw();

          if (Console.KeyAvailable == true) {
            key = ReadUserInput();

            player.Rotate(key);
          }

          if ( player.size < SCREEN_DELAY && player.size % 10 == 0 ) {
            speed = player.size ;
          }

          player.Erase();
          player.Move();
          player.Draw();

          if ( player.Head().x == food.x && player.Head().y == food.y ) player.Consume( food );

          if ( player.Collision() == true ) break;

          System.Threading.Thread.Sleep( SCREEN_DELAY - speed );
        }

        Console.SetCursorPosition( 0, HEIGHT + 1 );
        Console.WriteLine(@"Game over! Your score is {0}. Press enter to enter menu.", player.size);

        if (history.record < player.size) {
          Console.WriteLine(@"New Record! {0}! {1} above previous result!", player.size, player.size - history.record);
        }

        history.Remember(player.size);
        history.Draw();

        Console.ReadLine();
        Console.WriteLine("Press Y to continue");

        key = Console.ReadKey();
        if ( key.Key == ConsoleKey.Y ) {
          Console.Clear();
        } else {
          game_ongoing = false;
        }
      } while(game_ongoing);
    }
  }
}
