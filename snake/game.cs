using System;
using System.IO;

namespace Snake
{
  internal class Game {
    private static void PrepareScreen(){
      Console.SetWindowSize(120, 40);
      Console.SetBufferSize(120, 40);
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

      // first food is spawned before player. TODO: check for collision with player
      food.Draw();

      while(game_ongoing){
        player.Erase();

        if (Console.KeyAvailable == true) {
          ConsoleKeyInfo key;
          key = ReadUserInput();

          player.Rotate(key);
        }

        player.Move();
        player.Draw();

        System.Threading.Thread.Sleep(50);
      }
    }
  }
}
