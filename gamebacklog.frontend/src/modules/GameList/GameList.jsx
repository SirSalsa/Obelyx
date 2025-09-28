import './GameList.scss';
import test from '../../assets/test.jpg';
import GameCard from './GameCard';
import { useState } from 'react';

function GameList() {

  const [games, setGames] = useState([]);

  const handleSearch = async () => {
    try {
      const params = new URLSearchParams({
        page: 1,
        pageSize: 100
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
    // Clear all filters (reset state variables if any)
    // For now, just clear the game list
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
          <option value="title">Title</option>
          <option value="release-date">Release Date</option>
          <option value="rating">Rating</option>
          <option value="time-played">Time Played</option>
        </select>
        <h4>Advanced Filters</h4>
        <label>
          <input type="checkbox" />
          Rolled Credits
        </label>
        <label>
          <input type="checkbox" />
          Favorites Only
        </label>
        <label>
          <input type="checkbox" />
          Completed 100%
        </label>
        <div id="ScoreFilter">
          <label>Score</label>
          <input type="range" min="0" max="10" />
        </div>
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
            games.map((game) => (
              <GameCard
                key={game.id}
                title={game.title}
                imgSrc={game.imagePath || test}
              />
            ))
          )}
        </div>
      </div>
    </main>
  );
}

export default GameList;
