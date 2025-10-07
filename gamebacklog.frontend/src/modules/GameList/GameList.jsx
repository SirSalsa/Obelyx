import './GameList.scss';
import placeholder from '../../assets/placeholder.png';
import GameCard from './GameCard';
import { useState } from 'react';

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
        <div className="GameGallery">
          {games.length === 0 ? (
            <p>No games found. Try searching!</p>
          ) : (
            games.map(game => (
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
      </div>
      {selectedGame && (
        <div
          style={{
            marginTop: "30px",
            padding: "20px",
            background: "#222",
            color: "white",
            borderRadius: "10px",
            width: "400px",
          }}
        >
          <h2>{selectedGame.title}</h2>
          <p>This is your preview area. Add whatever info you want here.</p>
          <button
            onClick={() => setSelectedGame(null)}
            style={{
              marginTop: "10px",
              padding: "6px 12px",
              background: "#555",
              color: "white",
              border: "none",
              borderRadius: "5px",
              cursor: "pointer",
            }}
          >
            Close
          </button>
        </div>
      )}
    </main>
  );
}

export default GameList;
