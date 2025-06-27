<template>
  <div class="user-list-container">
    <h2 class="title">Lista de Utilizadores</h2>
    <router-link to="/" class="back-home-button">← Voltar à página inicial</router-link>

    <div class="prefix-search">
      <label for="prefixInput">Procurar utilizador:</label>
      <input id="prefixInput" v-model="prefix" @input="searchUsersByPrefix" placeholder="Escreve o início do nome" />
      <ul v-if="prefixUsers.length">
        <li v-for="user in prefixUsers" :key="user.id"><a :href="`/users/${user.id}`">{{ user.id }}</a> - {{ user.name }}</li>
      </ul>
    </div>

    <div class="pagination">
      <button @click="goBack10" :disabled="page < 10">« -10</button>
      <button @click="prevPage" :disabled="page === 0">← Anterior</button>

      <label>
        Página:
        <input type="number" min="1" v-model.number="inputPage" @keyup.enter="goToPage" />
      </label>

      <button @click="nextPage">Próxima →</button>
      <button @click="goForward10">+10 »</button>
    </div>

    <div v-if="loading" class="status">
      <p>A carregar utilizadores...</p>
    </div>

    <div v-else-if="error" class="status error">
      <p>Erro: {{ error }}</p>
    </div>

    <div v-else>
      <table v-if="users.length > 0" class="user-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>Código País</th>
            <th>Ativo?</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="user in users" :key="user.id">
            <td><router-link :to="`/users/${user.id}`">{{ user.id }}</router-link></td>
            <td>{{ user.name }}</td>
            <td>{{ user.country_code }}</td>
            <td><span :class="user.is_active ? 'active' : 'inactive'">{{ user.is_active ? 'Sim' : 'Não' }}</span></td>
          </tr>
        </tbody>
      </table>

      <div class="status" v-else>
        <p>Nenhum utilizador encontrado.</p>
      </div>
    </div>
  </div>
</template>


<script>
import axios from 'axios'

export default {
  name: 'UserList',
  data() {
    return {
      users: [],
      loading: false,
      error: null,
      page: 0,
      inputPage: 1,
      prefix: '',
      prefixUsers: []
    }
  },
  mounted() {
    this.fetchUsers()
  },
  methods: {
    async fetchUsers() {
      this.loading = true
      this.error = null
      try {
        const res = await axios.get(`/api/users?page=${this.page}`)
        this.users = res.data
      } catch (err) {
        this.error = err.response ? err.response.statusText : err.message
      } finally {
        this.loading = false
      }
    },
    nextPage() {
      this.page++
      this.inputPage = this.page + 1
      this.fetchUsers()
    },
    prevPage() {
      if (this.page > 0) {
        this.page--
        this.inputPage = this.page + 1
        this.fetchUsers()
      }
    },
    goToPage() {
      if (this.inputPage > 0) {
        this.page = this.inputPage - 1
        this.fetchUsers()
      }
    },
    goBack10() {
      this.page = Math.max(this.page - 10, 0)
      this.inputPage = this.page + 1
      this.fetchUsers()
    },
    goForward10() {
      this.page += 10
      this.inputPage = this.page + 1
      this.fetchUsers()
    },
    async searchUsersByPrefix() {
      if (this.prefix.length === 0) {
        this.prefixUsers = []
        return
      }

      try {
        const res = await axios.get(`/api/prefix-users/${this.prefix}`)
        this.prefixUsers = res.data
      } catch (err) {
        console.error('Erro na pesquisa por prefixo:', err)
        this.prefixUsers = []
      }
    }
  }
}
</script>


<style scoped>.user-list-container {
  max-width: 950px;
  margin: 40px auto;
  padding: 30px;
  background: linear-gradient(145deg, #ffffff, #f3f4f6);
  border-radius: 18px;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.06);
  font-family: "Segoe UI", sans-serif;
  transition: all 0.3s ease-in-out;
}

.title {
  text-align: center;
  font-size: 2em;
  color: #2c3e50;
  margin-bottom: 30px;
  position: relative;
}
.title::after {
  content: '';
  display: block;
  width: 60px;
  height: 4px;
  background-color: #3498db;
  margin: 12px auto 0;
  border-radius: 4px;
}

.status {
  text-align: center;
  font-size: 1.2em;
  color: #555;
  margin-top: 40px;
}
.status.error {
  color: #e74c3c;
}

.user-table {
  width: 100%;
  border-collapse: separate;
  border-spacing: 0 10px;
  margin-bottom: 30px;
}

.user-table th {
  background-color: #3498db;
  color: white;
  padding: 16px;
  text-align: left;
  border-top-left-radius: 8px;
  border-top-right-radius: 8px;
}

.user-table td {
  background-color: white;
  padding: 16px;
  border-bottom: 2px solid #ecf0f1;
  font-size: 0.95em;
  vertical-align: middle;
}

.user-table tr {
  transition: background-color 0.2s ease;
}
.user-table tr:hover {
  background-color: #f0faff;
}

.user-table a {
  color: #2980b9;
  font-weight: 600;
  text-decoration: none;
}
.user-table a:hover {
  text-decoration: underline;
}

.active,
.inactive {
  display: inline-block;
  padding: 6px 10px;
  border-radius: 12px;
  font-weight: bold;
  font-size: 0.85em;
}
.active {
  background-color: #2ecc71;
  color: white;
}
.inactive {
  background-color: #e74c3c;
  color: white;
}

.pagination {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 16px;
  margin-top: 20px;
  flex-wrap: wrap;
}

.pagination button {
  padding: 10px 20px;
  border: none;
  background: linear-gradient(to right, #3498db, #2980b9);
  color: white;
  font-weight: bold;
  border-radius: 8px;
  cursor: pointer;
  transition: background 0.3s ease;
}
.pagination button:hover {
  background: linear-gradient(to right, #2980b9, #1f6390);
}
.pagination button:disabled {
  background: #bdc3c7;
  cursor: not-allowed;
}

.pagination label {
  display: flex;
  align-items: center;
  gap: 6px;
  font-weight: 500;
  color: #2c3e50;
}

.pagination input {
  width: 60px;
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 6px;
  font-size: 0.95em;
  text-align: center;
  transition: border 0.2s ease;
}
.pagination input:focus {
  outline: none;
  border-color: #3498db;
  box-shadow: 0 0 3px rgba(52, 152, 219, 0.6);
}

.prefix-search {
  margin-bottom: 20px;
  text-align: center;
}

.prefix-search input {
  padding: 8px;
  width: 300px;
  border: 1px solid #ccc;
  border-radius: 6px;
  margin-left: 10px;
}

.prefix-search ul {
  list-style: none;
  padding: 0;
  margin-top: 10px;
  max-height: 200px;
  overflow-y: auto;
}

.prefix-search li {
  padding: 4px 0;
  font-weight: 500;
}

.back-home-button {
  display: inline-block;
  margin-bottom: 20px;
  background-color: #3498db;
  color: white;
  font-weight: bold;
  padding: 10px 20px;
  border-radius: 8px;
  text-decoration: none;
  transition: background 0.3s ease;
}
.back-home-button:hover {
  background-color: #2980b9;
}

</style>
