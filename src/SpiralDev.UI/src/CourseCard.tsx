import { Link } from 'react-router'

export interface Course {
  id: number
  name: string
  description: string
}

interface CourseCardProps {
  course: Course
}

function CourseCard({ course }: CourseCardProps) {
  return (
    <div className="course-card">
      <h2>{course.name}</h2>
      <p>{course.description}</p>
      <Link to={`/c/${course.id}`}>
        <button type="button">Comenzar</button>
      </Link>
    </div>
  )
}

export default CourseCard
