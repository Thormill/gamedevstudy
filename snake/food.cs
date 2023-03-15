using System;
using System.IO;

namespace Snake
{
  internal class Food {

    public const string BLOCK = "██";

    int x;
    int y;

    public Food(){
      Random rnd = new Random();

      x = rnd.Next(0, 120);
      if (x % 2 != 0) x+= 1;

      y = rnd.Next(0, 40);
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
