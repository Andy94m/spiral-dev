import { useEffect, useState } from 'react'
import CourseCard, { type Course } from '../CourseCard'

function HomePage() {
  const [courses, setCourses] = useState<Course[]>([])
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    fetch('/api/courses')
      .then((res) => {
        if (!res.ok) {
          throw new Error(`HTTP ${res.status}`)
        }
        return res.json()
      })
      .then((data: Course[]) => setCourses(data))
      .catch((err: Error) => setError(err.message))
  }, [])

  return (
    <main className="container">
      <h1>Spiral Dev 🌀</h1>
      <p className="subtitle">Aprende programando. Un concepto a la vez, siempre en espiral.</p>

      {error && <p className="error">Error conectando con la API: {error}</p>}

      {!error && courses.length === 0 && <p>Cargando carreras...</p>}

      <div className="courses">
        {courses.map((course) => (
          <CourseCard key={course.id} course={course} />
        ))}
      </div>
    </main>
  )
}

export default HomePage
