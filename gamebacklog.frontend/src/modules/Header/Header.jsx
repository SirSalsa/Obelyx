import React from "react";
import logo from "../../assets/react.svg"
import "./header.scss"

function Header(){
    return (
        <header>
            <img src={logo} alt="Logo"/>
            <div className="Header_Container">
                <p>Home</p>
                <p>Game List</p>
                <p>Statistics</p>
                <p>Settings</p>
            </div>
        </header>
    );
}

export default Header;