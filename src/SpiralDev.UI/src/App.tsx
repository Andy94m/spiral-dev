import { Routes, Route } from 'react-router'
import HomePage from './pages/HomePage'
import TopicListPage from './pages/TopicListPage'
import LessonListPage from './pages/LessonListPage'
import LessonPage from './pages/LessonPage'

function App() {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/c/:courseId" element={<TopicListPage />} />
      <Route path="/c/:courseId/tema/:topicId" element={<LessonListPage />} />
      <Route path="/c/:courseId/tema/:topicId/leccion/:lessonId" element={<LessonPage />} />
    </Routes>
  )
}

export default App
