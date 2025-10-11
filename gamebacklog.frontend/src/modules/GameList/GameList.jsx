import './GameList.scss';
import placeholder from '../../assets/placeholder.png';
import GameCard from './GameCard';
import { useState } from 'react';
import GameDetails from './GameDetails';

function GameList() {

  const [selectedGame, setSelectedGame] = useState(null);
  const [games, setGames] = useState([]);

  const handleSearch = async () => {
    try {
      const params = new URLSearchParams({
        page: 1,
        pageSize: 100,
        statusFilter: document.getElementById('status').value, // All if empty
        sortBy: document.getElementById('sort').value || 'title',
        minScore: document.getElementById('min-score').value || 0,
        rolledCreditsOnly: document.querySelector('input[type="checkbox"]').checked
      });

      const res = await fetch(`https://localhost:7125/api/games/interval?${params}`);
      if (!res.ok) throw new Error("Failed to fetch games");

      const data = await res.json();
      setGames(data);
    } catch (err) {
      console.error("Error fetching games:", err);
    }
  };

  const handleUpdate = async (updatedGame) => {
    try {
      const res = await fetch(`https://localhost:7125/api/games/`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          id: updatedGame.id,
          title: updatedGame.title || null,
          releaseYear: updatedGame.releaseYear || null,
          backlogStatus: updatedGame.backlogStatus || "",
          score: updatedGame.score,
          hoursPlayed: updatedGame.hoursPlayed,
          rolledCredits: updatedGame.rolledCredits,
        }),
      });

      if (!res.ok) throw new Error("Failed to update game");

      const saved = await res.json();

      // Update the local games list immediately (optimistic UI)
      setGames((prev) =>
        prev.map((g) => (g.id === saved.id ? { ...g, ...saved } : g))
      );

      // Optionally close the details view
      setSelectedGame(saved);

    } catch (err) {
      console.error("Error updating game:", err);
    }
  };

  const handleClear = () => {
    document.querySelector('input[placeholder="Game Title..."]').value = "";
    document.getElementById('status').value = "";
    document.getElementById('sort').value = "";
    document.getElementById('min-score').value = "";
    document.querySelector('input[type="checkbox"]').checked = false;
    selectedGame && setSelectedGame(null);
    setGames([]);
  };

  return (
    <main>
      {/* Left side */}
      <div className="Sidebar">
        <h2>Filters</h2>
        <input type="text" placeholder="Game Title..." />
        <select name="status" id="status">
          <option value="" disabled selected>Status</option>
          <option value="all">All</option>
          <option value="wishlist">Wishlist</option>
          <option value="planned">Planned</option>
          <option value="playing">Playing</option>
          <option value="paused">Paused</option>
          <option value="completed">Completed</option>
          <option value="dropped">Dropped</option>
        </select>
        <select name="sort" id="sort">
          <option value="" disabled selected>Sort By</option>
          <option value="Title">Title</option>
          <option value="ReleaseDate">Release Date</option>
          <option value="Score">Score</option>
          <option value="HoursPlayed">Time Played</option>
        </select>

        <h4>Advanced Filters</h4>
        <select name="min-score" id="min-score">
          <option value="" disabled selected>Minimum Score</option>
          <option value="1">★+</option>
          <option value="2">★★+</option>
          <option value="3">★★★+</option>
          <option value="4">★★★★+</option>
          <option value="5">★★★★★</option>
        </select>
        <label>
          <input type="checkbox" />
          Rolled Credits
        </label>
        {/*
        <label>
          <input type="checkbox" />
          Favorites Only
        </label>
        <label>
          <input type="checkbox" />
          Completed 100%
        </label>
        */}
        <hr />
        <div className="ButtonGroup">
          <button id="clear-filters" onClick={handleClear}>Clear Filters</button>
          <button id="search" onClick={handleSearch}>Search</button>
        </div>
      </div>
      {/* Right side */}
      <div className="GameList">
        <h2>My Games</h2>

        {/* Show game details if a game is selected, otherwise show the game gallery */}
        {selectedGame ? (
          <div>
            <GameDetails
              id={selectedGame.id}
              title={selectedGame.title}
              imgSrc={
                selectedGame.imagePath
                  ? `https://localhost:7125${selectedGame.imagePath}`
                  : placeholder
              }
              score={selectedGame.score}
              hoursPlayed={selectedGame.hoursPlayed}
              rolledCredits={selectedGame.rolledCredits}
              notes={selectedGame.notes}
              onUpdate={handleUpdate}
              onClose={() => setSelectedGame(null)}
            />
          </div>
        ) : (
          <div className="GameGallery">
            {games.length === 0 ? (
              <p>No games found. Try searching!</p>
            ) : (
              games.map((game) => (
                <GameCard
                  key={game.id}
                  title={game.title}
                  score={game.score}
                  imgSrc={
                    game.imagePath
                      ? `https://localhost:7125${game.imagePath}`
                      : placeholder
                  }
                  onClick={() => setSelectedGame(game)}
                />
              ))
            )}
          </div>
        )}
      </div>

    </main>
  );
}

export default GameList;