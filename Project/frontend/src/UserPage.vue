<template>
  <div class="user-page">
    <div v-if="loading" class="status">A carregar...</div>

    <div v-else-if="error" class="status error">Erro: {{ error }}</div>

    <div v-else-if="user === null" class="status">Utilizador não existe.</div>

    <div v-else class="card">
      <div class="header">
        <h2>{{ user.name }}</h2>
        <span class="country">🌍 {{ user.country_code }}</span>
      </div>

      <ul class="info-list">
        <li><i class="icon">🆔</i><span><strong>ID:</strong> {{ user.id }}</span></li>
        <li><i class="icon">🎂</i><span><strong>Idade:</strong> {{ user.age }} anos</span></li>
        <li><i class="icon">🛂</i><span><strong>Passaporte:</strong> {{ user.passport }}</span></li>
        <li><i class="icon">📅</i><span><strong>Conta criada:</strong> {{ formatDate(user.account_creation) }}</span></li>
        <li>
          <i class="icon">🔋</i>
          <span>
            <strong>Ativo?</strong>
            <span :class="user.active ? 'tag active' : 'tag inactive'">
              {{ user.active ? 'Sim' : 'Não' }}
            </span>
          </span>
        </li>
      
        <!-- Os seguintes dados não existem na resposta e causam erro -->
        <!-- 
        <li><i class="icon">🧳</i><span><strong>Reservas:</strong> {{ user.reservations }}</span></li>
        <li><i class="icon">💸</i><span><strong>Reservas reembolsadas:</strong> {{ user.reservations_refunded }}</span></li>
        <li><i class="icon">✈️</i><span><strong>Voos:</strong> {{ user.flights }}</span></li>
        <li><i class="icon">🛬</i><span><strong>Voos embarcados:</strong> {{ user.flights_arrived }}</span></li>
        <li><i class="icon">❌</i><span><strong>Voos perdidos:</strong> {{ user.flights_lost }}</span></li>
        <li><i class="icon">💰</i><span><strong>Total gasto:</strong> €{{ user.total_spent.toFixed(2) }}</span></li>
        -->
      </ul>


      <button class="back-button" @click="$router.back()">← Voltar</button>
    </div>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'UserPage',
  data() {
    return {
      user: null,
      loading: false,
      error: null
    }
  },
  mounted() {
    this.fetchData();
  },
  methods: {
    async fetchData() {
      this.loading = true;
      this.error = null;
      try {
        const userId = this.$route.params.id;
        const res = await axios.get(`/api/users/${userId}`);
        this.user = res.data;
      } catch (err) {
        this.error = err.response ? err.response.statusText : err.message;
        this.user = null;
      } finally {
        this.loading = false;
      }
    },
    formatDate(dateStr) {
      const d = new Date(dateStr);
      return d.toLocaleDateString('pt-PT', { year: 'numeric', month: 'long', day: 'numeric' });
    }
  },
  watch: {
    '$route.params.id': 'fetchData'
  }
}
</script>

<style scoped>
.user-page {
  max-width: 800px;
  margin: 60px auto;
  padding: 20px;
  font-family: 'Segoe UI', sans-serif;
  color: #2c3e50;
}

.status {
  text-align: center;
  font-size: 1.3em;
  padding: 20px;
  color: #444;
}
.status.error {
  color: #e74c3c;
}

.card {
  background: linear-gradient(to right, #fdfbfb, #ebedee);
  padding: 30px;
  border-radius: 16px;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.08);
  transition: all 0.3s ease;
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: baseline;
  margin-bottom: 20px;
}

.header h2 {
  font-size: 2em;
  margin: 0;
  color: #34495e;
}

.country {
  font-size: 1em;
  background-color: #dfe6e9;
  padding: 4px 10px;
  border-radius: 6px;
}

.info-list {
  list-style: none;
  padding: 0;
}

.info-list li {
  display: flex;
  align-items: center;
  margin-bottom: 12px;
}

.icon {
  width: 28px;
  font-size: 1.2em;
  margin-right: 12px;
}

.tag {
  padding: 2px 8px;
  border-radius: 6px;
  font-weight: bold;
  margin-left: 6px;
}
.tag.active {
  background-color: #2ecc71;
  color: white;
}
.tag.inactive {
  background-color: #e74c3c;
  color: white;
}

.back-button {
  margin-top: 30px;
  display: inline-block;
  background-color: #3498db;
  color: white;
  font-weight: bold;
  border: none;
  padding: 12px 24px;
  border-radius: 8px;
  cursor: pointer;
  transition: background 0.3s ease;
}
.back-button:hover {
  background-color: #2980b9;
}
</style>
