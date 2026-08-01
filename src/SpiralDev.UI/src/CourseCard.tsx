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
      <button type="button">Comenzar</button>
    </div>
  )
}

export default CourseCard
