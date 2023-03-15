using System;
using System.IO;

namespace Snake
{
  internal class Segment {
    public const int DIR_UP = 0;
    public const int DIR_LEFT = 1;
    public const int DIR_RIGHT = 2;
    public const int DIR_DOWN = 3;
    public const int SPEED_X = 2;
    public const int SPEED_Y = 1;


    public const string BLOCK = "██";

    public int x; // TODO: check for private
    public int y; // TODO: check for private
    public int dir;
    int move_delay;

    public Segment(int x = 20, int y = 10, int dir = DIR_UP, int delay = 0){
      this.x = x;
      this.y = y;
      this.dir = dir;
      this.move_delay = delay;
    }

    public void Rotate(int dir) {
      switch(dir) {
        case DIR_UP:
          this.dir = DIR_UP;
          break;
        case DIR_LEFT:
          this.dir = DIR_LEFT;
          break;
        case DIR_DOWN:
          this.dir = DIR_DOWN;
          break;
        case DIR_RIGHT:
          this.dir = DIR_RIGHT;
          break;
      }
    }

    public void Move() {
      if (move_delay > 0) {
        move_delay--;

        return;
      }

      switch(dir) {
        case DIR_UP:
          if (y - SPEED_Y >= 0) {
            y -= SPEED_Y;
          } else {
            y = Game.HEIGHT - 1;
          }
          break;
        case DIR_LEFT:
          if (x - SPEED_X >= 0) {
            x -= SPEED_X;
          } else {
            x = Game.WIDTH - 2;
          }
          break;
        case DIR_RIGHT:
          if (x + SPEED_X + 1 < Game.WIDTH) {
            x += SPEED_X;
          } else {
            x = 0;
          }
          break;
        case DIR_DOWN:
          if (y + SPEED_Y < Game.HEIGHT) {
            y += SPEED_Y;
          } else {
            y = 0;
          }
          break;
      }
    }

    public void Draw() {
      Console.SetCursorPosition(x, y);
      Console.Write(BLOCK);
    }

    public void Erase() {
      Console.SetCursorPosition(x, y);
      Console.Write("  ");
    }
  }
}
