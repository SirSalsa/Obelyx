import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import './App.scss'
import Header from './modules/Header/Header'
import Footer from './modules/Footer/Footer'

// Pages
import GameList from './modules/GameList/GameList'
import AddGame from './modules/AddGame/AddGame'

function App() {
  return (
    <Router>
      <div className="App">
        <Header />
        <Routes>
          {/** TODO: Implement actual page components */}
          <Route path="/games" element={<GameList />} />
          <Route path="/add-game" element={<AddGame />} />
        </Routes>
        <Footer />
      </div>
    </Router>
  )
}

export default App
