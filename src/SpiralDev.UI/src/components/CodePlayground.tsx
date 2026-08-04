import { useState } from 'react'
import Editor from '@monaco-editor/react'

interface CodePlaygroundProps {
  starterCode: string
}

interface RunResult {
  success: boolean
  stdout: string
  stderr: string
  exitCode: number | null
  status: string
}

function CodePlayground({ starterCode }: CodePlaygroundProps) {
  const [code, setCode] = useState(starterCode)
  const [stdin, setStdin] = useState('')
  const [result, setResult] = useState<RunResult | null>(null)
  const [running, setRunning] = useState(false)

  async function runCode() {
    setRunning(true)
    setResult(null)
    try {
      const res = await fetch('/api/execute', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ language: 'c', code, stdin })
      })
      const data: RunResult = await res.json()
      setResult(data)
    } catch {
      setResult({
        success: false,
        stdout: '',
        stderr: 'No se pudo conectar con el servidor de ejecución.',
        exitCode: null,
        status: 'Error'
      })
    } finally {
      setRunning(false)
    }
  }

  return (
    <div className="code-playground">
      <Editor
        height="220px"
        defaultLanguage="c"
        theme="vs-dark"
        value={code}
        onChange={(value) => setCode(value ?? '')}
        options={{ fontSize: 14, minimap: { enabled: false } }}
      />

      <div className="playground-controls">
        <label>
          Entrada (stdin):
          <input
            type="text"
            value={stdin}
            onChange={(e) => setStdin(e.target.value)}
            placeholder="ej: 5 3"
          />
        </label>
        <button onClick={runCode} disabled={running}>
          {running ? 'Ejecutando...' : '▶ Ejecutar'}
        </button>
      </div>

      {result && (
        <div className={`run-result ${result.success ? 'ok' : 'fail'}`}>
          <strong>{result.success ? '✅ Correcto' : `❌ ${result.status}`}</strong>
          {result.stdout && <pre className="output">{result.stdout}</pre>}
          {result.stderr && <pre className="error">{result.stderr}</pre>}
        </div>
      )}
    </div>
  )
}

export default CodePlayground
