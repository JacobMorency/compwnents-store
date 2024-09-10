/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './Views/**/*.cshtml',
    './wwwroot/**/*.js'
  ],
  theme: {
    extend: {},
  },
  daisyui: {
    themes: ["light", "dark", "retro", "nord"],    
  },
  plugins: [require('daisyui')],
}


