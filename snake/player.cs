using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


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

    // public int head_x;
    // public int head_y;
    int dir;
    int size;

    List<Segment> segments;

    public Player(){
      size = 1;

      segments = new List<Segment>();
      segments.Add(new Segment(20, 10, DIR_UP));
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
      for(int i = segments.Count; i > 1; i--){
        Segment segment = segments.ElementAt(i - 1);
        Segment previous_segment = segments.ElementAt(i - 2);

        segment.Rotate(previous_segment.dir);
      }
      // for(int i = 0; i < segments.Count - 1; i++){
      //   Segment segment = segments.ElementAt(i);
      //   Segment next_segment = segments.ElementAt(i + 1);

      //   next_segment.Rotate(segment.dir);
      // }

      Segment head = segments.ElementAt(0);
      head.Rotate(dir);

      foreach (var segment in segments) {
        segment.Move();
      }
    }

    public void Draw() {
      foreach (var segment in segments) {
        segment.Draw();
      }
    }

    public void Erase() {
      foreach (var segment in segments) {
        segment.Erase();
      }
    }

    public void Consume(Food food) {
      size++;
      Segment head = segments.ElementAt(0);
      segments.Add(new Segment(head.x, head.y, head.dir, size - 1));
      food.Redraw();
    }

    public Segment Head() {
      return segments.ElementAt(0);
    }
  }
}
