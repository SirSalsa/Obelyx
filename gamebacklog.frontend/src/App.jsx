import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import './App.scss'
import Header from './modules/Header/Header'
import Footer from './modules/Footer/Footer'

// Pages
import GameList from './modules/GameList/GameList'


function App() {
  return (
    <Router>
      <div className="App">
        <Header />
        <Routes>
          {/** TODO: Implement actual page components */}
          <Route path="/" element={<div>Home Page</div>} />
          <Route path="/games" element={<GameList />} />
          <Route path="/statistics" element={<div>Statistics Page</div>} />
          <Route path="/add-game" element={<div>Add Game Page</div>} />
          <Route path="/settings" element={<div>Settings Page</div>} />
        </Routes>
        <Footer />
      </div>
    </Router>
  )
}

export default App
