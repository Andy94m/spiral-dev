import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      // Cualquier llamada a /api/* desde el frontend
      // se redirige al backend .NET en localhost:5190
      '/api': {
        target: 'http://localhost:5190',
        changeOrigin: true,
      },
    },
  },
})
