using System;
using System.IO;

namespace Snake
{
  internal class Game {
    public const int HEIGHT = 40;
    public const int WIDTH = 120;
    private static void PrepareScreen() {
      Console.SetWindowSize( WIDTH, HEIGHT + 3 );
      Console.SetBufferSize( WIDTH, HEIGHT + 3 );
      Console.CursorVisible = false;
    }

    private static void DrawBottomBorder() {
      for(int i = 0; i < WIDTH; i++) {
        Console.SetCursorPosition( i, HEIGHT + 1 );
        Console.Write("V");
      }
    }

    private static void ShowScore(Player player) {
      Console.SetCursorPosition( 0, HEIGHT + 2 );
      Console.Write(@"Current score: ");

      Console.ForegroundColor = ConsoleColor.Red;
      Console.Write(player.size);
      Console.ResetColor();
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
      ConsoleKeyInfo key;

      do {
        Player player = new Player();
        Food food = new Food();

        PrepareScreen();
        DrawBottomBorder();

        while(true) {
          ShowScore(player);
          food.Draw();
          player.Erase();

          if (Console.KeyAvailable == true) {
            key = ReadUserInput();

            player.Rotate(key);
          }

          player.Move();
          player.Draw();

          if ( player.Head().x == food.x && player.Head().y == food.y ) player.Consume( food );

          if ( player.Collision() == true ) break;

          System.Threading.Thread.Sleep( 50 );
        }

        // Console.Clear();
        Console.SetCursorPosition( 0, HEIGHT + 2 );
        Console.WriteLine(@"Game over! Your score is {0}. Press enter to enter menu.", player.size);
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
