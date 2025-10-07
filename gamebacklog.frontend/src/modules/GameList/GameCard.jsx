import './GameList.scss';

function GameCard({ title, imgSrc, score, onClick }) {
  return (
    <div className="GameCard" onClick={onClick}>
      <div className="GameCardImageContainer">
        <img src={imgSrc} alt="Game Cover" />
        {/* Only show score if it's provided */}
        {score != null && (
          <div className="GameCardRating">{score} ★</div>
        )}
      </div>
      <h3>{title}</h3>
    </div>
  );
}

export default GameCard;