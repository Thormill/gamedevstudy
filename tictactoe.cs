using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace TicTacToe {
  // class of the field
  // class of the field square

  internal class Player {
    private string figure;
    public bool current_turn;

    public Player(string fig) {
      figure = fig;
      if (figure == Field.FIGURE_CROSS) {
        current_turn = true;
      } else {
        current_turn = false;
      }
    }

    public string Draw() {
      return figure;
    }

    public void Turn() {
      current_turn = false;
    }

    public void Ready() {
      current_turn = true;
    }
  }

  internal class Field {
    public string value;
    public bool busy;

    public const string FIGURE_DEFAULT = "-";
    public const string FIGURE_CROSS = "X";
    public const string FIGURE_CIRCLE = "O";

    public Field(string val = FIGURE_DEFAULT) {
      // TODO: check input for constant matching
      value = val;
      busy = false;
    }

    public void Draw(string val = FIGURE_DEFAULT) {
      value = val;
      busy = true;
    }
  }

  internal class Grid {
    private Field[] grid;

    public Grid() {
      grid = new Field[9];

      for(int i = 0; i < 9; i++) {
        grid[i] = new Field((i + 1).ToString());
      }
    }

    public void Draw() {
      Console.Clear();

      for(int i = 0; i < 9; i++) {
        Console.Write(@"|{0}|", grid[i].value);

        if ((i + 1) % 3 == 0 ) {
          Console.WriteLine();
        }
      }
    }

    public void Turn(int number, string figure) {
      grid[number].Draw(figure);
    }

    public int[] AvailableFields() {
      List<int> result = new List<int>();

      for(int i = 0; i < 9; i++) {
        if(grid[i].busy == false) {
          result.Add(i);
        }
      }

      return result.ToArray();
    }

    public bool CheckField(int number) {
      return grid[number].busy == true;
    }
  }

  class Game {
    static void Main(string[] args) {
      Random rnd = new Random();
      bool gameOngoing = true;

      Grid grid = new Grid();

      Player player;
      Player bot;

      // Toss a coin to your witcher:
      int coin = rnd.Next(1, 100);
      if (coin % 2 == 0) {
        player = new Player(Field.FIGURE_CROSS);
        bot = new Player(Field.FIGURE_CIRCLE);
      } else {
        player = new Player(Field.FIGURE_CIRCLE);
        bot = new Player(Field.FIGURE_CROSS);
      }

      Console.WriteLine(@"You are playing as {0}", player.Draw());
      Console.WriteLine("Press any key to start!");
      Console.ReadKey();

      while (gameOngoing) {
        grid.Draw();

        int[] free_fields = grid.AvailableFields();

        if (free_fields.Length == 0) {
          gameOngoing = false;

          break;
        }

        // Player turn
        if (player.current_turn == true) {
          Console.WriteLine("Enter field number");

          // User Input check
          try {
            int field = int.Parse(Console.ReadLine());

            // Check if field is already marked
            if (grid.CheckField(field - 1)) {
              Console.WriteLine("Wrong field! Press any key to retry.");
              Console.ReadKey();

              continue;
            }

            grid.Turn(field - 1, player.Draw());
          } catch(FormatException) {
            Console.WriteLine("Wrong input! Press any key to retry.");
            Console.ReadKey();

            continue;
          } catch(IndexOutOfRangeException) {
            Console.WriteLine("Wrong number! Press any key to retry.");
            Console.ReadKey();

            continue;
          }

          // Player switch
          player.Turn();
          bot.Ready();

          // Avoid passing turn to AI while current turn
          continue;
        } else {
          // AI turn
          int bot_field = rnd.Next(0, free_fields.Length);
          grid.Turn(free_fields[bot_field], bot.Draw());

          // Player switch
          bot.Turn();
          player.Ready();
        }
      }

      grid.Draw();
      Console.WriteLine("GameOver");
      Console.ReadKey();
    }
  }
}
