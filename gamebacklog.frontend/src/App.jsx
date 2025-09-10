import { useState } from 'react'
import './App.scss'
import Header from './modules/Header/Header'
import MainTemp from './modules/MainTemp/MainTemp'

function App() {
  return (
    <div className="App">
      <Header/>
      <MainTemp/>
    </div>
  )
}

export default App
