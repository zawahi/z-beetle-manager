import { createRouter, createWebHistory } from 'vue-router'
import Dashboard from '../views/Dashboard.vue'
import BeetleList from '../views/BeetleList.vue'
import BreedingList from '../views/BreedingList.vue'
import BeetleForm from '../views/BeetleForm.vue'
import BeetleDetail from '../views/BeetleDetail.vue'
import BreedingForm from '../views/BreedingForm.vue'
import Login from '../views/Login.vue'
import Register from '../views/Register.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: '/login', name: 'login', component: Login, meta: { public: true } },
    { path: '/register', name: 'register', component: Register, meta: { public: true } },
    { path: '/', name: 'dashboard', component: Dashboard },
    { path: '/beetles', name: 'beetles', component: BeetleList },
    { path: '/beetle/new', name: 'beetle-new', component: BeetleForm },
    { path: '/beetle/:id', name: 'beetle-detail', component: BeetleDetail },
    { path: '/breeding', name: 'breeding', component: BreedingList },
    { path: '/breeding/new', name: 'breeding-new', component: BreedingForm }
  ]
})

router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token')
  if (!to.meta.public && !token) {
    next('/login')
  } else if (to.meta.public && token) {
    next('/')
  } else {
    next()
  }
})

export default router
