import "./ImportExport.scss"

function ImportExport() {
    return (
        <main className="ImportExport">
            <h2>Title</h2>
            <select name="sort" id="sort">
                <option value="add">Add Game</option>
                {/** Add other pages later */}
                <option value="import">Import Games</option>
                <option value="export">Export Games</option>
            </select>
            {/** Render content based on selected option */}
            <div className="ImportExport_Content">

            </div>
            <div className="Add_Container">
                <input type="text" title="Title" placeholder="Enter game title" />
                <form>
                    <label htmlFor="image">Upload Cover Image:</label>
                    <input type="file" id="image" name="image" />
                </form>
                <input type="text" title="Release Year" placeholder="Enter release year" />
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
                <input type="number" title="Score" placeholder="Enter score (0-10)" min="0" max="10" />
                <input type="number" title="Hours Played" placeholder="Enter hours played" min="0" />
                <input type="checkbox" /> Rolled Credits
                <textarea title="Notes" placeholder="Enter any notes about the game"></textarea>
                <button id="add-game-button">Add Game</button>
            </div>
        </main>
    )
}

export default ImportExport;