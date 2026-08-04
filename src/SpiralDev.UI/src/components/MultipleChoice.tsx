import { useState } from 'react'

interface MultipleChoiceProps {
  exercise: {
    question?: string
    options?: string
  }
}

function MultipleChoice({ exercise }: MultipleChoiceProps) {
  const [selected, setSelected] = useState<number | null>(null)
  const options = (exercise.options ?? '').split(';').filter(Boolean)

  return (
    <div className="multiple-choice">
      {exercise.question && (
        <p className="challenge-question">{exercise.question}</p>
      )}
      <div className="options">
        {options.map((option, i) => (
          <button
            key={i}
            className={`option ${selected === i ? 'selected' : ''}`}
            onClick={() => setSelected(i)}
          >
            {String.fromCharCode(97 + i)}) {option}
          </button>
        ))}
      </div>
      {selected !== null && (
        <p className="challenge-hint">Respondiste la opción {String.fromCharCode(97 + selected)}.</p>
      )}
    </div>
  )
}

export default MultipleChoice
