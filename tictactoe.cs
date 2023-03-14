using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace TicTacToe {
  internal class History {
    // string[] turns;
    List<string> turns;

    public History() {
      turns = new List<string>();
    }

    public void Remember(string figure, int field){
      string record = $"Turn #{turns.Count}: {figure} at grid #{field}";
      turns.Add(record);
    }

    public void Draw() {
      foreach (var turn in turns) {
        Console.WriteLine(turn);
      }
    }
  }

  internal class Player {
    public string figure;
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

    public bool CheckWin(Grid grid) {
      bool win = false;
      List<int> marked = new List<int>();

      // grid indexes of marked elements
      for(int i = 0; i < grid.grid.Length; i++) {
        if(grid.grid[i].value == figure) {
          marked.Add(i);
        }
      }

      // TODO: rewrite with loops? TBD
      // horizontal lines
      if (marked.Contains(0) && marked.Contains(1) && marked.Contains(2)) win = true;
      if (marked.Contains(3) && marked.Contains(4) && marked.Contains(5)) win = true;
      if (marked.Contains(6) && marked.Contains(7) && marked.Contains(8)) win = true;

      // vertical lines
      if (marked.Contains(0) && marked.Contains(3) && marked.Contains(6)) win = true;
      if (marked.Contains(1) && marked.Contains(4) && marked.Contains(7)) win = true;
      if (marked.Contains(2) && marked.Contains(5) && marked.Contains(8)) win = true;

      // diagonal lines
      if (marked.Contains(0) && marked.Contains(4) && marked.Contains(8)) win = true;
      if (marked.Contains(2) && marked.Contains(4) && marked.Contains(6)) win = true;

      return win;
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

    public void Mark(string figure) {
      value = figure;
      busy = true;
    }

    public void Draw() {
      switch(value) {
        case FIGURE_CROSS:
          Console.BackgroundColor = ConsoleColor.Blue;
          Console.ForegroundColor = ConsoleColor.Red;

          break;
        case FIGURE_CIRCLE:
          Console.BackgroundColor = ConsoleColor.Red;
          Console.ForegroundColor = ConsoleColor.Blue;

          break;
      }

      Console.Write(@"|{0}|", value);
      Console.ResetColor();
    }
  }

  internal class Grid {
    public Field[] grid;
    private History history;

    public Grid() {
      grid = new Field[9];
      history = new History();

      for(int i = 0; i < 9; i++) {
        string shown_number = (i + 1).ToString();
        grid[i] = new Field(shown_number);
      }
    }

    public void Draw() {
      Console.Clear();

      for(int i = 0; i < 9; i++) {
        grid[i].Draw();

        if ((i + 1) % 3 == 0 ) {
          Console.WriteLine();
        }
      }

      history.Draw();
    }

    public void Turn(int number, string figure) {
      grid[number].Mark(figure);
      history.Remember(figure, number);
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
          Console.WriteLine("It's a draw!");
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
            if (grid.CheckField(field - 1)) throw new IndexOutOfRangeException();

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

          // Check win condition
          if (player.CheckWin(grid)) {
            grid.Draw();
            Console.WriteLine("You win!");
            gameOngoing = false;

            break;
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

          if (bot.CheckWin(grid)) {
            grid.Draw();
            Console.WriteLine("Bot wins!");
            gameOngoing = false;

            break;
          }

          // Player switch
          bot.Turn();
          player.Ready();
        }
      }

      Console.ReadKey();
    }
  }
}
