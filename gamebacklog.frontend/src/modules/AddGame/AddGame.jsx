import "./AddGame.scss"
import { useState, useRef } from 'react';

function AddGame() {
    const [image, setImage] = useState(null);
    const fileInputRef = useRef(null);

    const handleImageChange = (e) => {
        if (e.target.files && e.target.files[0]) {
            setImage(URL.createObjectURL(e.target.files[0]));
        }
    };

    const handleUploadButtonClick = () => {
        fileInputRef.current.click();
    };

    return (
        <main className="AddGame_Main">
            <h2>Add Game</h2>
            <div className="Add_Container">
                <div className="Add_Left">
                    <h3>Game Information</h3>
                    <input type="text" title="Title" placeholder="Enter game title" />
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
                    <label>
                        <input type="checkbox" />
                        Rolled Credits
                    </label>
                    <textarea title="Notes" placeholder="Enter any notes about the game"></textarea>
                </div>
                <div className="Add_Right">
                    <h3>Upload Cover Image</h3>
                    <div id="image-preview">
                        {image ? (
                            <img src={image} alt="Preview" />
                        ) : (
                            <span>No image selected</span>
                        )}
                    </div>
                    <input
                        type="file"
                        id="image"
                        name="image"
                        onChange={handleImageChange}
                        ref={fileInputRef}
                        className="hidden-file-input"
                    />
                    <button type="button" id="upload-image-button" onClick={handleUploadButtonClick}>
                        Choose Image
                    </button>
                </div>
                <div className="Add_Bottom">
                    <button id="clear-game-button">Clear</button>
                    <button id="add-game-button">Add Game</button>
                </div>
            </div>
        </main>
    )
}

export default AddGame;