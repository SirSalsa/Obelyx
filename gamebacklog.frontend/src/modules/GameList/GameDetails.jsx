import { useState } from 'react';
import './GameList.scss';

function GameDetails({ id, title, imgSrc, score, hoursPlayed, rolledCredits, notes, onUpdate, onClose }) {
    // Local state for editable fields
    const [currentScore, setCurrentScore] = useState(score || '');
    const [currentHours, setCurrentHours] = useState(hoursPlayed || 0);
    const [currentNotes, setCurrentNotes] = useState(notes || '');
    const [creditsRolled, setCreditsRolled] = useState(rolledCredits || false);
    const [startDate, setStartDate] = useState('');
    const [endDate, setEndDate] = useState('');

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

                    {/* Game details form */}
                    <div className="GameDetailsForm">
                        <article>
                            <label htmlFor="score">Score:</label>
                            <select
                                name="score"
                                id="score"
                                value={currentScore}
                                onChange={(e) => setCurrentScore(e.target.value)}
                            >
                                <option value="">Unscored</option>
                                <option value="1">★</option>
                                <option value="2">★★</option>
                                <option value="3">★★★</option>
                                <option value="4">★★★★</option>
                                <option value="5">★★★★★</option>
                            </select>
                        </article>

                        <article>
                            <label htmlFor="hours-played">Hours Played:</label>
                            <input
                                type="number"
                                name="hours-played"
                                id="hours-played"
                                value={currentHours}
                                onChange={(e) => setCurrentHours(e.target.value)}
                            />
                        </article>

                        <article>
                            <label htmlFor="start-date">Start Date:</label>
                            <input
                                type="date"
                                id="start-date"
                                value={startDate}
                                onChange={(e) => setStartDate(e.target.value)}
                            />
                        </article>

                        <article>
                            <label htmlFor="end-date">End Date:</label>
                            <input
                                type="date"
                                id="end-date"
                                value={endDate}
                                onChange={(e) => setEndDate(e.target.value)}
                            />
                        </article>
                    </div>

                    {/* Checkboxes */}
                    <div className='GameDetailsCheckboxes'>
                        <label>
                            <input
                                type="checkbox"
                                checked={creditsRolled}
                                onChange={(e) => setCreditsRolled(e.target.checked)}
                            />
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
                    value={currentNotes}
                    onChange={(e) => setCurrentNotes(e.target.value)}
                    placeholder="Add notes..."
                ></textarea>
            </div>

            {/* Buttons */}
            <div className="GameDetailsButtons">
                <button id="archive-game">Archive</button>
                <button
                    id="save-game"
                    onClick={() => {
                        onUpdate({
                            id: id.toString(), // backend expects string
                            score: currentScore ? parseInt(currentScore, 10) : null,
                            hoursPlayed: currentHours ? parseInt(currentHours, 10) : null,
                            rolledCredits: creditsRolled,
                            title, // optional
                            // backlogStatus, releaseYear could be added later
                        });
                    }}
                >
                    Save
                </button>
                <button id="close-details" onClick={onClose}>Close</button>
            </div>
        </div >
    );
}

export default GameDetails;
