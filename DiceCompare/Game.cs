using System;
using System.Collections.Generic;
using System.Linq;

namespace DiceCompare
{
    public class Game
    {
        public Player Player { get; set; }
        public Player Oponent { get; set; }
        private string activ;

        public Game(Player player, Player oponent)
        {
            Player = player;
            Oponent = oponent;
        }

        public Player Winner { get; set; }

        public void Play()
        {
            Player.Startfield = 1;
            Player.GoalEntrance = 40;
            Player.NoMoveCount = 0;
            Player.Figures = new List<int>() { -100, -100, -100, -100 };

            Oponent.Startfield = 21;
            Oponent.GoalEntrance = 20;
            Oponent.NoMoveCount = 0;
            Oponent.Figures = new List<int>() { -100, -100, -100, -100 };

            activ = Player.Name;
            while (!((Player.Figures.Where(x => x >= 100).Count() == 4) || (Oponent.Figures.Where(x => x >= 100).Count() == 4)))
            {
                if (activ == Player.Name)
                {
                    var role = Player.Dice.Role();
                    Player.Figures = TryMoveAny(Player, role);
                    Oponent.Figures = SendHome(Player.Figures, Oponent.Figures);
                    activ = Oponent.Name;
                }
                else
                {
                    var role = Oponent.Dice.Role();
                    Oponent.Figures = TryMoveAny(Oponent, role);
                    Player.Figures = SendHome(Oponent.Figures, Player.Figures);

                    activ = Player.Name;
                }
                if (Player.NoMoveCount > 10 && Oponent.NoMoveCount > 10)
                {
                    activ = "";
                    break;
                }
            }
            if (activ == Player.Name)
                Winner = Oponent;
            else if (activ == Oponent.Name)
                Winner = Player;
            else
                Winner = new Player("No Winner", true);
            Player.GamesPlayed++;
            Oponent.GamesPlayed++;

        }

        private List<int> SendHome(List<int> figuresActive, List<int> figuresPassive)
        {
            var inter = figuresPassive.Intersect(figuresActive);
            if (inter.Count() > 0 && inter.First() != -100 && inter != null)
            {                
                figuresPassive[figuresPassive.IndexOf(inter.First())] = -100;
                figuresPassive = figuresPassive.OrderByDescending(x=>x).ToList();
            }

            return figuresPassive;
        }

        private List<int> TryMoveAny(Player player, int role)
        {
            if (role == 6 && player.Figures.Contains(-100) && !player.Figures.Contains(player.Startfield))
                player.Figures[player.Figures.IndexOf(player.Figures.FirstOrDefault(x => x == -100))] = player.Startfield;
            else if (player.Figures.Where(x => x == -100).Count() < 4)
            {
                var i = 0;
                var moved = false;
                while (!moved)
                {
                    int activFigure = GetActivFigure(player, i);
                    if (CheckAtiveFigureCanMove(player, role, activFigure))
                    {
                        if (player.Figures[activFigure] + role >= 40 && player.Name == Oponent.Name)
                            player.Figures[activFigure] += role - 40;
                        else
                            player.Figures[activFigure] += role;
                        moved = true;
                        player.NoMoveCount = 0;

                    }
                    else if (CheckAtiveFigureCanMoveIntoHome(player, role, activFigure))
                    {
                        if (player.Figures[activFigure] >= 100)
                            player.Figures[activFigure] += role;
                        else
                            player.Figures[activFigure] += role + 100;
                        moved = true;
                        player.NoMoveCount = 0;
                    }
                    else
                    {
                        i++;
                        if (i >=4)
                        {
                            moved = true;
                            player.NoMoveCount++;
                            //Console.WriteLine($"{player.Name} coundn't move {player.NoMoveCount} times. With a role of {role} Positions: {string.Join(",",player.Figures)}");
                        }
                    }
                }
            }
            if (role == 6)
                player.Figures = TryMoveAny(player, player.Dice.Role());
            player.Figures = player.Figures.OrderByDescending(x => x).ToList();
            return player.Figures;

        }

        private bool CheckAtiveFigureCanMoveIntoHome(Player player, int role, int activFigure)
        {
            var figurePosition = player.Figures[activFigure];
            if (player.Figures.Contains(figurePosition + role + 100) || figurePosition == -100)
                return false;
            if (figurePosition + role + 100 <= player.GoalEntrance + 104)
                return true;
            if (figurePosition + role <= player.GoalEntrance + 104 && figurePosition>100)
                return true;
            else
                return false;
        }

        private bool CheckAtiveFigureCanMove(Player player, int role, int activFigure)
        {
            var figurePosition = player.Figures[activFigure];            
            if (player.Figures.Contains(figurePosition + role)||figurePosition==-100)
                return false;
            if (player.Name == Player.Name)
            {
                if (figurePosition + role <= player.GoalEntrance)
                    return true;
            }
            else
            {
                if (figurePosition + role <= player.GoalEntrance || (figurePosition >= player.Startfield && figurePosition + role <= 40))
                    return true;
            }
            return false;
        }

        private static int GetActivFigure(Player player, int i)
        {
            return i;
        }
    }
}