import { useState } from 'react'
import './App.scss'
import Header from './modules/Header/Header'
import MainTemp from './modules/MainTemp/MainTemp'
import Footer from './modules/Footer/Footer'

function App() {
  return (
    <div className="App">
      <Header/>
      <MainTemp/>
      <Footer/>
    </div>
  )
}

export default App
