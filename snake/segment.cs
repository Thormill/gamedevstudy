using System;
using System.IO;

namespace Snake
{
  internal class Segment {
    const string BLOCK = "██";

    public int x, y, dir, move_delay;
    public ConsoleColor color;

    public Segment(int x = 20, int y = 10, int dir = Player.DIR_UP, int delay = 0, ConsoleColor color = ConsoleColor.Gray) {
      this.x = x;
      this.y = y;
      this.dir = dir;
      this.move_delay = delay;
      this.color = color;
    }

    public void Rotate(int dir) {
      switch(dir) {
        case Player.DIR_UP:
          this.dir = Player.DIR_UP;
          break;
        case Player.DIR_LEFT:
          this.dir = Player.DIR_LEFT;
          break;
        case Player.DIR_DOWN:
          this.dir = Player.DIR_DOWN;
          break;
        case Player.DIR_RIGHT:
          this.dir = Player.DIR_RIGHT;
          break;
      }
    }

    public void Move() {
      if (move_delay > 0) {
        move_delay--;

        return;
      }

      switch(dir) {
        case Player.DIR_UP:
          if (y - Player.SPEED_Y >= 0) {
            y -= Player.SPEED_Y;
          } else {
            y = Game.HEIGHT - 1;
          }
          break;
        case Player.DIR_LEFT:
          if (x - Player.SPEED_X >= 0) {
            x -= Player.SPEED_X;
          } else {
            x = Game.WIDTH - 2;
          }
          break;
        case Player.DIR_RIGHT:
          if (x + Player.SPEED_X + 1 < Game.WIDTH) {
            x += Player.SPEED_X;
          } else {
            x = 0;
          }
          break;
        case Player.DIR_DOWN:
          if (y + Player.SPEED_Y < Game.HEIGHT) {
            y += Player.SPEED_Y;
          } else {
            y = 0;
          }
          break;
      }
    }

    public void Draw() {
      Console.SetCursorPosition(x, y);
      Console.ForegroundColor = color;
      Console.Write(BLOCK);
      Console.ResetColor();
    }

    public void Erase() {
      Console.SetCursorPosition(x, y);
      Console.Write("  ");
    }
  }
}
