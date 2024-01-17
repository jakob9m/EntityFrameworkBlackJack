using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccess
    {

        private readonly BlackjackDbContext _dbContext;

        public DataAccess()
        {
            _dbContext = new BlackjackDbContext();
        }


        public bool DoesPlayerExist(string playerId)
        {
            using (var context = new BlackjackDbContext())
            {
                return context.Players.Any(p => p.PlayerId == playerId);
            }
        }

        public void AddPlayer(string playerId, string name)
        {
            using (var context = new BlackjackDbContext())
            {
                var player = new Models.Player
                {
                    PlayerId = playerId,
                    Name = name
                };

                context.Players.Add(player);
                context.SaveChanges();
            }
        }

        public DataTable GetAllPlayers()
        {
            using (var context = new BlackjackDbContext())
            {
                DataTable dt = new DataTable();

                // Add columns to the DataTable
                dt.Columns.Add("PlayerId", typeof(string));
                dt.Columns.Add("Name", typeof(string));
             

                _dbContext.Players.AsEnumerable().ToList().ForEach(player =>
                    dt.Rows.Add(new object[] { player.PlayerId, player.Name })
                );

                return dt;
            }
        }

        public void DeletePlayer(string playerId)
        {
            using (var context = new BlackjackDbContext())
            {
                var player = _dbContext.Players.Find(playerId);
                if (player != null)
                {
                    _dbContext.Players.Remove(player);
                    _dbContext.SaveChanges();
                }
            }
        }

        public void DeleteGame(string gameId)
        {
            using (var context = new BlackjackDbContext())
            {
                var game = _dbContext.Games.Find(gameId);
                if (game != null)
                {
                    _dbContext.Games.Remove(game);
                    _dbContext.SaveChanges();
                }
            }
        }


        public void DeleteResult(string gameId, string playerId)
        {
            using (var context = new BlackjackDbContext())
            {
                var result = _dbContext.Results.FirstOrDefault(r => r.GameId == gameId && r.PlayerId == playerId);
                if (result != null)
                {
                    _dbContext.Results.Remove(result);
                    _dbContext.SaveChanges();
                }
            }
        }


        public void UpdatePlayerName(string playerId, string newName)
        {
            using (var context = new BlackjackDbContext())
            {
                var player = _dbContext.Players.Find(playerId);
                if (player != null)
                {
                    player.Name = newName;
                    _dbContext.SaveChanges();
                }
            }
        }

        public int GetLastGameNumber()
        {
            using (var context = new BlackjackDbContext())
            {
                // Retrieve the last game ID
                var lastGame = _dbContext.Games.OrderByDescending(g => g.GameId).FirstOrDefault();

                // If no games exist, return 0
                if (lastGame == null)
                    return 0;

                // Extract the numeric portion of the game ID
                string numericPart = lastGame.GameId.Substring(1);
                int.TryParse(numericPart, out int lastGameNumber);

                return lastGameNumber;
            }
        }

        public void AddGame(string gameId, int noOfPlayers, int deckSize)
        {
            using (var context = new BlackjackDbContext())
            {
                var game = new Models.Game
                {
                    GameId = gameId,
                    NoOfPlayers = noOfPlayers,
                    DeckSize = deckSize
                };

                _dbContext.Games.Add(game);
                _dbContext.SaveChanges();
            }
        }

        public void AddResult(string playerId, string gameId, string resultValue)
        {
            using (var context = new BlackjackDbContext())
            {
                var result = new Models.Result
                {
                    PlayerId = playerId,
                    GameId = gameId,
                    ResultValue = resultValue
                };

                _dbContext.Results.Add(result);
                _dbContext.SaveChanges();
            }
        }

        public DataTable GetGameDetails(string gameId)
        {
            using (var context = new BlackjackDbContext())
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("GameId", typeof(string));
                dt.Columns.Add("NoOfPlayers", typeof(int));
                dt.Columns.Add("DeckSize", typeof(int));

                var gameDetails = _dbContext.Games.FirstOrDefault(game => game.GameId == gameId);

                if (gameDetails != null)
                {
                    dt.Rows.Add(new object[] { gameDetails.GameId, gameDetails.NoOfPlayers, gameDetails.DeckSize });
                }

                return dt;
            }
        }

        public DataTable GetAllGames()
        {
            using (var context = new BlackjackDbContext())
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("GameId", typeof(string));
                dt.Columns.Add("NoOfPlayers", typeof(int));
                dt.Columns.Add("DeckSize", typeof(int));

                // Fetch all game records
                var allGames = _dbContext.Games.ToList();

                foreach (var gameDetails in allGames)
                {
                    dt.Rows.Add(new object[] { gameDetails.GameId, gameDetails.NoOfPlayers, gameDetails.DeckSize });
                }

                return dt;
            }
        }


        public DataTable GetResultsByPlayerId(string playerId)
        {
            using (var context = new BlackjackDbContext())
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("ResultValue", typeof(string));
                dt.Columns.Add("GameId", typeof(string));

                var playerResults = _dbContext.Results.Where(result => result.PlayerId == playerId).ToList();

                foreach (var result in playerResults)
                {
                    dt.Rows.Add(new object[] { result.ResultValue, result.GameId }); // Removed PlayerId
                }

                return dt;
            }
        }

        public DataTable GetResultsByGameId(string gameId)
        {
            using (var context = new BlackjackDbContext())
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("ResultValue", typeof(string));
                dt.Columns.Add("GameId", typeof(string));

                var gameResults = _dbContext.Results.Where(result => result.GameId == gameId).ToList();

                foreach (var result in gameResults)
                {
                    dt.Rows.Add(new object[] { result.ResultValue, result.PlayerId }); // Changed column to PlayerId
                }

                return dt;
            }
        }


    }
}
