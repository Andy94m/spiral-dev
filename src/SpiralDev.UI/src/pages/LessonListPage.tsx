import { useEffect, useState } from 'react'
import { Link, useParams } from 'react-router'

interface Lesson {
  id: number
  title: string
  order: number
}

interface TopicDetail {
  id: number
  title: string
  order: number
  lessons: Lesson[]
}

function LessonListPage() {
  const { courseId, topicId } = useParams()
  const [topic, setTopic] = useState<TopicDetail | null>(null)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    fetch(`/api/topics/${topicId}`)
      .then((res) => {
        if (!res.ok) {
          throw new Error(`HTTP ${res.status}`)
        }
        return res.json()
      })
      .then((data: TopicDetail) => setTopic(data))
      .catch((err: Error) => setError(err.message))
  }, [topicId])

  return (
    <main className="container">
      <Link to={`/c/${courseId}`}>← Capítulos</Link>
      <h1>{topic ? topic.title : '...'}</h1>

      {error && <p className="error">Error conectando con la API: {error}</p>}
      {!error && !topic && <p>Cargando lecciones...</p>}

      <div className="courses">
        {topic?.lessons.map((lesson) => (
          <Link
            key={lesson.id}
            to={`/c/${courseId}/tema/${topicId}/leccion/${lesson.id}`}
            className="course-card"
          >
            <h2>{lesson.order}. {lesson.title}</h2>
            <p>Leer lección →</p>
          </Link>
        ))}
      </div>
    </main>
  )
}

export default LessonListPage
