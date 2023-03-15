using System;
using System.IO;

namespace Snake
{
  internal class Player {
    public const int DIR_UP = 0;
    public const int DIR_LEFT = 1;
    public const int DIR_RIGHT = 2;
    public const int DIR_DOWN = 3;
    public const int SPEED_X = 2;
    public const int SPEED_Y = 1;

    public const string BLOCK = "██";

    public int head_x;
    public int head_y;
    int dir;
    int size;

    public Player(){
      head_x = 20;
      head_y = 10;
      dir = 0;
      size = 1;
    }

    public void Rotate(ConsoleKeyInfo key) {
      switch(key.Key) {
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

    public void Move() {
      switch(dir) {
        case DIR_UP:
          head_y -= SPEED_Y;
          break;
        case DIR_LEFT:
          head_x -= SPEED_X;
          break;
        case DIR_RIGHT:
          head_x += SPEED_X;
          break;
        case DIR_DOWN:
          head_y += SPEED_Y;
          break;
      }
    }

    public void Draw() {
      Console.SetCursorPosition(head_x, head_y);
      Console.Write(BLOCK);
    }

    public void Erase() {
      Console.SetCursorPosition(head_x, head_y);
      Console.Write("  ");
    }

    public void Consume(Food food) {
      size++;
      food.Redraw();
    }
  }
}
