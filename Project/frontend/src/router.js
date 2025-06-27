import { createRouter, createWebHistory } from 'vue-router'
import UserPage from './UserPage.vue'
import UserList from './UserList.vue'
import MetricsPage from './MetricsPage.vue'
import MainPage from './MainPage.vue'
import MissingPage from './MissingPage.vue'

const routes = [
  { path: '/', name: 'MainPage', component: MainPage},
  { path: '/users/:id', name: 'UserPage', component: UserPage},
  { path: '/users', name: 'UserList', component: UserList},
  { path: '/metrics', name: 'MetricsPage', component: MetricsPage},
  { path: '/:pathMatch(.*)*', name: 'MissingPage', component: MissingPage}
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
