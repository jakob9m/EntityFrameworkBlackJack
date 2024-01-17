using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class Controller
    {

        private readonly DataAccess _dataAccess;

        public Controller()
        {
            _dataAccess = new DataAccess();
        }

        public void CreatePlayer(string name)
        {
            
            string playerId;
            do
            {
                playerId = GenerateRandomPlayerId();
            } while (_dataAccess.DoesPlayerExist(playerId)); // Keep generating new IDs until we find one that doesn't exist

            _dataAccess.AddPlayer(playerId, name);
        }

        private string GenerateRandomPlayerId()
        {
            
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public DataTable GetAllPlayers()
        {
            return _dataAccess.GetAllPlayers();
        }

        public DataTable GetAllGames()
        {
            return _dataAccess.GetAllGames();
        }

        public void RemovePlayer(string playerId)
        {
            // Assuming you have an instance of your DataAccess class
            _dataAccess.DeletePlayer(playerId);
        }
        public void RemoveResult(string gameId, string playerId)
        {
            _dataAccess.DeleteResult(gameId, playerId);
        } 

        public void removeGame(string gameId)
        {
            _dataAccess.DeleteGame(gameId);
        }
        

        public void UpdatePlayerName(string playerId, string newName)
        {
            _dataAccess.UpdatePlayerName(playerId, newName);
        }

        public String CreateGame(int noOfPlayers, int deckSize)
        {
            string gameId = GenerateNextGameId();
            _dataAccess.AddGame(gameId, noOfPlayers, deckSize);
            return gameId;
        }

        private string GenerateNextGameId()
        {
            int lastGameNumber = _dataAccess.GetLastGameNumber();
            return "G" + (lastGameNumber + 1).ToString();
        }

        public void CreateResult(string playerId, string gameId, string resultValue)
        {
            _dataAccess.AddResult(playerId, gameId, resultValue);
        }

        public DataTable GetGameFromResult(string gameId)
        {
            return _dataAccess.GetGameDetails(gameId);
        }

        public DataTable GetResultsForPlayer(string playerId)
        {
            return _dataAccess.GetResultsByPlayerId(playerId);
        }

        public DataTable getResultFromGame(string gameId)
        {
            return _dataAccess.GetResultsByGameId(gameId);
        }

    }
}
