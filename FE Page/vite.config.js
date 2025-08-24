import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  base: '/CarDealership_FE/',
  build: {
    outDir: 'docs', // Change from 'dist' to 'docs'
    emptyOutDir: true // Optional: cleans the docs folder before build
  }
})