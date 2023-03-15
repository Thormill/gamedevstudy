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
          if (dir == DIR_DOWN) break;

          dir = DIR_UP;
          break;
        case ConsoleKey.A:
          if (dir == DIR_RIGHT) break;

          dir = DIR_LEFT;
          break;
        case ConsoleKey.S:
          if (dir == DIR_UP) break;

          dir = DIR_DOWN;
          break;
        case ConsoleKey.D:
          if (dir == DIR_LEFT) break;

          dir = DIR_RIGHT;
          break;
      }
    }

    public void Move() {
      switch(dir) {
        case DIR_UP:
          if (head_y - SPEED_Y > 0) {
            head_y -= SPEED_Y;
          } else {
            head_y = Game.HEIGHT - 1;
          }
          break;
        case DIR_LEFT:
          if (head_x - SPEED_X >= 1) {
            head_x -= SPEED_X;
          } else {
            head_x = Game.WIDTH - 2;
          }
          break;
        case DIR_RIGHT:
          if (head_x + SPEED_X + 1 < Game.WIDTH) {
            head_x += SPEED_X;
          } else {
            head_x = 0;
          }
          break;
        case DIR_DOWN:
          if (head_y + SPEED_Y < Game.HEIGHT) {
            head_y += SPEED_Y;
          } else {
            head_y = 0;
          }
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
