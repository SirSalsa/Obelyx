import './GameList.scss';
import test from '../../assets/test.jpg';

function GameList() {
  return (
    <main>
        {/* Left side */}
        <div className="Sidebar">
            <h2>Filters</h2>
            <input type="text" placeholder="Game Title..." />
            <h4>Status</h4>
            <select name="status" id="status">
                <option value="all">All</option>
                <option value="wishlist">Wishlist</option>
                <option value="planned">Planned</option>
                <option value="playing">Playing</option>
                <option value="paused">Paused</option>
                <option value="completed">Completed</option>
                <option value="dropped">Dropped</option>
            </select>
            <h4>Sort By</h4>
            <select name="sort" id="sort">
                <option value="title">Title</option>
                <option value="release-date">Release Date</option>
                <option value="rating">Rating</option>
                <option value="time-played">Time Played</option>
            </select>
            <h3>Advanced Filters</h3>
            <label>
                Rolled Credits
                <input type="checkbox" />
            </label>
            <button>Clear Filters</button>
            <button>Search</button>
        </div>
        {/* Right side */}
        <div className="GameList">
          <h2>My Games</h2>
          <div className="GameContainer">
            {/* Example Game Item */}
            <img src={test} alt="Game Title" />
            <img src={test} alt="Game Title" />
            <img src={test} alt="Game Title" />
            <img src={test} alt="Game Title" />
            <img src={test} alt="Game Title" />
            <img src={test} alt="Game Title" />
            <img src={test} alt="Game Title" />
          </div>
        </div>
    </main>
  );
}

export default GameList;
