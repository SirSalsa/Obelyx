import './GameList.scss';

function GameCard({ title, imgSrc }) {
  return (
    <div className="GameCard">
        <div className="GameCardImageContainer">
            <img src={imgSrc} alt="Game Cover" />
            <div className="GameCardRating">3 ★</div>
        </div>
        <h3>{title}</h3>
    </div>
  );
}

export default GameCard;