import './GameList.scss';
import test from '../../assets/test.jpg';
import GameCard from './GameCard';

function GameList() {
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
                <button id="clear-filters">Clear Filters</button>
                <button id="search">Search</button>
            </div>
        </div>
        {/* Right side */}
        <div className="GameList">
          <h2>My Games</h2>
          <div className="GameGallery">
            {/* Example Game Item */}
            <GameCard title="The wonderous adventures of baby dragon and his friends 2: Electric boogaloo" imgSrc={test} />
            <GameCard title="Game Title" imgSrc={test} />
            <GameCard title="Game Title" imgSrc={test} />
            <GameCard title="Game Title" imgSrc={test} />
            <GameCard title="Game Title" imgSrc={test} />
            <GameCard title="Game Title" imgSrc={test} />
            <GameCard title="Game Title" imgSrc={test} />
          </div>
        </div>
    </main>
  );
}

export default GameList;
