using System;
using System.IO;

namespace Snake
{
  internal class Player {
    public const int DIR_UP = 0;
    public const int DIR_LEFT = 1;
    public const int DIR_RIGHT = 2;
    public const int DIR_DOWN = 3;

    int head_x;
    int head_y;
    int dir;

    public Player(){
      head_x = 20;
      head_y = 10;
      dir = 0;
    }

    public void Rotate(ConsoleKeyInfo key) {
      switch(key.key) {
        case ConsoleKey.W:
          dir = DIR_UP;
          break;
        case ConsoleKey.A:
          dir = DIR_LEFT;
          break;
        case ConsoleKey.S:
          dir = DIR_DOWN;
          break;
        case ConsoleKey.D:
          dir = DIR_RIGHT;
          break;
      }
    }
  }
}
