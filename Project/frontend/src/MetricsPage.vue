<template>
  <div class="metrics-page">
    <h2>Métricas</h2>
    <router-link to="/" class="back-home-button">← Voltar à página inicial</router-link>

    <div class="selectors">
      <label>
        Ano:
        <select v-model="selectedYear">
          <option value="">-- Nenhum --</option>
          <option v-for="year in years" :key="year" :value="year">{{ year }}</option>
        </select>
      </label>

      <label>
        Mês:
        <select v-model="selectedMonth" :disabled="!selectedYear">
          <option value="">-- Nenhum --</option>
          <option v-for="(name, index) in months" :key="index" :value="index + 1">
            {{ name }}
          </option>
        </select>
      </label>

      <button @click="fetchMetrics">Pesquisar</button>
    </div>

    <div v-if="loading" class="status">A carregar...</div>
    <div v-if="error" class="status error">{{ error }}</div>

    <div class="metrics-list">
      <div
        class="metric-card"
        v-for="(item, index) in metrics"
        :key="`${item.year ?? ''}-${item.month ?? ''}-${item.day ?? ''}-${index}`"
      >

        <h3>{{ getTitle(item) }}</h3>

        <p>Utilizadores: {{ item.users }}</p>
        <p>Reservas: {{ item.reservations }}</p>
        <p>Voos: {{ item.flights }}</p>
        <p>Passageiros: {{ item.passengers }}</p>
        <p>Passageiros únicos: {{ item.unique_passengers }}</p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'

const selectedYear = ref('')
const selectedMonth = ref('')
const metrics = ref([])
const loading = ref(false)
const error = ref(null)

const years = Array.from({ length: 20 }, (_, i) => 2010 + i)
const months = [
  'Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho',
  'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'
]

const formatMonth = (num) => String(num).padStart(2, '0')

const fetchMetrics = async () => {
  loading.value = true
  error.value = null
  metrics.value = []

  try {
    let url = ''
    if (!selectedYear.value) {
      url = '/api/metrics-years'
    } else if (selectedYear.value && !selectedMonth.value) {
      url = `/api/metrics-months/${selectedYear.value}`
    } else {
      const month = formatMonth(selectedMonth.value)
      url = `/api/metrics-days/${selectedYear.value}/${month}`
    }

    const res = await axios.get(url)
    metrics.value = Array.isArray(res.data) ? res.data : []
  } catch (err) {
    error.value = err.response?.statusText || err.message
  } finally {
    loading.value = false
  }
}

const getTitle = (item) => {
  if (item.day) return `📅 Dia ${item.day}`
  if (item.month) return `🗓 ${months[item.month - 1]}`
  return `📆 ${item.year}`
}
</script>


<style scoped>
.metrics-page {
  max-width: 900px;
  margin: 40px auto;
  padding: 20px;
  font-family: 'Segoe UI', sans-serif;
}

h2 {
  text-align: center;
  font-size: 2rem;
  margin-bottom: 20px;
}

.selectors {
  display: flex;
  justify-content: center;
  gap: 20px;
  flex-wrap: wrap;
  margin-bottom: 30px;
}

.selectors label {
  font-weight: bold;
  display: flex;
  flex-direction: column;
  align-items: center;
}

button {
  padding: 10px 20px;
  background-color: #3498db;
  color: white;
  font-weight: bold;
  border: none;
  border-radius: 8px;
  cursor: pointer;
}

button:hover {
  background-color: #2980b9;
}

.status {
  text-align: center;
  margin-top: 20px;
  font-size: 1.1em;
}

.status.error {
  color: red;
}

.metrics-list {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(240px, 1fr));
  gap: 20px;
  margin-top: 30px;
}

.metric-card {
  background: linear-gradient(135deg, #e0f7fa, #ffffff);
  border-radius: 14px;
  box-shadow: 0 6px 16px rgba(0, 0, 0, 0.08);
  padding: 20px;
  transition: transform 0.2s ease, box-shadow 0.2s ease;
  border-left: 5px solid #3498db;
}

.metric-card:hover {
  transform: scale(1.02);
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
}

.metric-card h3 {
  margin: 0 0 12px;
  font-size: 1.4rem;
  color: #2c3e50;
  font-weight: 600;
}
.metric-card p {
  margin: 4px 0;
  font-size: 0.95rem;
  color: #34495e;
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
