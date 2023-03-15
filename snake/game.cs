using System;
using System.IO;

namespace Snake
{
  internal class Game {
    public const int HEIGHT = 40;
    public const int WIDTH = 120;
    private static void PrepareScreen(){
      Console.SetWindowSize(WIDTH, HEIGHT);
      Console.SetBufferSize(WIDTH, HEIGHT);
      Console.CursorVisible = false;
    }

    private static ConsoleKeyInfo ReadUserInput() {
      ConsoleKeyInfo result;

      Console.SetCursorPosition(0, 0);
      result = Console.ReadKey();

      Console.SetCursorPosition(0, 0);
      Console.Write(" ");

      return result;
    }

    static void Main(){
      bool game_ongoing = true;
      Player player = new Player();
      Food food = new Food();

      PrepareScreen();

      while(game_ongoing){
        food.Draw();
        player.Erase();

        if (Console.KeyAvailable == true) {
          ConsoleKeyInfo key;
          key = ReadUserInput();

          player.Rotate(key);
        }

        player.Move();
        player.Draw();

        if (player.Head().x == food.x && player.Head().y == food.y) player.Consume(food);

        if (player.Collision() == true) game_ongoing = false;

        System.Threading.Thread.Sleep(50);
      }

      Console.Clear();
      Console.SetCursorPosition(0, HEIGHT - 1);
      Console.WriteLine(@"Game over! Your score is {0}", player.size);
      Console.ReadLine();
    }
  }
}
