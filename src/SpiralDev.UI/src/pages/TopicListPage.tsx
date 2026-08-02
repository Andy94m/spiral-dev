import { useEffect, useState } from 'react'
import { Link, useParams } from 'react-router'

interface Topic {
  id: number
  title: string
  order: number
}

interface CourseDetail {
  id: number
  name: string
  description: string
  topics: Topic[]
}

function TopicListPage() {
  const { courseId } = useParams()
  const [course, setCourse] = useState<CourseDetail | null>(null)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    fetch(`/api/courses/${courseId}`)
      .then((res) => {
        if (!res.ok) {
          throw new Error(`HTTP ${res.status}`)
        }
        return res.json()
      })
      .then((data: CourseDetail) => setCourse(data))
      .catch((err: Error) => setError(err.message))
  }, [courseId])

  return (
    <main className="container">
      <Link to="/">← Carreras</Link>
      <h1>{course ? course.name : '...'}</h1>
      <p className="subtitle">{course?.description}</p>

      {error && <p className="error">Error conectando con la API: {error}</p>}
      {!error && !course && <p>Cargando capítulos...</p>}

      <div className="courses">
        {course?.topics.map((topic) => (
          <Link
            key={topic.id}
            to={`/c/${courseId}/tema/${topic.id}`}
            className="course-card"
          >
            <h2>{topic.order}. {topic.title}</h2>
          </Link>
        ))}
      </div>
    </main>
  )
}

export default TopicListPage
