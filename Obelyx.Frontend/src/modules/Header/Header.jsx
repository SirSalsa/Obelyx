import { Link } from "react-router-dom"
import logo from "../../assets/obelisk_large.png"
import "./header.scss"

function Header() {
    return (
        <header>
            <div className="Header_Wrapper">
                <div className="Header_Logo">
                    <img src={logo} alt="Logo" />
                </div>
                <div className="Header_Container">
                    <Link id="header-text" to="/games">Game List</Link>
                    <Link id="header-text" to="/add-game">Add Game</Link>
                </div>
            </div>

        </header>
    );
}

export default Header;