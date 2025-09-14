import { useState } from 'react'
import './App.scss'
import Header from './modules/Header/Header'
import Footer from './modules/Footer/Footer'
import GameList from './modules/GameList/GameList'

function App() {
  return (
    <div className="App">
      <Header/>
      <GameList/>
      <Footer/>
    </div>
  )
}

export default App
