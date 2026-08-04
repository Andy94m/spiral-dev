import { useEffect, useState } from 'react'
import { Link, useParams } from 'react-router'
import Markdown from 'react-markdown'
import CodePlayground from '../components/CodePlayground'
import MultipleChoice from '../components/MultipleChoice'

interface Exercise {
  id: number
  order: number
  type: string
  title: string
  statement: string
  question?: string
  options?: string
  starterCode?: string
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
        <>
          <article className="lesson-content">
            <Markdown>{lesson.contentMarkdown}</Markdown>
          </article>

          {lesson.exercises.length > 0 && (
            <section className="lesson-challenges">
              <h2>🧩 Desafíos</h2>
              {lesson.exercises.map((ex) => (
                <div key={ex.id} className="challenge-card">
                  <h3>{ex.title}</h3>
                  {ex.statement && <p className="challenge-statement">{ex.statement}</p>}

                  {ex.type === 'MultipleChoice' && (
                    <MultipleChoice exercise={ex} />
                  )}

                  {ex.type === 'CodeWriting' && (
                    <>
                      <p className="challenge-question">{ex.question}</p>
                      <CodePlayground starterCode={ex.starterCode ?? ''} />
                    </>
                  )}
                </div>
              ))}
            </section>
          )}
        </>
      )}
    </main>
  )
}

export default LessonPage
