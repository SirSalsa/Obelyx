import './GameList.scss';

function GameDetails({ title, imgSrc, score, hoursPlayed, rolledCredits, notes, onClose }) {
    return (
        <div className="GameDetails">
            {/* Left side */}
            <div className="GameDetailsImage">
                <img src={imgSrc} alt={`${title} Cover`} />
            </div>

            {/* Right side */}
            <div className="GameDetailsInfo">
                <h2>{title}</h2>
                <div className="GameDetailsRating">{score} ★</div>
                <p>{hoursPlayed ? `${hoursPlayed} hours played` : 'No data yet'}</p>

                <label>
                    <input type="checkbox" checked={rolledCredits} readOnly />
                    Rolled Credits
                </label>

                <textarea
                    name="notes"
                    id="notes"
                    value={notes || ''}
                    placeholder="Add notes..."
                    readOnly
                ></textarea>
            </div>

            {/* Buttons */}
            <div className="GameDetailsButtons">
                <button id="archive-game">Archive</button>
                <button id="save-game">Save</button>
                <button id="close-details" onClick={onClose}>Close</button>
            </div>
        </div>
    );
}

export default GameDetails;
