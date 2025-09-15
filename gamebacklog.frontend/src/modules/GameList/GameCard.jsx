import './GameList.scss';

function GameCard({ title, imgSrc }) {
  return (
    <div className="GameCard">
        <img src={imgSrc} alt="Game Cover" />
        <h3>{title}</h3>
    </div>
  );
}

export default GameCard;