import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from './router'
import axios from 'axios'
import BaseButton from './components/BaseButton.vue'
import BaseInput from './components/BaseInput.vue'

// Add auth interceptor
axios.interceptors.request.use(config => {
  const token = localStorage.getItem('token')
  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config
})

// Handle 401
axios.interceptors.response.use(response => response, error => {
  if (error.response && error.response.status === 401) {
    localStorage.removeItem('token')
    if (window.location.pathname !== '/login') {
      window.location.href = '/login'
    }
  }
  return Promise.reject(error)
})

const app = createApp(App)

app.component('BaseButton', BaseButton)
app.component('BaseInput', BaseInput)

app.use(router)

app.mount('#app')
