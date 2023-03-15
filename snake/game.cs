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
    static void Main(){
      bool game_ongoing = true;
      Player player = new Player();

      PrepareScreen();

      while(game_ongoing){
        if (Console.KeyAvailable == true) {
          ConsoleKeyInfo key;
          key = Console.ReadKey();

          player.Rotate(key);
        }

        System.Threading.Thread.Sleep(50);
      }
    }
  }
}
