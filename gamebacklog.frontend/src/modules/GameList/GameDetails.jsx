import './GameList.scss';

function GameDetails({ title, imgSrc, score, hoursPlayed, rolledCredits, notes, onClose }) {
    return (
        <div className="GameDetails">
            <div className="GameDetailsTop">
                {/* Left side */}
                <div className="GameDetailsImage">
                    <img src={imgSrc} alt={`${title} Cover`} />
                </div>

                {/* Right side */}
                <div className="GameDetailsInfo">
                    <h2>{title}</h2>

                    {/* Game details form on the left side*/}
                    <div className="GameDetailsForm">

                        <article>
                            <label htmlFor="score">Score:</label>
                            <select name="score" id="score" value={score || ''}>
                                <option value="" disabled selected>Unscored</option>
                                <option value="1">★</option>
                                <option value="2">★★</option>
                                <option value="3">★★★</option>
                                <option value="4">★★★★</option>
                                <option value="5">★★★★★</option>
                            </select>
                        </article>

                        <article>
                            <label htmlFor="hours-played">Hours Played:</label>
                            <input type="number" name="hours-played" id="hours-played" value={hoursPlayed || 0} />
                        </article>

                        <article>
                            <label htmlFor="start-date">Start Date:</label>
                            <input type="date" id="start-date" />
                        </article>

                        <article>
                            <label htmlFor="end-date">End Date:</label>
                            <input type="date" id="end-date" />
                        </article>
                    </div>

                    {/* Checkboxes on the right side */}
                    <div className='GameDetailsCheckboxes'>
                        <label>
                            <input type="checkbox" checked={rolledCredits} readOnly />
                            Rolled Credits
                        </label>
                    </div>
                </div>
            </div>

            {/* Notes section */}
            <div>
                <label htmlFor="notes">Notes:</label>
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
        </div >
    );
}

export default GameDetails;
