import { createRouter, createWebHistory } from 'vue-router'
import UserPage from './UserPage.vue'

const routes = [
  { path: '/users/:id', name: 'UserPage', component: UserPage}
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
