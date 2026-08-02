import { useEffect, useState } from 'react'
import { Link, useParams } from 'react-router'

interface Exercise {
  id: number
  type: string
  question: string
}

interface LessonDetail {
  id: number
  title: string
  order: number
  contentMarkdown: string
  exercises: Exercise[]
}

function LessonPage() {
  const { courseId, topicId, lessonId } = useParams()
  const [lesson, setLesson] = useState<LessonDetail | null>(null)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    fetch(`/api/lessons/${lessonId}`)
      .then((res) => {
        if (!res.ok) {
          throw new Error(`HTTP ${res.status}`)
        }
        return res.json()
      })
      .then((data: LessonDetail) => setLesson(data))
      .catch((err: Error) => setError(err.message))
  }, [lessonId])

  return (
    <main className="container">
      <Link to={`/c/${courseId}/tema/${topicId}`}>← Capítulo {topicId}</Link>
      <h1>{lesson ? lesson.title : '...'}</h1>

      {error && <p className="error">Error conectando con la API: {error}</p>}
      {!error && !lesson && <p>Cargando lección...</p>}

      {lesson && (
        <article className="lesson-content">
          {/* TODO (próximo paso): renderizar Markdown con react-markdown */}
          <pre>{lesson.contentMarkdown}</pre>
        </article>
      )}
    </main>
  )
}

export default LessonPage
