import "./AddGame.scss"
import { useState, useRef, useEffect } from 'react';

function AddGame() {
    const [image, setImage] = useState(null);
    const [imagePreview, setImagePreview] = useState(null);
    const fileInputRef = useRef(null);

    const handleImageChange = (e) => {
        if (e.target.files && e.target.files[0]) {
            const file = e.target.files[0];
            setImage(file); // store File object
            setImagePreview(URL.createObjectURL(file)); // create and store URL for preview
        }
    };

    const handleUploadButtonClick = () => {
        fileInputRef.current.click();
    };

    const handleClear = async () => {
        setImage(null);
        setImagePreview(null);
        fileInputRef.current.value = null;
        document.querySelector('input[title="Title"]').value = "";
        document.querySelector('select[name="status"]').value = "";
        document.querySelector('input[title="Release Year"]').value = "";
        document.querySelector('input[title="Score"]').value = "";
        document.querySelector('input[title="Hours Played"]').value = "";
        document.querySelector('input[type="checkbox"]').checked = false;
        document.querySelector('textarea[title="Notes"]').value = "";
    };

    const handleAddGame = async () => {
        try {
            const gameData = {
                title: document.querySelector('input[title="Title"]').value,
                status: document.querySelector('select[name="status"]').value,
                releaseYear: parseInt(document.querySelector('input[title="Release Year"]').value) || null,
                score: parseInt(document.querySelector('input[title="Score"]').value) || null,
                hoursPlayed: parseInt(document.querySelector('input[title="Hours Played"]').value) || null,
                rolledCredits: document.querySelector('input[type="checkbox"]').checked,
                notes: document.querySelector('textarea[title="Notes"]').value
            };

            const formData = new FormData();
            formData.append("gameData", JSON.stringify(gameData));

            if (image) {
                formData.append("coverImage", image);
            }

            const res = await fetch("https://localhost:7125/api/games", {
                method: "POST",
                body: formData
            });

            if (!res.ok) throw new Error("Failed to add game");

            const data = await res.json();
            console.log("Game added successfully:", data);

            // Clear form after successful submission
            await handleClear();
        } catch (err) {
            console.error("Error adding game:", err);
        }
    };

    // Clean up the object URL to avoid memory leaks
    useEffect(() => {
        return () => {
            if (imagePreview) {
                URL.revokeObjectURL(imagePreview);
            }
        };
    }, [imagePreview]);

    return (
        <main className="AddGame_Main">
            <h2>Add Game</h2>
            <div className="Add_Container">
                <div className="Add_Left">
                    <h3>Game Information</h3>
                    <input type="text" title="Title" placeholder="Enter game title" />
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
                    <input type="text" title="Release Year" placeholder="Enter release year" />
                    <input type="number" title="Score" placeholder="Enter score (0-5)" min="0" max="5" />
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
                        {imagePreview ? (
                            <img src={imagePreview} alt="Preview" />
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
                    <button id="clear-game-button" onClick={handleClear}>Clear</button>
                    <button id="add-game-button" onClick={handleAddGame}>Add Game</button>
                </div>
            </div>
        </main>
    )
}

export default AddGame;