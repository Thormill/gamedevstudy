using System;
using System.IO;

namespace Snake
{
  internal class Food {

    public const string BLOCK = "██";
    public ConsoleColor color;

    public int x;
    public int y;

    public Food(){
      Random rnd = new Random();

      x = rnd.Next(0, 120);
      if (x % 2 != 0) x += 1;

      y = rnd.Next(0, 40);

      ChangeColor();
    }

    private void ChangeColor() {
      var consoleColors = Enum.GetValues(typeof(ConsoleColor));
      Random rnd = new Random();

      do {
        color = (ConsoleColor)consoleColors.GetValue(rnd.Next(consoleColors.Length));
      } while(color == ConsoleColor.Black);
    }

    public void Draw() {
      Console.SetCursorPosition(x, y);
      Console.ForegroundColor = color;
      Console.Write(BLOCK);
      Console.ResetColor();
    }

    public void Redraw(Player player) {
      Random rnd = new Random();

      do {
        x = rnd.Next(0, Game.WIDTH);
        if (x % 2 != 0) x += 1;

        y = rnd.Next(0, Game.HEIGHT);
      } while( player.Collision(this) == true );

      Erase();
      ChangeColor();
      Draw();
    }
    public void Erase() {
      Console.SetCursorPosition(x, y);
      Console.Write("  ");
    }
  }
}
