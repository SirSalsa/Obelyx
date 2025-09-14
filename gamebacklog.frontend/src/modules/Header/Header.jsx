import React from "react";
import logo from "../../assets/react.svg"
import "./header.scss"

function Header(){
    return (
        <header>
            <div className="Header_Wrapper">
                <div className="Header_Logo">
                <img src={logo} alt="Logo"/>
                </div>
                <div className="Header_Container">
                    <p>Home</p>
                    <p>Game List</p>
                    <p>Statistics</p>
                    <p>Settings</p>
                </div>
            </div>
            
        </header>
    );
}

export default Header;